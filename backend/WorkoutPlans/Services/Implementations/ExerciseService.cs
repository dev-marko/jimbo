using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.DTO;
using WorkoutPlans.Domain.Enumerations;
using WorkoutPlans.Domain.Models;
using WorkoutPlans.Domain.Relations;
using WorkoutPlans.Domain.ViewModels;
using WorkoutPlans.Repository.Interfaces;
using WorkoutPlans.Services.Interfaces;

namespace WorkoutPlans.Services.Implementations
{
    public class ExerciseService : IExerciseService
    {
        private readonly IRepository<Exercise> exerciseRepositoryGeneric;
        private readonly IExerciseRepository exerciseRepository;

        public ExerciseService(IRepository<Exercise> exerciseRepositoryGeneric, IExerciseRepository exerciseRepository)
        {
            this.exerciseRepositoryGeneric = exerciseRepositoryGeneric;
            this.exerciseRepository = exerciseRepository;
        }

        public ExerciseVM CreateExercise(ExerciseDTO exerciseDTO)
        {
            var exercise = new Exercise
            {
                Name = exerciseDTO.Name,
                Description = exerciseDTO.Description,
                MuscleGroup = exerciseDTO.MuscleGroup,
                VideoUrl = exerciseDTO.VideoUrl
            };

            exercise = exerciseRepositoryGeneric.Insert(exercise);

            return CreateExerciseViewModel(exercise);
        }

        private ExerciseVM CreateExerciseViewModel(Exercise exercise)
        {
            return new ExerciseVM
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Description = exercise.Description,
                MuscleGroup = Enum.GetName(typeof(MuscleGroup), exercise.MuscleGroup),
                VideoUrl = exercise.VideoUrl
            };
        }

        public ExerciseVM DeleteExercise(Guid exerciseId)
        {
            var exercise = exerciseRepositoryGeneric.FetchById(exerciseId);
            exerciseRepositoryGeneric.Delete(exercise);
            return CreateExerciseViewModel(exercise);
        }

        public bool ExerciseExists(Guid exerciseId)
        {
            return (exerciseId != null) && (exerciseRepositoryGeneric.FetchById(exerciseId) != null);
        }

        public List<ExerciseVM> FetchAllExercises()
        {
            return exerciseRepositoryGeneric
                .FetchAll()
                .Select(e => CreateExerciseViewModel(e))
                .ToList();
        }

        public List<ExerciseVM> FetchAllExercisesByMuscleGroup(MuscleGroup muscleGroup)
        {
            return exerciseRepositoryGeneric
                .FetchAll()
                .Where(e => e.MuscleGroup.Equals(muscleGroup))
                .Select(e => CreateExerciseViewModel(e))
                .ToList();
        }

        public ExerciseVM FetchExerciseById(Guid exerciseId)
        {
            var exercise = exerciseRepositoryGeneric.FetchById(exerciseId);
            return CreateExerciseViewModel(exercise);
        }

        public ExerciseVM UpdateExercise(Guid exerciseId, ExerciseDTO exerciseDTO)
        {
            var toUpdate = exerciseRepositoryGeneric.FetchById(exerciseId);

            toUpdate.Name = exerciseDTO.Name;
            toUpdate.Description = exerciseDTO.Description;
            toUpdate.MuscleGroup = exerciseDTO.MuscleGroup;
            toUpdate.VideoUrl = exerciseDTO.VideoUrl;

            toUpdate = exerciseRepositoryGeneric.Update(toUpdate);

            return CreateExerciseViewModel(toUpdate);
        }

        public ExerciseForWorkoutSession CreateExerciseForWorkoutSession(ExerciseForWorkoutSessionDTO exerciseForWorkoutSessionDTO)
        {
            var exerciseForWorkoutSession = new ExerciseForWorkoutSession
            {
                SessionName = exerciseForWorkoutSessionDTO.SessionName,
                Sets = exerciseForWorkoutSessionDTO.Sets,
                Reps = exerciseForWorkoutSessionDTO.Reps,
                RestTime = exerciseForWorkoutSessionDTO.RestTime,
                ExerciseId = exerciseForWorkoutSessionDTO.ExerciseId,
                Exercise = exerciseRepositoryGeneric.FetchById(exerciseForWorkoutSessionDTO.ExerciseId)
            };

            return exerciseRepository.InsertExerciseForWorkoutSession(exerciseForWorkoutSession);
        }

        public ExerciseForWorkoutSession FetchExerciseForWorkoutSession(ExerciseForWorkoutSessionDTO exerciseForWorkoutSessionDTO)
        {
            return exerciseRepository
                .FetchExerciseForWorkoutSession(exerciseForWorkoutSessionDTO.ExerciseId, exerciseForWorkoutSessionDTO.SessionName);
        }
    }
}
