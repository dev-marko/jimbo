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
        private readonly ITopicRepository topicRepository;

        public PostService(IPostRepository postRepository, ITopicRepository topicRepository)
        {
            this.postRepository = postRepository;
            this.topicRepository = topicRepository;
        }

        public PostViewModel CreatePost(Post entity)
        {
            var post = postRepository.Insert(entity);
            return FetchPostViewModelById(post.Id);
        }

        public PostViewModel DeletePost(Post entity)
        {
            var post = postRepository.Delete(entity);
            return FetchPostViewModel(post);
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
            var topic = topicRepository.FetchTopicById(topicId);
            return topic.Posts.Select(p => FetchPostViewModelById(p.Id)).ToList();
        }

        public PostViewModel FetchPostViewModel(Post entity)
        {
            return new PostViewModel
            {
                PostId = entity.Id,
                TopicId = entity.TopicId,
                AuthorUsername = entity.AuthorUsername,
                Content = entity.Content,
                CreatedAt = entity.CreatedAt,
                LastModified = entity.LastModified
            };
        }

        public PostViewModel FetchPostViewModelById(Guid id)
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
            return FetchPostViewModelById(post.Id);
        }
    }
}
