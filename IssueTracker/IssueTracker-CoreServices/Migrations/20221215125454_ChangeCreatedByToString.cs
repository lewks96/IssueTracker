using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueTracker_CoreServices.Migrations
{
    public partial class ChangeCreatedByToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssuesDb_AspNetUsers_CreatedById",
                table: "IssuesDb");

            migrationBuilder.DropIndex(
                name: "IX_IssuesDb_CreatedById",
                table: "IssuesDb");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "IssuesDb");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "IssuesDb",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "IssuesDb");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "IssuesDb",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssuesDb_CreatedById",
                table: "IssuesDb",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_IssuesDb_AspNetUsers_CreatedById",
                table: "IssuesDb",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
