using Forum.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Domain.DTO
{
    public class SubforumDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Categories Category { get; set; }
    }
}
