using Forum.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Domain.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid TopicId { get; set; }
        public Topic Topic { get; set; }
        public string AuthorUsername { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
    }
}
