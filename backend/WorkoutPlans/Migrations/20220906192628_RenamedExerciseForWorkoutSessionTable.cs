using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlans.Migrations
{
    public partial class RenamedExerciseForWorkoutSessionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionForWeeks_ExerciseForWorkoutSessions_ExerciseSessionE~",
                table: "SessionForWeeks");

            migrationBuilder.DropTable(
                name: "ExerciseForWorkoutSessions");

            migrationBuilder.DropIndex(
                name: "IX_SessionForWeeks_ExerciseSessionExerciseId_ExerciseSessionSe~",
                table: "SessionForWeeks");

            migrationBuilder.DropColumn(
                name: "ExerciseSessionExerciseId",
                table: "SessionForWeeks");

            migrationBuilder.DropColumn(
                name: "ExerciseSessionSessionName",
                table: "SessionForWeeks");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkoutSessionExerciseId",
                table: "SessionForWeeks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkoutSessionSessionName",
                table: "SessionForWeeks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkoutSessionsForExercises",
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
                    table.PrimaryKey("PK_WorkoutSessionsForExercises", x => new { x.ExerciseId, x.SessionName });
                    table.ForeignKey(
                        name: "FK_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionForWeeks_WorkoutSessionsForExercises_WorkoutSessionE~",
                table: "SessionForWeeks");

            migrationBuilder.DropTable(
                name: "WorkoutSessionsForExercises");

            migrationBuilder.DropIndex(
                name: "IX_SessionForWeeks_WorkoutSessionExerciseId_WorkoutSessionSess~",
                table: "SessionForWeeks");

            migrationBuilder.DropColumn(
                name: "WorkoutSessionExerciseId",
                table: "SessionForWeeks");

            migrationBuilder.DropColumn(
                name: "WorkoutSessionSessionName",
                table: "SessionForWeeks");

            migrationBuilder.AddColumn<Guid>(
                name: "ExerciseSessionExerciseId",
                table: "SessionForWeeks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExerciseSessionSessionName",
                table: "SessionForWeeks",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExerciseForWorkoutSessions",
                columns: table => new
                {
                    ExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionName = table.Column<string>(type: "text", nullable: false),
                    Reps = table.Column<string>(type: "text", nullable: true),
                    RestTime = table.Column<string>(type: "text", nullable: true),
                    Sets = table.Column<string>(type: "text", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_SessionForWeeks_ExerciseSessionExerciseId_ExerciseSessionSe~",
                table: "SessionForWeeks",
                columns: new[] { "ExerciseSessionExerciseId", "ExerciseSessionSessionName" });

            migrationBuilder.AddForeignKey(
                name: "FK_SessionForWeeks_ExerciseForWorkoutSessions_ExerciseSessionE~",
                table: "SessionForWeeks",
                columns: new[] { "ExerciseSessionExerciseId", "ExerciseSessionSessionName" },
                principalTable: "ExerciseForWorkoutSessions",
                principalColumns: new[] { "ExerciseId", "SessionName" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
