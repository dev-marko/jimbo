using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlans.Migrations
{
    public partial class ChangedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgramWeek",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutSessionsForExercises",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutSessionsForExercises_TrainingProgramId_Name",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingProgramWeeks",
                table: "TrainingProgramWeeks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TrainingProgramWeeks");

            migrationBuilder.AddColumn<string>(
                name: "SessionName",
                table: "WorkoutSessionsForExercises",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WeekName",
                table: "TrainingProgramWeeks",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutSessionsForExercises",
                table: "WorkoutSessionsForExercises",
                columns: new[] { "ExerciseId", "SessionName", "WeekName", "TrainingProgramId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingProgramWeeks",
                table: "TrainingProgramWeeks",
                columns: new[] { "TrainingProgramId", "WeekName" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessionsForExercises_TrainingProgramId_WeekName",
                table: "WorkoutSessionsForExercises",
                columns: new[] { "TrainingProgramId", "WeekName" });

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgramWeek",
                table: "WorkoutSessionsForExercises",
                columns: new[] { "TrainingProgramId", "WeekName" },
                principalTable: "TrainingProgramWeeks",
                principalColumns: new[] { "TrainingProgramId", "WeekName" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingProgramWeek",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutSessionsForExercises",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutSessionsForExercises_TrainingProgramId_WeekName",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingProgramWeeks",
                table: "TrainingProgramWeeks");

            migrationBuilder.DropColumn(
                name: "SessionName",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropColumn(
                name: "WeekName",
                table: "TrainingProgramWeeks");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WorkoutSessionsForExercises",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TrainingProgramWeeks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutSessionsForExercises",
                table: "WorkoutSessionsForExercises",
                columns: new[] { "ExerciseId", "Name", "WeekName", "TrainingProgramId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingProgramWeeks",
                table: "TrainingProgramWeeks",
                columns: new[] { "TrainingProgramId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessionsForExercises_TrainingProgramId_Name",
                table: "WorkoutSessionsForExercises",
                columns: new[] { "TrainingProgramId", "Name" });

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingProgramWeek",
                table: "WorkoutSessionsForExercises",
                columns: new[] { "TrainingProgramId", "Name" },
                principalTable: "TrainingProgramWeeks",
                principalColumns: new[] { "TrainingProgramId", "Name" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
