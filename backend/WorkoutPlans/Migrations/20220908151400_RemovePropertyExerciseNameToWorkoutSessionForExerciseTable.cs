using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlans.Migrations
{
    public partial class RemovePropertyExerciseNameToWorkoutSessionForExerciseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExerciseName",
                table: "WorkoutSessionsForExercises");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExerciseName",
                table: "WorkoutSessionsForExercises",
                type: "text",
                nullable: true);
        }
    }
}
