using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlans.Migrations
{
    public partial class CreateWeeksTableAndWorkoutSessionInWeekRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weeks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutSessionsInWeeks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WorkoutSessionId = table.Column<Guid>(nullable: false),
                    WeekId = table.Column<Guid>(nullable: false)
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
                name: "IX_WorkoutSessionsInWeeks_WeekId",
                table: "WorkoutSessionsInWeeks",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessionsInWeeks_WorkoutSessionId",
                table: "WorkoutSessionsInWeeks",
                column: "WorkoutSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSessionId",
                table: "ExerciseSessionsInWorkoutSessions");

            migrationBuilder.DropTable(
                name: "WorkoutSessionsInWeeks");

            migrationBuilder.DropTable(
                name: "Weeks");
        }
    }
}
