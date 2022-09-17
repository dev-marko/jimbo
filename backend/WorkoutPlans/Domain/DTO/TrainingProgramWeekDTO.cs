using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutPlans.Domain.DTO
{
    public class TrainingProgramWeekDTO
    {
        public Guid TrainingProgramId { get; set; }
        public string OldWeekName { get; set; } // add old name if editing or deleting week
        public string WeekName { get; set; }
        public List<WorkoutSessionForExerciseDTO> WorkoutSessions { get; set; }
    }
}
