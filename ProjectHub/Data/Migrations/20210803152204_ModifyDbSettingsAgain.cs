using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHub.Data.Migrations
{
    public partial class ModifyDbSettingsAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_AspNetUsers_AuthorId",
                table: "Rates");

            migrationBuilder.DropForeignKey(
                name: "FK_Rates_AspNetUsers_RecipientId",
                table: "Rates");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_AspNetUsers_AuthorId",
                table: "Rates",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_AspNetUsers_RecipientId",
                table: "Rates",
                column: "RecipientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_AspNetUsers_AuthorId",
                table: "Rates");

            migrationBuilder.DropForeignKey(
                name: "FK_Rates_AspNetUsers_RecipientId",
                table: "Rates");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_AspNetUsers_AuthorId",
                table: "Rates",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_AspNetUsers_RecipientId",
                table: "Rates",
                column: "RecipientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
