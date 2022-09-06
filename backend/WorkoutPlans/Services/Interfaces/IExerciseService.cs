using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.DTO;
using WorkoutPlans.Domain.Enumerations;
using WorkoutPlans.Domain.Models;
using WorkoutPlans.Domain.Relations;
using WorkoutPlans.Domain.ViewModels;

namespace WorkoutPlans.Services.Interfaces
{
    public interface IExerciseService
    {
        ExerciseVM FetchExerciseById(Guid exerciseId);
        List<ExerciseVM> FetchAllExercises();
        List<ExerciseVM> FetchAllExercisesByMuscleGroup(MuscleGroup muscleGroup);
        ExerciseForWorkoutSession FetchExerciseForWorkoutSession(ExerciseForWorkoutSessionDTO exerciseForWorkoutSessionDTO);
        ExerciseForWorkoutSession CreateExerciseForWorkoutSession(ExerciseForWorkoutSessionDTO exerciseForWorkoutSessionDTO);
        ExerciseVM CreateExercise(ExerciseDTO exerciseDTO);
        ExerciseVM UpdateExercise(Guid exerciseId, ExerciseDTO exerciseDTO);
        ExerciseVM DeleteExercise(Guid exerciseId);
        bool ExerciseExists(Guid exerciseId);
    }
}
