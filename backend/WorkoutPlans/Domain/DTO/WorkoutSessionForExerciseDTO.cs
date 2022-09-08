using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutPlans.Domain.DTO
{
    public class WorkoutSessionForExerciseDTO
    {
        public string OldName { get; set; } // add old name if editing workout session
        public string Name { get; set; }            
        public string Reps { get; set; }
        public string Sets { get; set; }
        public string RestTime { get; set; }
        public Guid OldExerciseId { get; set; } // add old exercise id if editing workout session
        public Guid ExerciseId { get; set; }
    }
}
