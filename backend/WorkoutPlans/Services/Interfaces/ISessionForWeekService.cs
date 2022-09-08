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
        List<SessionForWeek> FetchAllWorkoutSessionsForWeek(TrainingProgramWeekDTO trainingProgramWeekDTO);
        SessionForWeek CreateWorkoutSessionForWeek(TrainingProgramWeekDTO trainingProgramWeekDTO, WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO);
        void CreateMultipleWorkoutSessionsForWeek(TrainingProgramWeekDTO trainingProgramWeekDTO);
        void UpdateMultipleWorkoutSessionsForWeek(List<SessionForWeek> oldSessions, TrainingProgramWeekDTO trainingProgramWeekDTO);

        // TODO: 
        // DELETE
        // UPDATE
    }
}
