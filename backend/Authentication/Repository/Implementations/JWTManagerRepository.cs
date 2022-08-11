using Authentication.Context;
using Authentication.Domain.DTO;
using Authentication.Domain.Models;
using Authentication.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Repository.Implementations
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly AuthenticationContext context;
        private readonly DbSet<User> entities;
        private readonly IConfiguration configuration;

        public JWTManagerRepository(AuthenticationContext context, IConfiguration configuration)
        {
            this.context = context;
            this.entities = context.Set<User>();
            this.configuration = configuration;
        }

        public User Authenticate(UserLoginDTO userLogin)
        {
            return entities.SingleOrDefault(i => i.Username == userLogin.Username && i.Password == userLogin.Password);
        }

        public Tokens GenerateToken(User user)
        {
            // Steps
            // 1) Key
            // 2) Credentials that are going to be included in the token (based on the key)
            // 3) Define claims
            // 4) Create the actual token

            var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken
                (
                    configuration["JWT:Issuer"],
                    configuration["JWT:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: credentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            return new Tokens { Token = tokenHandler.WriteToken(token) };

            // An alternative way to create a token

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new System.Security.Claims.ClaimsIdentity(
            //            new Claim[]
            //            {
            //                new Claim(ClaimTypes.Name, user.Email)
            //            }
            //        ),
            //    Expires = DateTime.UtcNow.AddMinutes(30),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            //};

            //var token = tokenHandler.CreateToken(tokenDescriptor);

            //return new Tokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}
