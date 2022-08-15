using Authentication.Domain.Models;
using Authentication.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserRepository userRepository { get; set; }

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok("Hello you are public user");
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentlyLoggedInUser ()
        {
            var currentUser = GetCurrentUser();

            return Ok(currentUser);
        }

        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var claims = identity.Claims;
                var currentUser = userRepository.FetchUserByUsername(claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value);
                return currentUser;
            }

            return null;
        }
    }
}
