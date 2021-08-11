using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHub.Data.Migrations
{
    public partial class ModifyOfferEntityAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Projects_ProjectId",
                table: "Offers");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Projects_ProjectId",
                table: "Offers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Projects_ProjectId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Offers");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Projects_ProjectId",
                table: "Offers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
