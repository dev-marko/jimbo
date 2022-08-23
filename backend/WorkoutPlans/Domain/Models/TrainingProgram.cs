using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Relations;

namespace WorkoutPlans.Domain.Models
{
    public class TrainingProgram : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<WeekInTrainingProgram> Weeks { get; set; }
    }
}
