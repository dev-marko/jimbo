using Forum.Domain.DTO;
using Forum.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Forum.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet("{id}")]
        public IActionResult GetPost(Guid id)
        {
            if (id == null || !postService.PostExists(id))
            {
                return NotFound(new { error = $"Post with ID: '{id}' not found" });
            }

            var postViewModel = postService.FetchPostViewModelById(id);
            return Ok(postViewModel);
        }

        [HttpPut("{id}")]
        public IActionResult EditPost(Guid id, [FromBody] PostDTO postDTO)
        {
            if (id == null || !postService.PostExists(id))
            {
                return NotFound(new { error = $"Post with ID: '{id}' not found" });
            }

            var postToEdit = postService.FetchPostById(id);

            postToEdit.Content = postDTO.Content;
            postToEdit.LastModified = DateTime.UtcNow;

            var postViewModel = postService.UpdatePost(postToEdit);

            return Ok(postViewModel);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public IActionResult DeletePost(Guid id)
        {
            if (id == null || !postService.PostExists(id))
            {
                return NotFound(new { error = $"Post with ID: '{id}' not found" });
            }

            var postToDelete = postService.FetchPostById(id);
            var postViewModel = postService.DeletePost(postToDelete);
            return Ok(postViewModel);
        }
    }
}
