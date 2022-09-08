using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Enumerations;
using WorkoutPlans.Domain.Relations;

namespace WorkoutPlans.Domain.Models
{
    public class Exercise : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public MuscleGroup MuscleGroup { get; set; }
        public string VideoUrl { get; set; } // video tutorial showing you how the exercise is performed
        public virtual ICollection<WorkoutSessionForExercise> WorkoutSessions { get; set; }
    }
}
