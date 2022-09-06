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
        List<TrainingProgram> FetchAllTrainingPrograms();
        List<TrainingProgramWeek> FetchAllWeeksForTrainingProgram(Guid trainingProgramId);
        TrainingProgram FetchTrainingProgramById(Guid trainingProgramId);
        TrainingProgramWeek FetchTrainingProgramWeek(TrainingProgramWeekDTO trainingProgramWeekDTO);
        TrainingProgramWeek CreateTrainingProgramWeek(TrainingProgramWeekDTO trainingProgramWeekDTO);
        TrainingProgram CreateTrainingProgram(TrainingProgramDTO trainingProgramDTO);
        TrainingProgram UpdateTrainingProgram(Guid trainingProgramId, TrainingProgramDTO trainingProgramDTO);
        TrainingProgram DeleteTrainingProgram(Guid trainingProgramId);
        bool TrainingProgramExists(Guid trainingProgramId);
    }
}
