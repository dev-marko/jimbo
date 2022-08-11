using Authentication.Domain.DTO;
using Authentication.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly IJWTManagerRepository jWTManagerRepository;

        public LoginController(IConfiguration config, IJWTManagerRepository jWTManagerRepository)
        {
            this.config = config;
            this.jWTManagerRepository = jWTManagerRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLoginDTO userLogin)
        {
            var user = jWTManagerRepository.Authenticate(userLogin);

            if (user != null)
            {
                var token = jWTManagerRepository.GenerateToken(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }
    }
}
