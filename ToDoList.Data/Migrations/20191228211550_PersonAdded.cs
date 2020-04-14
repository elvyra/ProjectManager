using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoList.Data.Migrations
{
    public partial class PersonAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedToEmail",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Tasks",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientCompany",
                table: "Projects",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectOwnerEmail",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScrumMasterEmail",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Projects",
                rowVersion: true,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: true),
                    Surname = table.Column<string>(maxLength: 60, nullable: true),
                    Education = table.Column<string>(maxLength: 255, nullable: true),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    Skills = table.Column<string>(maxLength: 255, nullable: true),
                    Notes = table.Column<string>(maxLength: 255, nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTeam",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTeam", x => new { x.Id, x.Email });
                    table.ForeignKey(
                        name: "FK_ProjectTeam_Person_Email",
                        column: x => x.Email,
                        principalTable: "Person",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTeam_Projects_Id",
                        column: x => x.Id,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedToEmail",
                table: "Tasks",
                column: "AssignedToEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectOwnerEmail",
                table: "Projects",
                column: "ProjectOwnerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ScrumMasterEmail",
                table: "Projects",
                column: "ScrumMasterEmail");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeam_Email",
                table: "ProjectTeam",
                column: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Person_ProjectOwnerEmail",
                table: "Projects",
                column: "ProjectOwnerEmail",
                principalTable: "Person",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Person_ScrumMasterEmail",
                table: "Projects",
                column: "ScrumMasterEmail",
                principalTable: "Person",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Person_AssignedToEmail",
                table: "Tasks",
                column: "AssignedToEmail",
                principalTable: "Person",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Person_ProjectOwnerEmail",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Person_ScrumMasterEmail",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Person_AssignedToEmail",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "ProjectTeam");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssignedToEmail",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectOwnerEmail",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ScrumMasterEmail",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AssignedToEmail",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ClientCompany",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectOwnerEmail",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ScrumMasterEmail",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Projects");
        }
    }
}
