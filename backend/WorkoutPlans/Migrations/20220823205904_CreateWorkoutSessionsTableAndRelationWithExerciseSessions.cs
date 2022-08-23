using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlans.Migrations
{
    public partial class CreateWorkoutSessionsTableAndRelationWithExerciseSessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkoutSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseSessionsInWorkoutSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExerciseSessionId = table.Column<Guid>(nullable: false),
                    WorkoutSessionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSessionsInWorkoutSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseSessionsInWorkoutSessions_WorkoutSessions_ExerciseS~",
                        column: x => x.ExerciseSessionId,
                        principalTable: "WorkoutSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseSessionsInWorkoutSessions_ExerciseSessions_WorkoutS~",
                        column: x => x.WorkoutSessionId,
                        principalTable: "ExerciseSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSessionsInWorkoutSessions_ExerciseSessionId",
                table: "ExerciseSessionsInWorkoutSessions",
                column: "ExerciseSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSessionsInWorkoutSessions_WorkoutSessionId",
                table: "ExerciseSessionsInWorkoutSessions",
                column: "WorkoutSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseSessionsInWorkoutSessions");

            migrationBuilder.DropTable(
                name: "WorkoutSessions");
        }
    }
}
