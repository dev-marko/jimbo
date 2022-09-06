using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlans.Migrations
{
    public partial class NewERModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExercisesInWorkoutSessions");

            migrationBuilder.DropTable(
                name: "WeeksInTrainingPrograms");

            migrationBuilder.DropTable(
                name: "WorkoutSessionsInWeeks");

            migrationBuilder.DropTable(
                name: "Weeks");

            migrationBuilder.DropTable(
                name: "WorkoutSessions");

            migrationBuilder.CreateTable(
                name: "ExerciseForWorkoutSessions",
                columns: table => new
                {
                    SessionName = table.Column<string>(nullable: false),
                    ExerciseId = table.Column<Guid>(nullable: false),
                    Reps = table.Column<string>(nullable: true),
                    Sets = table.Column<string>(nullable: true),
                    RestTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseForWorkoutSessions", x => new { x.ExerciseId, x.SessionName });
                    table.ForeignKey(
                        name: "FK_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingProgramWeeks",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    TrainingProgramId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingProgramWeeks", x => new { x.TrainingProgramId, x.Name });
                    table.ForeignKey(
                        name: "FK_TrainingProgramId",
                        column: x => x.TrainingProgramId,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseForWorkoutSessions");

            migrationBuilder.DropTable(
                name: "TrainingProgramWeeks");

            migrationBuilder.CreateTable(
                name: "Weeks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeeksInTrainingPrograms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainingProgramId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeekId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeksInTrainingPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingProgramId",
                        column: x => x.TrainingProgramId,
                        principalTable: "TrainingPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeekId",
                        column: x => x.WeekId,
                        principalTable: "Weeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExercisesInWorkoutSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Reps = table.Column<string>(type: "text", nullable: true),
                    RestTime = table.Column<string>(type: "text", nullable: true),
                    Sets = table.Column<string>(type: "text", nullable: true),
                    WorkoutSessionId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "WorkoutSessionsInWeeks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WeekId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkoutSessionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutSessionsInWeeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeekId",
                        column: x => x.WeekId,
                        principalTable: "Weeks",
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

            migrationBuilder.CreateIndex(
                name: "IX_WeeksInTrainingPrograms_TrainingProgramId",
                table: "WeeksInTrainingPrograms",
                column: "TrainingProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_WeeksInTrainingPrograms_WeekId",
                table: "WeeksInTrainingPrograms",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessionsInWeeks_WeekId",
                table: "WorkoutSessionsInWeeks",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessionsInWeeks_WorkoutSessionId",
                table: "WorkoutSessionsInWeeks",
                column: "WorkoutSessionId");
        }
    }
}
