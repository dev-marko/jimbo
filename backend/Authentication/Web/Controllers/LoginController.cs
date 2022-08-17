using Authentication.Domain.DTO;
using Authentication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Authentication.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly IJWTManagerService jWTManagerService;
        private readonly IUserService userService;

        public LoginController(IConfiguration config, IJWTManagerService jWTManagerService, IUserService userService)
        {
            this.config = config;
            this.jWTManagerService = jWTManagerService;
            this.userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLoginDTO userLogin)
        {
            var user = userService.Authenticate(userLogin);

            if (user != null)
            {
                var token = jWTManagerService.GenerateToken(user);
                return Ok(JsonSerializer.Serialize(new { token = token.Token, username = user.Username }));
            }

            return NotFound(JsonSerializer.Serialize(new { error = "User not found"}));
        }
    }
}
