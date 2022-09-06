using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Models;

namespace WorkoutPlans.Domain.Relations
{
    public class TrainingProgramWeek
    {
        public string Name { get; set; }
        public Guid TrainingProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        public virtual ICollection<SessionForWeek> Sessions { get; set; }
    }
}
