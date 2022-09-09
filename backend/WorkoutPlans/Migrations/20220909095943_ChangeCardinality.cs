using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlans.Migrations
{
    public partial class ChangeCardinality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionForWeeks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutSessionsForExercises",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.AddColumn<string>(
                name: "WeekName",
                table: "WorkoutSessionsForExercises",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingProgramId",
                table: "WorkoutSessionsForExercises",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutSessionsForExercises",
                table: "WorkoutSessionsForExercises",
                columns: new[] { "ExerciseId", "Name", "WeekName", "TrainingProgramId" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessionsForExercises_TrainingProgramId_Name",
                table: "WorkoutSessionsForExercises",
                columns: new[] { "TrainingProgramId", "Name" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSessionsForExercises_TrainingProgramWeeks_TrainingPr~",
                table: "WorkoutSessionsForExercises",
                columns: new[] { "TrainingProgramId", "Name" },
                principalTable: "TrainingProgramWeeks",
                principalColumns: new[] { "TrainingProgramId", "Name" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSessionsForExercises_TrainingProgramWeeks_TrainingPr~",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkoutSessionsForExercises",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutSessionsForExercises_TrainingProgramId_Name",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropColumn(
                name: "WeekName",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.DropColumn(
                name: "TrainingProgramId",
                table: "WorkoutSessionsForExercises");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkoutSessionsForExercises",
                table: "WorkoutSessionsForExercises",
                columns: new[] { "ExerciseId", "Name" });

            migrationBuilder.CreateTable(
                name: "SessionForWeeks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WeekName = table.Column<string>(type: "text", nullable: true),
                    WeekTrainingProgramId = table.Column<Guid>(type: "uuid", nullable: true),
                    WorkoutSessionExerciseId = table.Column<Guid>(type: "uuid", nullable: true),
                    WorkoutSessionName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionForWeeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionForWeeks_TrainingProgramWeeks_WeekTrainingProgramId_~",
                        columns: x => new { x.WeekTrainingProgramId, x.WeekName },
                        principalTable: "TrainingProgramWeeks",
                        principalColumns: new[] { "TrainingProgramId", "Name" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionForWeeks_WorkoutSessionsForExercises_WorkoutSessionE~",
                        columns: x => new { x.WorkoutSessionExerciseId, x.WorkoutSessionName },
                        principalTable: "WorkoutSessionsForExercises",
                        principalColumns: new[] { "ExerciseId", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionForWeeks_WeekTrainingProgramId_WeekName",
                table: "SessionForWeeks",
                columns: new[] { "WeekTrainingProgramId", "WeekName" });

            migrationBuilder.CreateIndex(
                name: "IX_SessionForWeeks_WorkoutSessionExerciseId_WorkoutSessionName",
                table: "SessionForWeeks",
                columns: new[] { "WorkoutSessionExerciseId", "WorkoutSessionName" });
        }
    }
}
