using Forum.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Repository.Interfaces
{
    public interface ITopicRepository
    {
        IEnumerable<Topic> FetchAllTopics();
        IEnumerable<Topic> FetchTopicsForSubforum(Guid subforumId);
        Topic FetchTopicById(Guid? id);
        Topic Insert(Topic entity);
        Topic Update(Topic entity);
        Topic Delete(Topic entity);
    }
}
