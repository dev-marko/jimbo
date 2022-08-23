using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Models;

namespace WorkoutPlans.Domain.Relations
{
    public class WorkoutSessionInWeek : BaseEntity
    {
        public Guid WorkoutSessionId { get; set; }
        public WorkoutSession WorkoutSession { get; set; }
        public Guid WeekId { get; set; }
        public Week Week { get; set; }
    }
}
