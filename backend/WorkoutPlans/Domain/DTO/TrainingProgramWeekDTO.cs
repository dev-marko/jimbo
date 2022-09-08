using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutPlans.Domain.DTO
{
    public class TrainingProgramWeekDTO
    {
        public Guid TrainingProgramId { get; set; }
        public string OldName { get; set; } // add old name if editing week
        public string Name { get; set; }
        public List<WorkoutSessionForExerciseDTO> WorkoutSessions { get; set; }
    }
}
