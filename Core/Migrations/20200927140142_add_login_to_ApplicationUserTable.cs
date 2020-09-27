using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class add_login_to_ApplicationUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "ApplicationUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "ApplicationUsers");
        }
    }
}
