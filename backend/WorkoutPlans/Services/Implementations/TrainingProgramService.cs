using System;
using System.Collections.Generic;
using System.Linq;
using WorkoutPlans.Domain.DTO;
using WorkoutPlans.Domain.Models;
using WorkoutPlans.Domain.Relations;
using WorkoutPlans.Repository.Interfaces;
using WorkoutPlans.Services.Interfaces;

namespace WorkoutPlans.Services.Implementations
{
    public class TrainingProgramService : ITrainingProgramService
    {
        private readonly IRepository<TrainingProgram> trainingProgramRepositoryGeneric;
        private readonly ITrainingProgramRepository trainingProgramRepository;

        public TrainingProgramService(IRepository<TrainingProgram> trainingProgramRepositoryGeneric, ITrainingProgramRepository trainingProgramRepository)
        {
            this.trainingProgramRepositoryGeneric = trainingProgramRepositoryGeneric;
            this.trainingProgramRepository = trainingProgramRepository;
        }

        // TRAINING PROGRAM
        public TrainingProgram FetchTrainingProgramById(Guid trainingProgramId)
        {
            return trainingProgramRepositoryGeneric.FetchById(trainingProgramId);
        }

        public TrainingProgram CreateTrainingProgram(TrainingProgramDTO trainingProgramDTO)
        {
            var trainingProgram = new TrainingProgram
            {
                Name = trainingProgramDTO.Name,
                Description = trainingProgramDTO.Description
            };

            return trainingProgramRepositoryGeneric.Insert(trainingProgram);
        }

        public TrainingProgram DeleteTrainingProgram(Guid trainingProgramId)
        {
            var trainingProgram = trainingProgramRepositoryGeneric.FetchById(trainingProgramId);
            return trainingProgramRepositoryGeneric.Delete(trainingProgram);
        }

        public List<TrainingProgram> FetchAllTrainingPrograms()
        {
            return trainingProgramRepositoryGeneric.FetchAll().ToList();
        }

        public bool TrainingProgramExists(Guid trainingProgramId)
        {
            return (trainingProgramId != null) && trainingProgramRepositoryGeneric.FetchById(trainingProgramId) != null;
        }

        public TrainingProgram UpdateTrainingProgram(Guid trainingProgramId, TrainingProgramDTO trainingProgramDTO)
        {
            var toUpdate = trainingProgramRepositoryGeneric.FetchById(trainingProgramId);

            toUpdate.Name = trainingProgramDTO.Name;
            toUpdate.Description = trainingProgramDTO.Description;

            return trainingProgramRepositoryGeneric.Update(toUpdate);
        }

        // TRAINING PROGRAM WEEK
        public TrainingProgramWeek CreateTrainingProgramWeek(TrainingProgramWeekDTO trainingProgramWeekDTO)
        {
            var trainingProgramWeek = new TrainingProgramWeek
            {
                WeekName = trainingProgramWeekDTO.WeekName,
                TrainingProgramId = trainingProgramWeekDTO.TrainingProgramId,
                TrainingProgram = FetchTrainingProgramById(trainingProgramWeekDTO.TrainingProgramId)
            };

            return trainingProgramRepository.InsertTrainingProgramWeek(trainingProgramWeek);
        }

        public TrainingProgramWeek DeleteTrainingProgramWeek(TrainingProgramWeekDTO trainingProgramWeekDTO)
        {
            var toDelete = trainingProgramRepository.FetchTrainingProgramWeek(trainingProgramWeekDTO.TrainingProgramId, trainingProgramWeekDTO.OldWeekName);
            return trainingProgramRepository.DeleteTrainingProgramWeek(toDelete);
        }

        public List<TrainingProgramWeek> FetchAllWeeksForTrainingProgram(Guid trainingProgramId)
        {
            return trainingProgramRepository.FetchWeeksForTrainingProgram(trainingProgramId).ToList();
        }

        public TrainingProgramWeek FetchTrainingProgramWeek(TrainingProgramWeekDTO trainingProgramWeekDTO)
        {
            return trainingProgramRepository
                .FetchTrainingProgramWeek(trainingProgramWeekDTO.TrainingProgramId, trainingProgramWeekDTO.WeekName);
        }

        public TrainingProgramWeek UpdateTrainingProgramWeek(TrainingProgramWeekDTO trainingProgramWeekDTO)
        {
            var toUpdate = trainingProgramRepository.FetchTrainingProgramWeek(trainingProgramWeekDTO.TrainingProgramId, trainingProgramWeekDTO.OldWeekName);

            // Don't forget to add OldWeekName property to the DTO
            DeleteTrainingProgramWeek(trainingProgramWeekDTO); 

            toUpdate.WeekName = trainingProgramWeekDTO.WeekName;

            return trainingProgramRepository.InsertTrainingProgramWeek(toUpdate);
        }
    }
}
