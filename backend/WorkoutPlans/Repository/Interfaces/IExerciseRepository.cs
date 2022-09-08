using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Relations;

namespace WorkoutPlans.Repository.Interfaces
{
    public interface IExerciseRepository
    {
        WorkoutSessionForExercise FetchWorkoutSessionForExercise(Guid exerciseId, string workoutSessionName);
        WorkoutSessionForExercise InsertWorkoutSessionForExercise(WorkoutSessionForExercise workoutSessionForExercise);
        WorkoutSessionForExercise DeleteWorkoutSessionForExercise(WorkoutSessionForExercise workoutSessionForExercise);
    }
}
