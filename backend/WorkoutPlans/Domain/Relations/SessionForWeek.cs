using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Models;

namespace WorkoutPlans.Domain.Relations
{
    public class SessionForWeek : BaseEntity
    {
        public TrainingProgramWeek Week { get; set; }
        public WorkoutSessionForExercise WorkoutSession { get; set; }
    }
}
