using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHub.Data.Migrations
{
    public partial class AddSomeDbContextSettingsAgain4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDesigners_Projects_ProjectId",
                table: "ProjectDesigners");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Investors_InvestorId",
                table: "Projects");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDesigners_Projects_ProjectId",
                table: "ProjectDesigners",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Investors_InvestorId",
                table: "Projects",
                column: "InvestorId",
                principalTable: "Investors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDesigners_Projects_ProjectId",
                table: "ProjectDesigners");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Investors_InvestorId",
                table: "Projects");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDesigners_Projects_ProjectId",
                table: "ProjectDesigners",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
