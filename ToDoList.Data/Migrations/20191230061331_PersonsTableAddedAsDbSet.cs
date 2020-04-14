using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoList.Data.Migrations
{
    public partial class PersonsTableAddedAsDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Person_ProjectOwnerEmail",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Person_ScrumMasterEmail",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeam_Person_Email",
                table: "ProjectTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Person_AssignedToEmail",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Persons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Persons_ProjectOwnerEmail",
                table: "Projects",
                column: "ProjectOwnerEmail",
                principalTable: "Persons",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Persons_ScrumMasterEmail",
                table: "Projects",
                column: "ScrumMasterEmail",
                principalTable: "Persons",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTeam_Persons_Email",
                table: "ProjectTeam",
                column: "Email",
                principalTable: "Persons",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Persons_AssignedToEmail",
                table: "Tasks",
                column: "AssignedToEmail",
                principalTable: "Persons",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Persons_ProjectOwnerEmail",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Persons_ScrumMasterEmail",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeam_Persons_Email",
                table: "ProjectTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Persons_AssignedToEmail",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "Person");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
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
                name: "FK_ProjectTeam_Person_Email",
                table: "ProjectTeam",
                column: "Email",
                principalTable: "Person",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Person_AssignedToEmail",
                table: "Tasks",
                column: "AssignedToEmail",
                principalTable: "Person",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
