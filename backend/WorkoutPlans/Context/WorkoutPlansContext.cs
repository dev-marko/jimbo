using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlans.Domain.Models;
using WorkoutPlans.Domain.Relations;

namespace WorkoutPlans.Context
{
    public class WorkoutPlansContext : DbContext
    {
        public WorkoutPlansContext(DbContextOptions<WorkoutPlansContext> options) : base(options)
        {
        }

        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<ExerciseSession> ExerciseSessions { get; set; }
        public virtual DbSet<WorkoutSession> WorkoutSessions { get; set; }
        public virtual DbSet<ExerciseSessionInWorkoutSession> ExerciseSessionsInWorkoutSessions { get; set; }
        public virtual DbSet<Week> Weeks { get; set; }
        public virtual DbSet<WorkoutSessionInWeek> WorkoutSessionsInWeeks { get; set; }
        public virtual DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public virtual DbSet<WeekInTrainingProgram> WeeksInTrainingPrograms { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // === Primary keys ===
            modelBuilder.Entity<Exercise>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ExerciseSession>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<WorkoutSession>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ExerciseSessionInWorkoutSession>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Week>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<WorkoutSessionInWeek>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<TrainingProgram>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<WeekInTrainingProgram>().Property(e => e.Id).ValueGeneratedOnAdd();

            // === Foreign Keys (one-to-one & one-to-many relations) ===
            modelBuilder.Entity<ExerciseSession>().HasOne(e => e.Exercise);

            // === Foreign Keys (many-to-many relations) ===

            // Foreign key to the Exercise for a Workout Session
            modelBuilder.Entity<ExerciseSessionInWorkoutSession>()
                .HasOne(e => e.ExerciseSession)
                .WithMany(e => e.Workouts)
                .HasForeignKey(e => e.ExerciseSessionId)
                .HasConstraintName("FK_ExerciseSessionId");

            // Foreign key to the Workout for an Exercise Session
            modelBuilder.Entity<ExerciseSessionInWorkoutSession>()
                .HasOne(e => e.WorkoutSession)
                .WithMany(e => e.Exercises)
                .HasForeignKey(e => e.WorkoutSessionId)
                .HasConstraintName("FK_WorkoutSessionId");

            // Foreign key to the Workout Session for a Week
            modelBuilder.Entity<WorkoutSessionInWeek>()
                .HasOne(e => e.WorkoutSession)
                .WithMany(e => e.Weeks)
                .HasForeignKey(e => e.WorkoutSessionId)
                .HasConstraintName("FK_WorkoutSessionId");

            // Foreign key to the Week for a Workout Session
            modelBuilder.Entity<WorkoutSessionInWeek>()
                .HasOne(e => e.Week)
                .WithMany(e => e.Workouts)
                .HasForeignKey(e => e.WeekId)
                .HasConstraintName("FK_WeekId");

            // Foreing key to the Week in a Training Program
            modelBuilder.Entity<WeekInTrainingProgram>()
                .HasOne(e => e.Week)
                .WithMany(e => e.TrainingPrograms)
                .HasForeignKey(e => e.WeekId)
                .HasConstraintName("FK_WeekId");

            // Foreign key to the Training Program in a Week
            modelBuilder.Entity<WeekInTrainingProgram>()
                .HasOne(e => e.TrainingProgram)
                .WithMany(e => e.Weeks)
                .HasForeignKey(e => e.TrainingProgramId)
                .HasConstraintName("FK_TrainingProgramId");

        }
    }
}
