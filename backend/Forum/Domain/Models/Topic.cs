using Forum.Domain.Enumerations;
using Forum.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Domain.Models
{
    public class Topic
    {
        public Guid Id { get; set; }
        public Guid SubforumId { get; set; }
        public Subforum Subforum { get; set; }
        public string OwnerUsername { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
