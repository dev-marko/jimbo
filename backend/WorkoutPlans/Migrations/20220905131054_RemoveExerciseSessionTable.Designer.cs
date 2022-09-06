﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WorkoutPlans.Context;

namespace WorkoutPlans.Migrations
{
    [DbContext(typeof(WorkoutPlansContext))]
    [Migration("20220905131054_RemoveExerciseSessionTable")]
    partial class RemoveExerciseSessionTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("WorkoutPlans.Domain.Models.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("MuscleGroup")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("WorkoutPlans.Domain.Models.TrainingProgram", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TrainingPrograms");
                });

            modelBuilder.Entity("WorkoutPlans.Domain.Models.Week", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Weeks");
                });

            modelBuilder.Entity("WorkoutPlans.Domain.Models.WorkoutSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("WorkoutSessions");
                });

            modelBuilder.Entity("WorkoutPlans.Domain.Relations.ExerciseInWorkoutSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Reps")
                        .HasColumnType("text");

                    b.Property<string>("RestTime")
                        .HasColumnType("text");

                    b.Property<string>("Sets")
                        .HasColumnType("text");

                    b.Property<Guid>("WorkoutSessionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("WorkoutSessionId");

                    b.ToTable("ExercisesInWorkoutSessions");
                });

            modelBuilder.Entity("WorkoutPlans.Domain.Relations.WeekInTrainingProgram", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("TrainingProgramId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WeekId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TrainingProgramId");

                    b.HasIndex("WeekId");

                    b.ToTable("WeeksInTrainingPrograms");
                });

            modelBuilder.Entity("WorkoutPlans.Domain.Relations.WorkoutSessionInWeek", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("WeekId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WorkoutSessionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WeekId");

                    b.HasIndex("WorkoutSessionId");

                    b.ToTable("WorkoutSessionsInWeeks");
                });

            modelBuilder.Entity("WorkoutPlans.Domain.Relations.ExerciseInWorkoutSession", b =>
                {
                    b.HasOne("WorkoutPlans.Domain.Models.Exercise", "Exercise")
                        .WithMany("WorkoutSessions")
                        .HasForeignKey("ExerciseId")
                        .HasConstraintName("FK_ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutPlans.Domain.Models.WorkoutSession", "WorkoutSession")
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutSessionId")
                        .HasConstraintName("FK_WorkoutSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutPlans.Domain.Relations.WeekInTrainingProgram", b =>
                {
                    b.HasOne("WorkoutPlans.Domain.Models.TrainingProgram", "TrainingProgram")
                        .WithMany("Weeks")
                        .HasForeignKey("TrainingProgramId")
                        .HasConstraintName("FK_TrainingProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutPlans.Domain.Models.Week", "Week")
                        .WithMany("TrainingPrograms")
                        .HasForeignKey("WeekId")
                        .HasConstraintName("FK_WeekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WorkoutPlans.Domain.Relations.WorkoutSessionInWeek", b =>
                {
                    b.HasOne("WorkoutPlans.Domain.Models.Week", "Week")
                        .WithMany("Workouts")
                        .HasForeignKey("WeekId")
                        .HasConstraintName("FK_WeekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutPlans.Domain.Models.WorkoutSession", "WorkoutSession")
                        .WithMany("Weeks")
                        .HasForeignKey("WorkoutSessionId")
                        .HasConstraintName("FK_WorkoutSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
