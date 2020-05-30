using Microsoft.EntityFrameworkCore.Migrations;

namespace MysiePysieService.Migrations
{
    public partial class classesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_Classid",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Classid",
                table: "Students",
                newName: "classid");

            migrationBuilder.RenameIndex(
                name: "IX_Students_Classid",
                table: "Students",
                newName: "IX_Students_classid");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_classid",
                table: "Students",
                column: "classid",
                principalTable: "Classes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_classid",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "classid",
                table: "Students",
                newName: "Classid");

            migrationBuilder.RenameIndex(
                name: "IX_Students_classid",
                table: "Students",
                newName: "IX_Students_Classid");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_Classid",
                table: "Students",
                column: "Classid",
                principalTable: "Classes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
