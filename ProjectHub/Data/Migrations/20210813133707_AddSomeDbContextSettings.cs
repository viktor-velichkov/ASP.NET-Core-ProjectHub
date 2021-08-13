using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHub.Data.Migrations
{
    public partial class AddSomeDbContextSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Investors_InvestorId",
                table: "Projects");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Investors_InvestorId",
                table: "Projects",
                column: "InvestorId",
                principalTable: "Investors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Investors_InvestorId",
                table: "Projects");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Investors_InvestorId",
                table: "Projects",
                column: "InvestorId",
                principalTable: "Investors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
