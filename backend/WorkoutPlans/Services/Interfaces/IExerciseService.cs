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
        // exercises
        ExerciseVM FetchExerciseById(Guid exerciseId);
        List<ExerciseVM> FetchAllExercises();
        List<ExerciseVM> FetchAllExercisesByMuscleGroup(MuscleGroup muscleGroup);
        ExerciseVM CreateExercise(ExerciseDTO exerciseDTO);
        ExerciseVM UpdateExercise(Guid exerciseId, ExerciseDTO exerciseDTO);
        ExerciseVM DeleteExercise(Guid exerciseId);
        bool ExerciseExists(Guid exerciseId);

        // workout sessions
        WorkoutSessionForExercise FetchWorkoutSessionForExercise(WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO);
        WorkoutSessionForExercise CreateWorkoutSessionForExercise(WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO);
        List<WorkoutSessionForExercise> CreateWorkoutSessionsForListOfExercises(List<WorkoutSessionForExerciseDTO> workoutSessionForExerciseDTOs);
        WorkoutSessionForExercise UpdateWorkoutSessionForExercise(WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO);
        List<WorkoutSessionForExercise> UpdateWorkoutSessionsForListOfExercises(List<WorkoutSessionForExerciseDTO> workoutSessionForExerciseDTOs);
        WorkoutSessionForExercise DeleteWorkoutSessionForExercise(WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO);
        bool WorkoutSessionForExerciseExists(WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO);
    }
}
