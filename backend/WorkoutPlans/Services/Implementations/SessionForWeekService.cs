using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.DTO;
using WorkoutPlans.Domain.Relations;
using WorkoutPlans.Repository.Interfaces;
using WorkoutPlans.Services.Interfaces;

namespace WorkoutPlans.Services.Implementations
{
    public class SessionForWeekService : ISessionForWeekService
    {
        private readonly IRepository<SessionForWeek> sessionForWeekRepositoryGeneric;
        private readonly IExerciseService exerciseService;
        private readonly ITrainingProgramService trainingProgramService;

        public SessionForWeekService(IRepository<SessionForWeek> sessionForWeekRepositoryGeneric, IExerciseService exerciseService, ITrainingProgramService trainingProgramService)
        {
            this.sessionForWeekRepositoryGeneric = sessionForWeekRepositoryGeneric;
            this.exerciseService = exerciseService;
            this.trainingProgramService = trainingProgramService;
        }

        public SessionForWeek CreateWorkoutSessionForWeek(TrainingProgramWeekDTO trainingProgramWeekDTO, ExerciseForWorkoutSessionDTO exerciseForWorkoutSessionDTO)
        {
            var trainingProgramWeek = trainingProgramService.FetchTrainingProgramWeek(trainingProgramWeekDTO);
            var exerciseForWorkoutSession = exerciseService.FetchExerciseForWorkoutSession(exerciseForWorkoutSessionDTO);

            var sessionForWeek = new SessionForWeek
            {
                Week = trainingProgramWeek,
                ExerciseSession = exerciseForWorkoutSession
            };

            return sessionForWeekRepositoryGeneric.Insert(sessionForWeek);
        }

        public List<SessionForWeek> FetchAllWorkoutSessionsForWeek(Guid trainingProgramId, string weekName)
        {
            throw new NotImplementedException();
        }
    }
}
