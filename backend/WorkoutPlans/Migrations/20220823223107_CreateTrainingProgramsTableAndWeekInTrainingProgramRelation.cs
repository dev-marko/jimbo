using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkoutPlans.Migrations
{
    public partial class CreateTrainingProgramsTableAndWeekInTrainingProgramRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingPrograms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeeksInTrainingPrograms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WeekId = table.Column<Guid>(nullable: false),
                    TrainingProgramId = table.Column<Guid>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_WeeksInTrainingPrograms_TrainingProgramId",
                table: "WeeksInTrainingPrograms",
                column: "TrainingProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_WeeksInTrainingPrograms_WeekId",
                table: "WeeksInTrainingPrograms",
                column: "WeekId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeeksInTrainingPrograms");

            migrationBuilder.DropTable(
                name: "TrainingPrograms");
        }
    }
}
