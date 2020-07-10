using Microsoft.EntityFrameworkCore.Migrations;

namespace Tester.Migrator.Migrations
{
    public partial class AddFieldsInUserAnswerUserTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                schema: "report",
                table: "user_test",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RightAnswers",
                schema: "report",
                table: "user_test",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WrongAnswers",
                schema: "report",
                table: "user_test",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsRight",
                schema: "report",
                table: "user_answer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChecked",
                schema: "report",
                table: "user_test");

            migrationBuilder.DropColumn(
                name: "RightAnswers",
                schema: "report",
                table: "user_test");

            migrationBuilder.DropColumn(
                name: "WrongAnswers",
                schema: "report",
                table: "user_test");

            migrationBuilder.DropColumn(
                name: "IsRight",
                schema: "report",
                table: "user_answer");
        }
    }
}
