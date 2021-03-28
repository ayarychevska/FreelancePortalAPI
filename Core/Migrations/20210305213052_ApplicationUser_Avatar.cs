using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class ApplicationUser_Avatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "ApplicationUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "ApplicationUsers");
        }
    }
}
