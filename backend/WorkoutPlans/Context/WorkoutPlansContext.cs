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
        public virtual DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public virtual DbSet<TrainingProgramWeek> TrainingProgramWeeks { get; set; }
        public virtual DbSet<ExerciseForWorkoutSession> ExerciseForWorkoutSessions { get; set; }
        public virtual DbSet<SessionForWeek> SessionForWeeks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // === Primary keys ===
            modelBuilder.Entity<Exercise>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<TrainingProgram>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<SessionForWeek>().Property(e => e.Id).ValueGeneratedOnAdd();

            // === Primary keys for weak entities ===
            modelBuilder.Entity<TrainingProgramWeek>().HasKey(e => new { e.TrainingProgramId, e.Name });
            modelBuilder.Entity<ExerciseForWorkoutSession>().HasKey(e => new { e.ExerciseId, e.SessionName });

            // === Foreign Keys ===
            modelBuilder.Entity<TrainingProgramWeek>()
                .HasOne(e => e.TrainingProgram)
                .WithMany(e => e.Weeks)
                .HasForeignKey(e => e.TrainingProgramId)
                .HasConstraintName("FK_TrainingProgramId");

            modelBuilder.Entity<ExerciseForWorkoutSession>()
                .HasOne(e => e.Exercise)
                .WithMany(e => e.WorkoutSessions)
                .HasForeignKey(e => e.ExerciseId)
                .HasConstraintName("FK_ExerciseId");

        }
    }
}
