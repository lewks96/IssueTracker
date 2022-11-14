using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueTracker_CoreServices.Migrations
{
    public partial class AddSev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Severity",
                table: "IssuesDb",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Severity",
                table: "IssuesDb");
        }
    }
}
