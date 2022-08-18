using Forum.Domain.Models;
using Forum.Domain.ViewModels;
using Forum.Repository.Interfaces;
using Forum.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly ITopicService topicService;
        private readonly IUserService userService;

        public PostService(IPostRepository postRepository, ITopicService topicService, IUserService userService)
        {
            this.postRepository = postRepository;
            this.topicService = topicService;
            this.userService = userService;
        }

        public PostViewModel CreatePost(Post entity)
        {
            var post = postRepository.Insert(entity);
            return FetchPostViewModel(post.Id);
        }

        public PostViewModel DeletePost(Post entity)
        {
            var post = postRepository.Delete(entity);
            return FetchPostViewModel(post.Id);
        }

        public List<Post> FetchAllPosts()
        {
            return postRepository.FetchAllPosts().ToList();
        }

        public Post FetchPostById(Guid? id)
        {
            return postRepository.FetchPostById(id);
        }

        public List<PostViewModel> FetchPostsForTopic(Guid topicId)
        {
            var topic = topicService.FetchTopicById(topicId);
            var postViewModels = new List<PostViewModel>();

            foreach(var p in topic.Posts)
            {
                postViewModels.Add(FetchPostViewModel(p.Id));
            }

            return postViewModels;
        }

        public PostViewModel FetchPostViewModel(Guid id)
        {
            var post = FetchPostById(id);

            return new PostViewModel
            {
                PostId = post.Id,
                TopicId = post.TopicId,
                AuthorUsername = post.AuthorUsername,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                LastModified = post.LastModified
            };
        }

        public bool PostExists(Guid id)
        {
            return FetchPostById(id) != null;
        }

        public PostViewModel UpdatePost(Post entity)
        {
            var post = postRepository.Update(entity);
            return FetchPostViewModel(post.Id);
        }
    }
}
