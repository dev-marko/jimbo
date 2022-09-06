using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlans.Migrations
{
    public partial class RemoveExerciseSessionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseSessionsInWorkoutSessions");

            migrationBuilder.DropTable(
                name: "ExerciseSessions");

            migrationBuilder.CreateTable(
                name: "ExercisesInWorkoutSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExerciseId = table.Column<Guid>(nullable: false),
                    WorkoutSessionId = table.Column<Guid>(nullable: false),
                    Sets = table.Column<string>(nullable: true),
                    Reps = table.Column<string>(nullable: true),
                    RestTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExercisesInWorkoutSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutSessionId",
                        column: x => x.WorkoutSessionId,
                        principalTable: "WorkoutSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesInWorkoutSessions_ExerciseId",
                table: "ExercisesInWorkoutSessions",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesInWorkoutSessions_WorkoutSessionId",
                table: "ExercisesInWorkoutSessions",
                column: "WorkoutSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExercisesInWorkoutSessions");

            migrationBuilder.CreateTable(
                name: "ExerciseSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uuid", nullable: true),
                    Reps = table.Column<string>(type: "text", nullable: true),
                    RestTime = table.Column<string>(type: "text", nullable: true),
                    Sets = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseSessions_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseSessionsInWorkoutSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkoutSessionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSessionsInWorkoutSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseSessionId",
                        column: x => x.ExerciseSessionId,
                        principalTable: "ExerciseSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutSessionId",
                        column: x => x.WorkoutSessionId,
                        principalTable: "WorkoutSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSessions_ExerciseId",
                table: "ExerciseSessions",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSessionsInWorkoutSessions_ExerciseSessionId",
                table: "ExerciseSessionsInWorkoutSessions",
                column: "ExerciseSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSessionsInWorkoutSessions_WorkoutSessionId",
                table: "ExerciseSessionsInWorkoutSessions",
                column: "WorkoutSessionId");
        }
    }
}
