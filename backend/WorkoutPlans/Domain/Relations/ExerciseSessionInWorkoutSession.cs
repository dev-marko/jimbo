using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Models;

namespace WorkoutPlans.Domain.Relations
{
    public class ExerciseSessionInWorkoutSession : BaseEntity
    {
        public Guid ExerciseSessionId { get; set; }
        public ExerciseSession ExerciseSession { get; set; }
        public Guid WorkoutSessionId { get; set; }
        public WorkoutSession WorkoutSession { get; set; }
    }
}
