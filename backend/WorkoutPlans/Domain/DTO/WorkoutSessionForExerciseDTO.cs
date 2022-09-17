using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutPlans.Domain.DTO
{
    public class WorkoutSessionForExerciseDTO
    {
        public string OldSessionName { get; set; } // add old name if editing or deleting workout session
        public string SessionName { get; set; }            
        public string Reps { get; set; }
        public string Sets { get; set; }
        public string RestTime { get; set; }
        public Guid OldExerciseId { get; set; } // add old exercise id if editing or deleting workout session
        public Guid ExerciseId { get; set; }
        public string OldWeekName { get; set; }
        public string WeekName { get; set; }
        public Guid TrainingProgramId { get; set; }
    }
}
