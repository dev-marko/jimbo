using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlans.Migrations
{
    public partial class CreateSessionsForWeeksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SessionForWeeks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WeekTrainingProgramId = table.Column<Guid>(nullable: true),
                    WeekName = table.Column<string>(nullable: true),
                    ExerciseSessionExerciseId = table.Column<Guid>(nullable: true),
                    ExerciseSessionSessionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionForWeeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionForWeeks_ExerciseForWorkoutSessions_ExerciseSessionE~",
                        columns: x => new { x.ExerciseSessionExerciseId, x.ExerciseSessionSessionName },
                        principalTable: "ExerciseForWorkoutSessions",
                        principalColumns: new[] { "ExerciseId", "SessionName" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionForWeeks_TrainingProgramWeeks_WeekTrainingProgramId_~",
                        columns: x => new { x.WeekTrainingProgramId, x.WeekName },
                        principalTable: "TrainingProgramWeeks",
                        principalColumns: new[] { "TrainingProgramId", "Name" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionForWeeks_ExerciseSessionExerciseId_ExerciseSessionSe~",
                table: "SessionForWeeks",
                columns: new[] { "ExerciseSessionExerciseId", "ExerciseSessionSessionName" });

            migrationBuilder.CreateIndex(
                name: "IX_SessionForWeeks_WeekTrainingProgramId_WeekName",
                table: "SessionForWeeks",
                columns: new[] { "WeekTrainingProgramId", "WeekName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionForWeeks");
        }
    }
}
