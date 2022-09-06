using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Relations;

namespace WorkoutPlans.Repository.Interfaces
{
    public interface ISessionForWeekRepository
    {
        IEnumerable<SessionForWeek> FetchWorkoutSessionsForWeek(Guid trainingProgramId, string weekName);
    }
}
