using Forum.Context;
using Forum.Domain.Enumerations;
using Forum.Domain.Models;
using Forum.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Repository.Implementations
{
    public class SubforumRepository : ISubforumRepository
    {
        private readonly ForumContext context;
        private readonly DbSet<Subforum> entities;

        public SubforumRepository(ForumContext context)
        {
            this.context = context;
            this.entities = context.Set<Subforum>();
        }

        public Subforum Delete(Subforum entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var subforum = entities.Remove(entity).Entity;
            context.SaveChanges();
            return subforum;
        }

        public IEnumerable<Subforum> FetchAllSubforums()
        {
            return entities.AsEnumerable();
        }

        public IEnumerable<Subforum> FetchAllSubforumsByCategory(string category)
        { 
            Categories _category = (Categories)Enum.Parse(typeof(Categories), category);
            return entities.Where(i => i.Category.Equals(_category));
        }

        public Subforum FetchSubforumById(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            return entities
                .Include(e => e.Topics)
                .SingleOrDefault(i => i.Id.Equals(id));
        }

        public Subforum Insert(Subforum entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var subforum = entities.Add(entity).Entity;
            context.SaveChanges();
            return subforum;
        }

        public Subforum Update(Subforum entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var subforum = entities.Update(entity).Entity;
            context.SaveChanges();
            return subforum;
        }
    }
}
