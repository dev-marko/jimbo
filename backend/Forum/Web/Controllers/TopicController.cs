using Forum.Domain.DTO;
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
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Forum.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService topicService;
        private readonly IUserService userService;
        private readonly IPostService postService;

        public TopicController(ITopicService topicService, IUserService userService, IPostService postService)
        {
            this.topicService = topicService;
            this.userService = userService;
            this.postService = postService;
        }

        // END-POINTS FOR TOPICS
        [HttpGet("{id}")]
        public IActionResult GetTopic(Guid id)
        {
            if (id == null || !topicService.TopicExists(id))
            {
                return NotFound(JsonConvert.SerializeObject(new { error = $"Topic with ID: '{id}' not found" }));
            }

            var topicViewModel = topicService.FetchTopicViewModelWithPostsById(id);
            return Ok(topicViewModel);
        }

        [HttpGet("{id}/posts")]
        public IActionResult GetPostsForTopic(Guid id)
        {
            if (id == null || !topicService.TopicExists(id))
            {
                return NotFound(JsonConvert.SerializeObject(new { error = $"Topic with ID: '{id}' not found" }));
            }

            var posts = postService.FetchPostsForTopic(id);
            return Ok(posts);
        }

        // POST METHOD FOR ADDING A NEW TOPIC IS IN THE FORUM CONTROLLER

        [HttpPut("{id}")]
        //[Authorize]
        public IActionResult EditTopic(Guid id, [FromBody] TopicDTO topicDTO)
        {
            if (id == null || !topicService.TopicExists(id))
            {
                return NotFound(JsonConvert.SerializeObject(new { error = $"Topic with ID: '{id}' not found" }));
            }

            var topic = topicService.FetchTopicById(id);
            topic.Title = topicDTO.Title;

            var topicViewModel = topicService.UpdateTopic(topic);

            return Ok(topicViewModel);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public IActionResult DeleteTopic(Guid id)
        {
            if (id == null || !topicService.TopicExists(id))
            {
                return NotFound(JsonConvert.SerializeObject(new { error = $"Topic with ID: '{id}' not found" }));
            }

            var topicToDelete = topicService.FetchTopicById(id);
            var topicViewModel = topicService.DeleteTopic(topicToDelete);
            return Ok(topicViewModel);
        }

        // END-POINTS FOR POSTS

        [HttpPost("{id}/add-post")]
        //[Authorize]
        public IActionResult AddPostToTopic(Guid id, [FromBody] PostDTO postDTO)
        {
            string token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            
            if (string.IsNullOrEmpty(token))
            {
                return NotFound(JsonConvert.SerializeObject(new { error = "Invalid Bearer Token" }));
            }

            User loggedInUser = userService.FetchCurrentUser(token).Result;

            if (loggedInUser == null)
            {
                return NotFound(JsonConvert.SerializeObject(new { error = "User not found, cannot create topic" }));

            }

            var topic = topicService.FetchTopicById(id);

            if (id == null || !topicService.TopicExists(id))
            {
                return NotFound(JsonConvert.SerializeObject(new { error = $"Topic with ID: '{id}' not found" }));
            }

            var post = new Post
            {
                TopicId = topic.Id,
                Topic = topic,
                AuthorUsername = loggedInUser.Username,
                Content = postDTO.Content,
                CreatedAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

            var postViewModel = postService.CreatePost(post);

            return Ok(postViewModel);
        }
    }
}
