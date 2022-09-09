using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ITrainingProgramRepository trainingProgramRepository;
        private readonly IExerciseRepository exerciseRepository;

        public ExerciseService(IRepository<Exercise> exerciseRepositoryGeneric, ITrainingProgramRepository trainingProgramRepository, IExerciseRepository exerciseRepository)
        {
            this.exerciseRepositoryGeneric = exerciseRepositoryGeneric;
            this.trainingProgramRepository = trainingProgramRepository;
            this.exerciseRepository = exerciseRepository;
        }

        // EXERCISES
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


        // WORKOUT SESSION FOR EXERCISE
        public WorkoutSessionForExercise CreateWorkoutSessionForExercise(WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO)
        {
            var exerciseForWorkoutSession = new WorkoutSessionForExercise
            {
                SessionName = workoutSessionForExerciseDTO.SessionName,
                Sets = workoutSessionForExerciseDTO.Sets,
                Reps = workoutSessionForExerciseDTO.Reps,
                RestTime = workoutSessionForExerciseDTO.RestTime,
                ExerciseId = workoutSessionForExerciseDTO.ExerciseId,
                //Exercise = exerciseRepositoryGeneric.FetchById(workoutSessionForExerciseDTO.ExerciseId),
                WeekName = workoutSessionForExerciseDTO.WeekName,
                TrainingProgramId = workoutSessionForExerciseDTO.TrainingProgramId,
                TrainingProgramWeek = trainingProgramRepository.FetchTrainingProgramWeek(workoutSessionForExerciseDTO.TrainingProgramId, workoutSessionForExerciseDTO.WeekName)
            };

            return exerciseRepository.InsertWorkoutSessionForExercise(exerciseForWorkoutSession);
        }

        public WorkoutSessionForExercise FetchWorkoutSessionForExercise(WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO)
        {
            return exerciseRepository
                .FetchWorkoutSessionForExercise(
                workoutSessionForExerciseDTO.ExerciseId, 
                workoutSessionForExerciseDTO.SessionName,
                workoutSessionForExerciseDTO.WeekName,
                workoutSessionForExerciseDTO.TrainingProgramId);
        }

        public WorkoutSessionForExercise DeleteWorkoutSessionForExercise(WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO)
        {
            // Don't forget to add OldExerciseId, OldName, OldWeekName & TrainingProgramId properties to DTOs
            var toDelete = exerciseRepository.FetchWorkoutSessionForExercise(
                workoutSessionForExerciseDTO.OldExerciseId, 
                workoutSessionForExerciseDTO.OldSessionName, 
                workoutSessionForExerciseDTO.OldWeekName, 
                workoutSessionForExerciseDTO.TrainingProgramId);
            return exerciseRepository.DeleteWorkoutSessionForExercise(toDelete);
        }

        public bool WorkoutSessionForExerciseExists(WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO)
        {
            // Don't forget to add OldExerciseId, OldName, OldWeekName & TrainingProgramId properties to DTOs
            return exerciseRepository.FetchWorkoutSessionForExercise(
                workoutSessionForExerciseDTO.OldExerciseId, 
                workoutSessionForExerciseDTO.OldSessionName,
                workoutSessionForExerciseDTO.OldWeekName,
                workoutSessionForExerciseDTO.TrainingProgramId) != null;
        }

        public WorkoutSessionForExercise UpdateWorkoutSessionForExercise(WorkoutSessionForExerciseDTO workoutSessionForExerciseDTO)
        {
            // Don't forget to add OldExerciseId, OldName, OldWeekName & TrainingProgramId properties to DTOs
            WorkoutSessionForExercise toUpdate = exerciseRepository.FetchWorkoutSessionForExercise(
                workoutSessionForExerciseDTO.OldExerciseId, 
                workoutSessionForExerciseDTO.OldSessionName,
                workoutSessionForExerciseDTO.OldWeekName,
                workoutSessionForExerciseDTO.TrainingProgramId);

            DeleteWorkoutSessionForExercise(workoutSessionForExerciseDTO);

            toUpdate.SessionName = workoutSessionForExerciseDTO.SessionName;
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

        public List<WorkoutSessionForExercise> FetchWorkoutSessionsForWeek(TrainingProgramWeekDTO trainingProgramWeekDTO)
        {
            return exerciseRepository.FetchWorkoutSessionsForWeek(trainingProgramWeekDTO.WeekName, trainingProgramWeekDTO.TrainingProgramId).ToList();
        }

        public void DeleteMultipleWorkoutSessions(List<WorkoutSessionForExerciseDTO> workoutSessionForExerciseDTOs)
        {
            workoutSessionForExerciseDTOs.ForEach(e => DeleteWorkoutSessionForExercise(e));
        }
    }
}
