using Forum.Context;
using Forum.Domain.Models;
using Forum.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Repository.Implementations
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ForumContext context;
        private readonly DbSet<Topic> entities;

        public TopicRepository(ForumContext context)
        {
            this.context = context;
            this.entities = context.Set<Topic>();
        }

        public Topic Delete(Topic entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var topic = entities.Remove(entity).Entity;
            context.SaveChanges();
            return topic;
        }

        public IEnumerable<Topic> FetchAllTopics()
        {
            return entities.AsEnumerable();
        }

        public Topic FetchTopicById(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            return entities
                .Include(e => e.Posts)
                .SingleOrDefault(i => i.Id.Equals(id));
        }

        public IEnumerable<Topic> FetchTopicsForSubforum(Guid subforumId)
        {
            return entities.Where(e => e.SubforumId.Equals(subforumId)).AsEnumerable();
        }

        public Topic Insert(Topic entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var topic = entities.Add(entity).Entity;
            context.SaveChanges();
            return topic;
        }

        public Topic Update(Topic entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var topic = entities.Update(entity).Entity;
            context.SaveChanges();
            return topic;
        }
    }
}
