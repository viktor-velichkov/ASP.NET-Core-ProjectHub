using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHub.Data.Migrations
{
    public partial class AddSomeDbContextSettingsAgain2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDesigners_Projects_ProjectId",
                table: "ProjectDesigners");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDesigners_Projects_ProjectId",
                table: "ProjectDesigners",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDesigners_Projects_ProjectId",
                table: "ProjectDesigners");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDesigners_Projects_ProjectId",
                table: "ProjectDesigners",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
