using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Relations;

namespace WorkoutPlans.Repository.Interfaces
{
    public interface IExerciseRepository
    {
        ExerciseForWorkoutSession FetchExerciseForWorkoutSession(Guid exerciseId, string workoutSessionName);
        ExerciseForWorkoutSession InsertExerciseForWorkoutSession(ExerciseForWorkoutSession exerciseForWorkoutSession);
    }
}
