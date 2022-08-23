using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Relations;

namespace WorkoutPlans.Domain.Models
{
    public class WorkoutSession : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ExerciseSessionInWorkoutSession> Exercises { get; set; }
        public virtual ICollection<WorkoutSessionInWeek> Weeks { get; set; }
    }
}
