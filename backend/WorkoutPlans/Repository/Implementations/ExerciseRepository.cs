using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Context;
using WorkoutPlans.Domain.Relations;
using WorkoutPlans.Repository.Interfaces;

namespace WorkoutPlans.Repository.Implementations
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly WorkoutPlansContext context;
        private DbSet<WorkoutSessionForExercise> workoutSessionsForExercises;

        public ExerciseRepository(WorkoutPlansContext context)
        {
            this.context = context;
            this.workoutSessionsForExercises = context.Set<WorkoutSessionForExercise>();
        }

        public WorkoutSessionForExercise FetchWorkoutSessionForExercise(Guid exerciseId, string workoutSessionName, string weekName, Guid trainingProgramId)
        {
            return workoutSessionsForExercises
                .SingleOrDefault(e => (e.ExerciseId == exerciseId) && (e.TrainingProgramId == trainingProgramId) && (e.SessionName.Equals(workoutSessionName)) && (e.WeekName.Equals(weekName)));
        }

        public WorkoutSessionForExercise InsertWorkoutSessionForExercise(WorkoutSessionForExercise workoutSessionForExercise)
        {
            var entity = workoutSessionsForExercises.Add(workoutSessionForExercise).Entity;
            context.SaveChanges();
            return entity;
        }

        public WorkoutSessionForExercise DeleteWorkoutSessionForExercise(WorkoutSessionForExercise workoutSessionForExercise)
        {
            var entity = workoutSessionsForExercises.Remove(workoutSessionForExercise).Entity;
            context.SaveChanges();
            return entity;
        }

        public IEnumerable<WorkoutSessionForExercise> FetchWorkoutSessionsForWeek(string weekName, Guid trainingProgramId)
        {
            return workoutSessionsForExercises
                .Where(e => e.WeekName.Equals(weekName) && (e.TrainingProgramId == trainingProgramId));
        }
    }
}
