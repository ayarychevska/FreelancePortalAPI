using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class ForeignKeyFixedPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Subjects_SubjectId1",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_SubjectId1",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SubjectId1",
                table: "Posts");

            migrationBuilder.AlterColumn<long>(
                name: "SubjectId",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SubjectId",
                table: "Posts",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Subjects_SubjectId",
                table: "Posts",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Subjects_SubjectId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_SubjectId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Posts",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "SubjectId1",
                table: "Posts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SubjectId1",
                table: "Posts",
                column: "SubjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Subjects_SubjectId1",
                table: "Posts",
                column: "SubjectId1",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
