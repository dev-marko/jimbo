using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.DTO;
using WorkoutPlans.Domain.Relations;

namespace WorkoutPlans.Services.Interfaces
{
    public interface ISessionForWeekService
    {
        List<SessionForWeek> FetchAllWorkoutSessionsForWeek(Guid trainingProgramId, string weekName);
        SessionForWeek CreateWorkoutSessionForWeek(TrainingProgramWeekDTO trainingProgramWeekDTO, ExerciseForWorkoutSessionDTO exerciseForWorkoutSessionDTO);

        // TODO: 
        // DELETE
        // UPDATE
    }
}
