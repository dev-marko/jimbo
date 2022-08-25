using Forum.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Domain.ViewModels
{
    public class SubforumViewModel
    {
        public Guid SubforumId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public List<TopicViewModel> Topics { get; set; }
    }
}
