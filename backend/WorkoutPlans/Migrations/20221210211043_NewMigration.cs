using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlans.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MuscleGroup = table.Column<int>(nullable: false),
                    VideoUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPrograms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingProgramWeeks",
                columns: table => new
                {
                    WeekName = table.Column<string>(nullable: false),
                    TrainingProgramId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingProgramWeeks", x => new { x.TrainingProgramId, x.WeekName });
                    table.ForeignKey(
                        name: "FK_TrainingProgramId",
                        column: x => x.TrainingProgramId,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutSessionsForExercises",
                columns: table => new
                {
                    SessionName = table.Column<string>(nullable: false),
                    ExerciseId = table.Column<Guid>(nullable: false),
                    WeekName = table.Column<string>(nullable: false),
                    TrainingProgramId = table.Column<Guid>(nullable: false),
                    Reps = table.Column<string>(nullable: true),
                    Sets = table.Column<string>(nullable: true),
                    RestTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutSessionsForExercises", x => new { x.ExerciseId, x.SessionName, x.WeekName, x.TrainingProgramId });
                    table.ForeignKey(
                        name: "FK_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TrainingProgramWeek",
                        columns: x => new { x.TrainingProgramId, x.WeekName },
                        principalTable: "TrainingProgramWeeks",
                        principalColumns: new[] { "TrainingProgramId", "WeekName" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessionsForExercises_TrainingProgramId_WeekName",
                table: "WorkoutSessionsForExercises",
                columns: new[] { "TrainingProgramId", "WeekName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutSessionsForExercises");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "TrainingProgramWeeks");

            migrationBuilder.DropTable(
                name: "TrainingPrograms");
        }
    }
}
