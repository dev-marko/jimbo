using Authentication.Context;
using Authentication.Domain.DTO;
using Authentication.Domain.Models;
using Authentication.Services.Interfaces;
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

namespace Authentication.Services.Implementations
{
    public class JWTManagerService : IJWTManagerService
    {
        private readonly IConfiguration configuration;

        public JWTManagerService(IConfiguration configuration)
        {
            this.configuration = configuration;
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
        }
    }
}
