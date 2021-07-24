using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHub.Data.Migrations
{
    public partial class AddUserKindDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserKind_UserKindId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserKind",
                table: "UserKind");

            migrationBuilder.RenameTable(
                name: "UserKind",
                newName: "UserKinds");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserKinds",
                table: "UserKinds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserKinds_UserKindId",
                table: "AspNetUsers",
                column: "UserKindId",
                principalTable: "UserKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserKinds_UserKindId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserKinds",
                table: "UserKinds");

            migrationBuilder.RenameTable(
                name: "UserKinds",
                newName: "UserKind");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserKind",
                table: "UserKind",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserKind_UserKindId",
                table: "AspNetUsers",
                column: "UserKindId",
                principalTable: "UserKind",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
