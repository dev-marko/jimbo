using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlans.Migrations
{
    public partial class ForeignKeysWorkoutSessionWeek : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSessionsForExercises_TrainingProgramWeeks_TrainingPr~",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgramWeek",
                table: "WorkoutSessionsForExercises",
                columns: new[] { "TrainingProgramId", "Name" },
                principalTable: "TrainingProgramWeeks",
                principalColumns: new[] { "TrainingProgramId", "Name" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgramWeek",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSessionsForExercises_TrainingProgramWeeks_TrainingPr~",
                table: "WorkoutSessionsForExercises",
                columns: new[] { "TrainingProgramId", "Name" },
                principalTable: "TrainingProgramWeeks",
                principalColumns: new[] { "TrainingProgramId", "Name" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
