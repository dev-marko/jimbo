using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Context;
using WorkoutPlans.Domain.Relations;
using WorkoutPlans.Repository.Interfaces;

namespace WorkoutPlans.Repository.Implementations
{
    public class SessionForWeekRepository : ISessionForWeekRepository
    {
        private readonly WorkoutPlansContext context;
        private readonly DbSet<SessionForWeek> sessionForWeeks;

        public SessionForWeekRepository(WorkoutPlansContext context)
        {
            this.context = context;
            this.sessionForWeeks = context.Set<SessionForWeek>();
        }

        public IEnumerable<SessionForWeek> FetchWorkoutSessionsForWeek(Guid trainingProgramId, string weekName)
        {
            return sessionForWeeks
                .Include(e => e.WorkoutSession)
                .ThenInclude(e => e.Exercise)
                .Where(e => (e.Week.TrainingProgramId == trainingProgramId) && e.Week.Name.Equals(weekName));
        }
    }
}
