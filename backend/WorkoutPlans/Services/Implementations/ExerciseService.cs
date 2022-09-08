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

        public WorkoutSessionForExercise CreateWorkoutSessionForExercise(WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO)
        {
            var exerciseForWorkoutSession = new WorkoutSessionForExercise
            {
                Name = workoutSessionForExerciseDTO.Name,
                Sets = workoutSessionForExerciseDTO.Sets,
                Reps = workoutSessionForExerciseDTO.Reps,
                RestTime = workoutSessionForExerciseDTO.RestTime,
                ExerciseId = workoutSessionForExerciseDTO.ExerciseId,
                Exercise = exerciseRepositoryGeneric.FetchById(workoutSessionForExerciseDTO.ExerciseId)
            };

            return exerciseRepository.InsertWorkoutSessionForExercise(exerciseForWorkoutSession);
        }

        public WorkoutSessionForExercise FetchWorkoutSessionForExercise(WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO)
        {
            return exerciseRepository
                .FetchWorkoutSessionForExercise(workoutSessionForExerciseDTO.ExerciseId, workoutSessionForExerciseDTO.Name);
        }

        public WorkoutSessionForExercise DeleteWorkoutSessionForExercise(WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO)
        {
            var toDelete = exerciseRepository.FetchWorkoutSessionForExercise(workoutSessionForExerciseDTO.OldExerciseId, workoutSessionForExerciseDTO.OldName);
            return exerciseRepository.DeleteWorkoutSessionForExercise(toDelete);
        }

        public bool WorkoutSessionForExerciseExists(WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO)
        {
            return exerciseRepository.FetchWorkoutSessionForExercise(workoutSessionForExerciseDTO.OldExerciseId, workoutSessionForExerciseDTO.OldName) != null;
        }

        public WorkoutSessionForExercise UpdateWorkoutSessionForExercise(WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO)
        {
            WorkoutSessionForExercise toUpdate = exerciseRepository.FetchWorkoutSessionForExercise(workoutSessionForExerciseDTO.OldExerciseId, workoutSessionForExerciseDTO.OldName);

            DeleteWorkoutSessionForExercise(workoutSessionForExerciseDTO);

            toUpdate.Name = workoutSessionForExerciseDTO.Name;
            toUpdate.Sets = workoutSessionForExerciseDTO.Sets;
            toUpdate.Reps = workoutSessionForExerciseDTO.Reps;
            toUpdate.RestTime = workoutSessionForExerciseDTO.RestTime;
            toUpdate.Exercise = exerciseRepositoryGeneric.FetchById(workoutSessionForExerciseDTO.ExerciseId);

            return exerciseRepository.InsertWorkoutSessionForExercise(toUpdate);
        }

        public List<WorkoutSessionForExercise> CreateWorkoutSessionsForListOfExercises(List<WorkoutSessionForExerciseDTO> workoutSessionForExerciseDTOs)
        {
            return workoutSessionForExerciseDTOs.Select(e => CreateWorkoutSessionForExercise(e)).ToList();
        }

        public List<WorkoutSessionForExercise> UpdateWorkoutSessionsForListOfExercises(List<WorkoutSessionForExerciseDTO> workoutSessionForExerciseDTOs)
        {
            // Don't forget to add OldExerciseId and OldName properties to DTOs
            workoutSessionForExerciseDTOs.ForEach(e => DeleteWorkoutSessionForExercise(e));
            return CreateWorkoutSessionsForListOfExercises(workoutSessionForExerciseDTOs);
        }
    }
}
