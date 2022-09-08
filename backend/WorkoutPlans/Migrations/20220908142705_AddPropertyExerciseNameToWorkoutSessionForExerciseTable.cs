using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlans.Migrations
{
    public partial class AddPropertyExerciseNameToWorkoutSessionForExerciseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionForWeeks_WorkoutSessionsForExercises_WorkoutSessionE~",
                table: "SessionForWeeks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutSessionsForExercises",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropIndex(
                name: "IX_SessionForWeeks_WorkoutSessionExerciseId_WorkoutSessionSess~",
                table: "SessionForWeeks");

            migrationBuilder.DropColumn(
                name: "SessionName",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropColumn(
                name: "WorkoutSessionSessionName",
                table: "SessionForWeeks");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WorkoutSessionsForExercises",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExerciseName",
                table: "WorkoutSessionsForExercises",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkoutSessionName",
                table: "SessionForWeeks",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutSessionsForExercises",
                table: "WorkoutSessionsForExercises",
                columns: new[] { "ExerciseId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_SessionForWeeks_WorkoutSessionExerciseId_WorkoutSessionName",
                table: "SessionForWeeks",
                columns: new[] { "WorkoutSessionExerciseId", "WorkoutSessionName" });

            migrationBuilder.AddForeignKey(
                name: "FK_SessionForWeeks_WorkoutSessionsForExercises_WorkoutSessionE~",
                table: "SessionForWeeks",
                columns: new[] { "WorkoutSessionExerciseId", "WorkoutSessionName" },
                principalTable: "WorkoutSessionsForExercises",
                principalColumns: new[] { "ExerciseId", "Name" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionForWeeks_WorkoutSessionsForExercises_WorkoutSessionE~",
                table: "SessionForWeeks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutSessionsForExercises",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropIndex(
                name: "IX_SessionForWeeks_WorkoutSessionExerciseId_WorkoutSessionName",
                table: "SessionForWeeks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropColumn(
                name: "ExerciseName",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropColumn(
                name: "WorkoutSessionName",
                table: "SessionForWeeks");

            migrationBuilder.AddColumn<string>(
                name: "SessionName",
                table: "WorkoutSessionsForExercises",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkoutSessionSessionName",
                table: "SessionForWeeks",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutSessionsForExercises",
                table: "WorkoutSessionsForExercises",
                columns: new[] { "ExerciseId", "SessionName" });

            migrationBuilder.CreateIndex(
                name: "IX_SessionForWeeks_WorkoutSessionExerciseId_WorkoutSessionSess~",
                table: "SessionForWeeks",
                columns: new[] { "WorkoutSessionExerciseId", "WorkoutSessionSessionName" });

            migrationBuilder.AddForeignKey(
                name: "FK_SessionForWeeks_WorkoutSessionsForExercises_WorkoutSessionE~",
                table: "SessionForWeeks",
                columns: new[] { "WorkoutSessionExerciseId", "WorkoutSessionSessionName" },
                principalTable: "WorkoutSessionsForExercises",
                principalColumns: new[] { "ExerciseId", "SessionName" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
