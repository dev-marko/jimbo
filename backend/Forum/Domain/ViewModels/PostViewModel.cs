using Forum.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Domain.ViewModels
{
    public class PostViewModel
    {
        public Guid PostId { get; set; }
        public Guid TopicId { get; set; }
        public string AuthorUsername { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
    }
}
