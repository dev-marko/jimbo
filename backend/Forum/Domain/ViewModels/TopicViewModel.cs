using Forum.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Domain.ViewModels
{
    public class TopicViewModel
    {
        public Guid TopicId { get; set; }
        public Guid SubforumId { get; set; }
        public string OwnerUsername { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<PostViewModel> Posts { get; set; }
    }
}
