using Authentication.Domain.DTO;
using Authentication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Authentication.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService userService;

        public RegisterController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register([FromBody] UserRegisterDTO userRegisterDTO)
        {

            if (userService.UserExists(userRegisterDTO))
            {
                return BadRequest(JsonSerializer.Serialize(new { error = "User already exists" }));
            }

            var user = userService.Register(userRegisterDTO);
            return Ok(JsonSerializer.Serialize(user));
        }
    }
}
