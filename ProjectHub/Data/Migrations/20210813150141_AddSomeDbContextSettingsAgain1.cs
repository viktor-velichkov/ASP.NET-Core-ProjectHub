using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHub.Data.Migrations
{
    public partial class AddSomeDbContextSettingsAgain1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDesigners_Designers_DesignerId",
                table: "ProjectDesigners");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDesigners_Projects_ProjectId",
                table: "ProjectDesigners");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDesigners_Designers_DesignerId",
                table: "ProjectDesigners",
                column: "DesignerId",
                principalTable: "Designers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDesigners_Projects_ProjectId",
                table: "ProjectDesigners",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDesigners_Designers_DesignerId",
                table: "ProjectDesigners");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDesigners_Projects_ProjectId",
                table: "ProjectDesigners");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDesigners_Designers_DesignerId",
                table: "ProjectDesigners",
                column: "DesignerId",
                principalTable: "Designers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDesigners_Projects_ProjectId",
                table: "ProjectDesigners",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
