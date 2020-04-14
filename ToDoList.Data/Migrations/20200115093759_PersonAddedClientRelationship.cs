using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoList.Data.Migrations
{
    public partial class PersonAddedClientRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ClientEmail",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientEmail",
                table: "Projects",
                column: "ClientEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Persons_ClientEmail",
                table: "Projects",
                column: "ClientEmail",
                principalTable: "Persons",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Persons_ClientEmail",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ClientEmail",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "ClientEmail",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
