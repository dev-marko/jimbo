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
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository topicRepository;
        private readonly ISubforumRepository subforumRepository;
        private readonly IPostService postService;

        public TopicService(ITopicRepository topicRepository, ISubforumRepository subforumRepository, IPostService postService)
        {
            this.topicRepository = topicRepository;
            this.subforumRepository = subforumRepository;
            this.postService = postService;
        }

        public TopicViewModel CreateTopic(Topic entity)
        {
            var topic = topicRepository.Insert(entity);
            return FetchTopicViewModelWithPosts(topic);
        }

        public TopicViewModel DeleteTopic(Topic entity)
        {
            var topic = topicRepository.Delete(entity);
            return FetchTopicViewModelWithPosts(topic);
        }

        public List<Topic> FetchAllTopics()
        {
            return topicRepository.FetchAllTopics().ToList();
        }

        public Topic FetchTopicById(Guid? id)
        {
            return topicRepository.FetchTopicById(id);
        }

        public List<TopicViewModel> FetchTopicsForSubforum(Guid subforumId)
        {
            var topics = subforumRepository.FetchSubforumById(subforumId).Topics;
            return topics.Select(t => FetchTopicViewModelWithPosts(t)).ToList();
        }

        public TopicViewModel FetchTopicViewModelWithPosts(Topic entity)
        {
            var posts = entity.Posts;
            var postViewModels = posts.Select(p => postService.FetchPostViewModel(p.Id)).ToList();

            return new TopicViewModel
            {
                TopicId = entity.Id,
                SubforumId = entity.SubforumId,
                OwnerUsername = entity.OwnerUsername,
                Title = entity.Title,
                CreatedAt = entity.CreatedAt,
                Posts = postViewModels
            };
        }

        public TopicViewModel FetchTopicViewModelById(Guid id)
        {
            var topic = FetchTopicById(id);

            return new TopicViewModel
            {
                TopicId = topic.Id,
                SubforumId = topic.SubforumId,
                OwnerUsername = topic.OwnerUsername,
                Title = topic.Title,
                CreatedAt = topic.CreatedAt
            };
        }

        public TopicViewModel FetchTopicViewModelWithPostsById(Guid id)
        {
            var topic = FetchTopicById(id);
            var posts = topic.Posts;
            var postViewModels = posts.Select(p => postService.FetchPostViewModel(p.Id)).ToList();

            return new TopicViewModel
            {
                TopicId = topic.Id,
                SubforumId = topic.SubforumId,
                OwnerUsername = topic.OwnerUsername,
                Title = topic.Title,
                CreatedAt = topic.CreatedAt,
                Posts = postViewModels
            };
        }

        public bool TopicExists(Guid id)
        {
            return FetchTopicById(id) != null;
        }

        public TopicViewModel UpdateTopic(Topic entity)
        {
            var topic = topicRepository.Update(entity);
            return FetchTopicViewModelWithPosts(topic);
        }
    }
}
