using Microsoft.EntityFrameworkCore.Migrations;

namespace Forum.Migrations
{
    public partial class RemovedCategoryPropertyFromTopic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Topics");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Topics",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
