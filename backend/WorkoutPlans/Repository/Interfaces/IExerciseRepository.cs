using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Relations;

namespace WorkoutPlans.Repository.Interfaces
{
    public interface IExerciseRepository
    {
        WorkoutSessionForExercise FetchWorkoutSessionForExercise(Guid exerciseId, string workoutSessionName, string weekName, Guid trainingProgramId);
        IEnumerable<WorkoutSessionForExercise> FetchWorkoutSessionsForWeek(string weekName, Guid trainingProgramId);
        WorkoutSessionForExercise InsertWorkoutSessionForExercise(WorkoutSessionForExercise workoutSessionForExercise);
        WorkoutSessionForExercise DeleteWorkoutSessionForExercise(WorkoutSessionForExercise workoutSessionForExercise);
    }
}
