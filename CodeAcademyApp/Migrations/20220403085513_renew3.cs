using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeAcademyApp.Migrations
{
    public partial class renew3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_EducationCategories_EducationCategoryId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "EduCategoryId",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "EducationCategoryId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_EducationCategories_EducationCategoryId",
                table: "Groups",
                column: "EducationCategoryId",
                principalTable: "EducationCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_EducationCategories_EducationCategoryId",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "EducationCategoryId",
                table: "Groups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EduCategoryId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_EducationCategories_EducationCategoryId",
                table: "Groups",
                column: "EducationCategoryId",
                principalTable: "EducationCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
