using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Models;

namespace WorkoutPlans.Domain.Relations
{
    public class WeekInTrainingProgram : BaseEntity
    {
        public Guid WeekId { get; set; }
        public Week Week { get; set; }
        public Guid TrainingProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
    }
}
