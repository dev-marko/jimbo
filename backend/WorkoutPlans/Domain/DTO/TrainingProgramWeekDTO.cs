using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutPlans.Domain.DTO
{
    public class TrainingProgramWeekDTO
    {
        public Guid TrainingProgramId { get; set; }
        public string Name { get; set; }
    }
}
