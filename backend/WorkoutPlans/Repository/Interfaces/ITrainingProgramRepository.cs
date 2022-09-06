using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Relations;

namespace WorkoutPlans.Repository.Interfaces
{
    public interface ITrainingProgramRepository
    {
        IEnumerable<TrainingProgramWeek> FetchWeeksForTrainingProgram(Guid trainingProgramId);
        TrainingProgramWeek FetchTrainingProgramWeek(Guid trainingProgramId, string weekName);
        TrainingProgramWeek InsertTrainingProgramWeek(TrainingProgramWeek trainingProgramWeek);
    }
}
