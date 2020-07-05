using Microsoft.EntityFrameworkCore.Migrations;

namespace Tester.Migrator.Migrations
{
    public partial class AddNumberOfQuestionsField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOver",
                schema: "report",
                table: "user_test",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfQuestions",
                schema: "app",
                table: "test",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOver",
                schema: "report",
                table: "user_test");

            migrationBuilder.DropColumn(
                name: "NumberOfQuestions",
                schema: "app",
                table: "test");
        }
    }
}
