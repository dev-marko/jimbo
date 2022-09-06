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
        private DbSet<ExerciseForWorkoutSession> exerciseForWorkoutSessions;

        public ExerciseRepository(WorkoutPlansContext context)
        {
            this.context = context;
            this.exerciseForWorkoutSessions = context.Set<ExerciseForWorkoutSession>();
        }

        public ExerciseForWorkoutSession FetchExerciseForWorkoutSession(Guid exerciseId, string workoutSessionName)
        {
            return exerciseForWorkoutSessions
                .SingleOrDefault(e => (e.ExerciseId == exerciseId) && e.SessionName.Equals(workoutSessionName));
        }

        public ExerciseForWorkoutSession InsertExerciseForWorkoutSession(ExerciseForWorkoutSession exerciseForWorkoutSession)
        {
            var entity = exerciseForWorkoutSessions.Add(exerciseForWorkoutSession).Entity;
            context.SaveChanges();
            return entity;
        }
    }
}
