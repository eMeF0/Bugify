using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bugify.API.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceTaskProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Progress",
                table: "Tasks");

            migrationBuilder.AddColumn<Guid>(
                name: "ProgressId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TaskProgresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskProgresses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TaskProgresses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("759c64cb-9639-4218-bd6f-e68718429075"), "NotStarted" },
                    { new Guid("852a80bb-a5d1-4ebd-8959-c115a0ddee68"), "Completed" },
                    { new Guid("86704552-8451-4916-8494-b0dfd3490f44"), "Cancelled" },
                    { new Guid("915ea79b-f8fa-4f1a-bf02-e9d73faf14a6"), "InProgress" },
                    { new Guid("c99a883d-51e3-4738-9ee6-c6ad53d73b37"), "OnHold" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProgressId",
                table: "Tasks",
                column: "ProgressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskProgresses_ProgressId",
                table: "Tasks",
                column: "ProgressId",
                principalTable: "TaskProgresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskProgresses_ProgressId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskProgresses");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ProgressId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ProgressId",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "Progress",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
