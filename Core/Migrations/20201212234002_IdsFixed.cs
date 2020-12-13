using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class IdsFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Subjects_SubjectId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersSubjectsRelation_Subjects_SubjectId",
                table: "UsersSubjectsRelation");

            migrationBuilder.AlterColumn<long>(
                name: "SubjectId",
                table: "UsersSubjectsRelation",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SubjectId",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Subjects_SubjectId",
                table: "Appointments",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSubjectsRelation_Subjects_SubjectId",
                table: "UsersSubjectsRelation",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Subjects_SubjectId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersSubjectsRelation_Subjects_SubjectId",
                table: "UsersSubjectsRelation");

            migrationBuilder.AlterColumn<long>(
                name: "SubjectId",
                table: "UsersSubjectsRelation",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "SubjectId",
                table: "Appointments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Subjects_SubjectId",
                table: "Appointments",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSubjectsRelation_Subjects_SubjectId",
                table: "UsersSubjectsRelation",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
