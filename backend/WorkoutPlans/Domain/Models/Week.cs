using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Relations;

namespace WorkoutPlans.Domain.Models
{
    public class Week : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<WorkoutSessionInWeek> Workouts { get; set; }
        public virtual ICollection<WeekInTrainingProgram> TrainingPrograms { get; set; }
    }
}
