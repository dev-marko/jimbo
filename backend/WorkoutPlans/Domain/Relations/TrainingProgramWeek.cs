using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Models;

namespace WorkoutPlans.Domain.Relations
{
    public class TrainingProgramWeek
    {
        public string WeekName { get; set; }
        public Guid TrainingProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        public virtual ICollection<WorkoutSessionForExercise> WorkoutSessions { get; set; }
    }
}
