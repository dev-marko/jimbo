using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Relations;

namespace WorkoutPlans.Domain.Models
{
    public class ExerciseSession : BaseEntity
    {
        public Exercise Exercise { get; set; }
        public string Sets { get; set; }
        public string Reps { get; set; }
        public string RestTime { get; set; }
        public virtual ICollection<ExerciseSessionInWorkoutSession> Workouts { get; set; }

    }
}
