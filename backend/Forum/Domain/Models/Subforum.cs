using Forum.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Domain.Models
{
    public class Subforum
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Categories Category { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
    }
}
