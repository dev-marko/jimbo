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
    public class PostRepository : IPostRepository
    {
        private readonly ForumContext context;
        private readonly DbSet<Post> entities;

        public PostRepository(ForumContext context)
        {
            this.context = context;
            this.entities = context.Set<Post>();
        }

        public IEnumerable<Post> FetchAllPosts()
        {
            return entities.AsEnumerable();
        }

        public IEnumerable<Post> FetchAllPostsByTopic(Guid? topicId)
        {
            if (topicId == null)
            {
                throw new ArgumentNullException("id");
            }

            return entities.Where(e => e.TopicId.Equals(topicId)).AsEnumerable();
        }

        public Post FetchPostById(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            return entities.SingleOrDefault(e => e.Id.Equals(id));
        }

        public Post Insert(Post entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var post = entities.Add(entity).Entity;
            context.SaveChanges();
            return post;
        }

        public Post Update(Post entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var post = entities.Update(entity).Entity;
            context.SaveChanges();
            return post;
        }

        public Post Delete(Post entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var post = entities.Remove(entity).Entity;
            context.SaveChanges();
            return post;
        }
    }
}
