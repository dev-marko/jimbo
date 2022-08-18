using Forum.Domain.Models;
using Forum.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services.Interfaces
{
    public interface ITopicService
    {
        List<Topic> FetchAllTopics();
        Topic FetchTopicById(Guid? id);
        TopicViewModel FetchTopicViewModelWithPostsById(Guid id);
        TopicViewModel FetchTopicViewModelWithPosts(Topic entity);
        TopicViewModel FetchTopicViewModelById(Guid id);
        List<TopicViewModel> FetchTopicsForSubforum(Guid subforumId);
        TopicViewModel CreateTopic(Topic entity);
        TopicViewModel UpdateTopic(Topic entity);
        TopicViewModel DeleteTopic(Topic entity);
        bool TopicExists(Guid id);
    }
}
