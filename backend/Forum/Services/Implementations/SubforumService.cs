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
    public class SubforumService : ISubforumService
    {
        private readonly ISubforumRepository subforumRepository;
        private readonly ITopicService topicService;

        public SubforumService(ISubforumRepository subforumRepository, ITopicService topicService)
        {
            this.subforumRepository = subforumRepository;
            this.topicService = topicService;
        }

        public SubforumViewModel UpdateSubforum(Subforum entity)
        {
            var subforum = subforumRepository.Update(entity);

            return FetchSubforumViewModelWithTopics(subforum);
        }

        public bool SubforumExists(Guid id)
        {
            return FetchSubforumById(id) != null;
        }

        public SubforumViewModel CreateSubforum(Subforum entity)
        {
            var subforum = subforumRepository.Insert(entity);

            return FetchSubforumViewModelWithTopics(subforum);
        }

        public SubforumViewModel DeleteSubforum(Subforum entity)
        {
            var subforum = subforumRepository.Delete(entity);

            return FetchSubforumViewModelWithTopics(subforum);
        }

        public List<Subforum> FetchAllSubforums()
        {
            return subforumRepository.FetchAllSubforums().ToList();
        }

        public List<Subforum> FetchAllSubforumsByCategory(string category)
        {
            return subforumRepository.FetchAllSubforumsByCategory(category).ToList();
        }

        public Subforum FetchSubforumById(Guid? id)
        {
            return subforumRepository.FetchSubforumById(id);
        }

        public SubforumViewModel FetchSubforumViewModelWithTopicsById(Guid id)
        {
            var subforum = FetchSubforumById(id);
            var topics = new List<TopicViewModel>();

            foreach(var t in subforum.Topics)
            {
                topics.Add(topicService.FetchTopicViewModelById(t.Id));
            }

            return new SubforumViewModel
            {
                SubforumId = subforum.Id,
                Name = subforum.Name,
                Description = subforum.Description,
                Category = subforum.Category,
                Topics = topics
            };
        }

        public SubforumViewModel FetchSubforumViewModelWithTopics(Subforum entity)
        {
            var topics = new List<TopicViewModel>();

            foreach (var t in entity.Topics)
            {
                topics.Add(topicService.FetchTopicViewModelById(t.Id));
            }

            return new SubforumViewModel
            {
                SubforumId = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Category = entity.Category,
                Topics = topics
            };
        }
    }
}
