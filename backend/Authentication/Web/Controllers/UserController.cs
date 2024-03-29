﻿using Authentication.Domain.Models;
using Authentication.Repository.Interfaces;
using Authentication.Services.Interfaces;
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
        public IUserService userService { get; set; }

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        // TODO: Edit user properties, forgot password, change email, username etc.

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

        [HttpGet("{id}")]
        public IActionResult GetUserByUsername(string username)
        {
            var user = userService.FetchUserByUsername(username);

            return Ok(user);
        }

        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null) return null;
            var claims = identity.Claims;
            var currentUser = userService.FetchUserByUsername(claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value);
            return currentUser;

        }
    }
}
