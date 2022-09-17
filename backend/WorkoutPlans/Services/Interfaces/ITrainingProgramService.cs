using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.DTO;
using WorkoutPlans.Domain.Models;
using WorkoutPlans.Domain.Relations;

namespace WorkoutPlans.Services.Interfaces
{
    public interface ITrainingProgramService
    {
        // training program
        List<TrainingProgram> FetchAllTrainingPrograms();
        TrainingProgram FetchTrainingProgramById(Guid trainingProgramId);
        TrainingProgram CreateTrainingProgram(TrainingProgramDTO trainingProgramDTO);
        TrainingProgram UpdateTrainingProgram(Guid trainingProgramId, TrainingProgramDTO trainingProgramDTO);
        TrainingProgram DeleteTrainingProgram(Guid trainingProgramId);
        bool TrainingProgramExists(Guid trainingProgramId);
        // training program week
        List<TrainingProgramWeek> FetchAllWeeksForTrainingProgram(Guid trainingProgramId);
        TrainingProgramWeek FetchTrainingProgramWeek(TrainingProgramWeekDTO trainingProgramWeekDTO);
        TrainingProgramWeek CreateTrainingProgramWeek(TrainingProgramWeekDTO trainingProgramWeekDTO);
        TrainingProgramWeek UpdateTrainingProgramWeek(TrainingProgramWeekDTO trainingProgramWeekDTO);
        TrainingProgramWeek DeleteTrainingProgramWeek(TrainingProgramWeekDTO trainingProgramWeekDTO);
    }
}
