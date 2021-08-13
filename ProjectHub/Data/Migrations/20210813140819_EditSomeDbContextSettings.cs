using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHub.Data.Migrations
{
    public partial class EditSomeDbContextSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Projects_ProjectId",
                table: "Offers");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Projects_ProjectId",
                table: "Offers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Projects_ProjectId",
                table: "Offers");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Projects_ProjectId",
                table: "Offers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
