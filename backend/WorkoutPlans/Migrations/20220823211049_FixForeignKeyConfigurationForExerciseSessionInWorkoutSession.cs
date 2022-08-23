using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlans.Migrations
{
    public partial class FixForeignKeyConfigurationForExerciseSessionInWorkoutSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSessionsInWorkoutSessions_WorkoutSessions_ExerciseS~",
                table: "ExerciseSessionsInWorkoutSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSessionsInWorkoutSessions_ExerciseSessions_WorkoutS~",
                table: "ExerciseSessionsInWorkoutSessions");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSessionId",
                table: "ExerciseSessionsInWorkoutSessions",
                column: "ExerciseSessionId",
                principalTable: "ExerciseSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSessionId",
                table: "ExerciseSessionsInWorkoutSessions",
                column: "WorkoutSessionId",
                principalTable: "WorkoutSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSessionId",
                table: "ExerciseSessionsInWorkoutSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSessionId",
                table: "ExerciseSessionsInWorkoutSessions");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSessionsInWorkoutSessions_WorkoutSessions_ExerciseS~",
                table: "ExerciseSessionsInWorkoutSessions",
                column: "ExerciseSessionId",
                principalTable: "WorkoutSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSessionsInWorkoutSessions_ExerciseSessions_WorkoutS~",
                table: "ExerciseSessionsInWorkoutSessions",
                column: "WorkoutSessionId",
                principalTable: "ExerciseSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
