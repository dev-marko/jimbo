﻿using Forum.Domain.DTO;
using Forum.Domain.Models;
using Forum.Domain.ViewModels;
using Forum.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/**
 * Forum and Sub-forum API controller
 */

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly ISubforumService subforumService;
        private readonly IUserService userService;
        private readonly ITopicService topicService;

        public ForumController(ISubforumService subforumService, IUserService userService, ITopicService topicService)
        {
            this.subforumService = subforumService;
            this.userService = userService;
            this.topicService = topicService;
        }

        [HttpGet("sub-forum/{id}")]
        public IActionResult GetSubforum(Guid id)
        {
            var subforumViewModel = subforumService.FetchSubforumViewModelWithTopicsById(id);

            return Ok(subforumViewModel);
        }

        [HttpGet("sub-forum/{id}/topics")]
        public IActionResult GetAllTopicsForSubforum(Guid id)
        {
            var subforum = subforumService.FetchSubforumById(id);
            return Ok(subforum.Topics.ToList());
        }

        [HttpPost("sub-forum")]
        [Authorize]
        public IActionResult AddSubforum([FromBody] SubforumDTO subforumDTO)
        {
            Subforum newSubforum = new Subforum
            {
                Name = subforumDTO.Name,
                Description = subforumDTO.Description,
                Category = subforumDTO.Category
            };

            var subforumViewModel = subforumService.CreateSubforum(newSubforum);

            return Ok(subforumViewModel);
        }

        [HttpPut("sub-forum/{id}")]
        [Authorize]
        public IActionResult EditSubforum(Guid id, [FromBody] SubforumDTO subforumDTO)
        {
            if (id == null || !subforumService.SubforumExists(id))
            {
                return NotFound(JsonConvert.SerializeObject(new { error = $"Subforum with ID: '{id}' not found" }));
            }

            var subforumToEdit = subforumService.FetchSubforumById(id);

            subforumToEdit.Name = subforumDTO.Name;
            subforumToEdit.Description = subforumDTO.Description;
            subforumToEdit.Category = subforumDTO.Category;

            var subforumViewModel = subforumService.UpdateSubforum(subforumToEdit);

            return Ok(subforumViewModel);
        }

        [HttpDelete("sub-forum/{id}")]
        [Authorize]
        public IActionResult DeleteSubforum(Guid id)
        {
            Subforum subforumToDelete = subforumService.FetchSubforumById(id);
            var subforumViewModel = subforumService.DeleteSubforum(subforumToDelete);

            return Ok(subforumViewModel);
        }

        // END-POINT FOR ADDING A NEW POST

        [HttpPost("sub-forum/{id}/add-topic")]
        [Authorize]
        public IActionResult AddTopicToSubforum(Guid id, [FromBody] TopicDTO topicDTO)
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var loggedInUser = userService.FetchCurrentUser(token).Result;
            var subforum = subforumService.FetchSubforumById(id);

            var topic = new Topic
            {
                OwnerUsername = loggedInUser.Username,
                SubforumId = subforum.Id,
                Subforum = subforum,
                Title = topicDTO.Title,
                CreatedAt = DateTime.UtcNow
            };

            topicService.CreateTopic(topic);

            return Ok();
        }
    }
}
