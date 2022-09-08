using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Models;
using WorkoutPlans.Domain.Relations;

namespace WorkoutPlans.Domain.Relations
{
    public class WorkoutSessionForExercise
    {
        public string Name { get; set; }
        public string Reps { get; set; }
        public string Sets { get; set; }
        public string RestTime { get; set; }
        public Guid ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public virtual ICollection<SessionForWeek> Weeks { get; set; }
    }
}
