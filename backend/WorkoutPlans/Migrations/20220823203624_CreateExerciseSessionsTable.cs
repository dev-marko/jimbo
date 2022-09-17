using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlans.Migrations
{
    public partial class CreateExerciseSessionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExerciseSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExerciseId = table.Column<Guid>(nullable: true),
                    Sets = table.Column<string>(nullable: true),
                    Reps = table.Column<string>(nullable: true),
                    RestTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseSessions_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSessions_ExerciseId",
                table: "ExerciseSessions",
                column: "ExerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseSessions");
        }
    }
}
