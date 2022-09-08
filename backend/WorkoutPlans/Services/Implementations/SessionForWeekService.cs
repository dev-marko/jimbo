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
        private readonly ISessionForWeekRepository sessionForWeekRepository;
        private readonly IExerciseService exerciseService;
        private readonly ITrainingProgramService trainingProgramService;

        public SessionForWeekService(IRepository<SessionForWeek> sessionForWeekRepositoryGeneric, ISessionForWeekRepository sessionForWeekRepository, IExerciseService exerciseService, ITrainingProgramService trainingProgramService)
        {
            this.sessionForWeekRepositoryGeneric = sessionForWeekRepositoryGeneric;
            this.sessionForWeekRepository = sessionForWeekRepository;
            this.exerciseService = exerciseService;
            this.trainingProgramService = trainingProgramService;
        }

        public void CreateMultipleWorkoutSessionsForWeek(TrainingProgramWeekDTO trainingProgramWeekDTO)
        {
            var trainingProgramWeek = trainingProgramService.FetchTrainingProgramWeek(trainingProgramWeekDTO);
            trainingProgramWeekDTO.WorkoutSessions.ForEach(ws =>
            {
                var workoutSessionForExercise = exerciseService.FetchWorkoutSessionForExercise(ws);
                var sessionForWeek = new SessionForWeek
                {
                    Week = trainingProgramWeek,
                    WorkoutSession = workoutSessionForExercise
                };
                sessionForWeekRepositoryGeneric.Insert(sessionForWeek);
            });
        }

        public SessionForWeek CreateWorkoutSessionForWeek(TrainingProgramWeekDTO trainingProgramWeekDTO, WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO)
        {
            var trainingProgramWeek = trainingProgramService.FetchTrainingProgramWeek(trainingProgramWeekDTO);
            var workoutSessionForExercise = exerciseService.FetchWorkoutSessionForExercise(workoutSessionForExerciseDTO);

            var sessionForWeek = new SessionForWeek
            {
                Week = trainingProgramWeek,
                WorkoutSession = workoutSessionForExercise
            };

            return sessionForWeekRepositoryGeneric.Insert(sessionForWeek);
        }

        public List<SessionForWeek> FetchAllWorkoutSessionsForWeek(TrainingProgramWeekDTO trainingProgramWeekDTO)
        {
            if (trainingProgramWeekDTO.OldName != null)
            {
                return sessionForWeekRepository.FetchWorkoutSessionsForWeek(trainingProgramWeekDTO.TrainingProgramId, trainingProgramWeekDTO.OldName).ToList();
            }

            return sessionForWeekRepository.FetchWorkoutSessionsForWeek(trainingProgramWeekDTO.TrainingProgramId, trainingProgramWeekDTO.Name).ToList();
        }

        public void UpdateMultipleWorkoutSessionsForWeek(List<SessionForWeek> oldSessions, TrainingProgramWeekDTO trainingProgramWeekDTO)
        {
            for(int i = 0; i < oldSessions.Count; i++)
            {
                var toUpdate = oldSessions[i];
                var workoutSessionForExercise = exerciseService.FetchWorkoutSessionForExercise(trainingProgramWeekDTO.WorkoutSessions[i]);
                toUpdate.WorkoutSession = workoutSessionForExercise;
                sessionForWeekRepositoryGeneric.Update(toUpdate);
            }
        }
    }
}
