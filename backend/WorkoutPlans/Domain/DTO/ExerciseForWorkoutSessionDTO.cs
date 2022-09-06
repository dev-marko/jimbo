using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutPlans.Domain.DTO
{
    public class ExerciseForWorkoutSessionDTO
    {
        public string SessionName { get; set; }
        public string Reps { get; set; }
        public string Sets { get; set; }
        public string RestTime { get; set; }
        public Guid ExerciseId { get; set; }
    }
}
