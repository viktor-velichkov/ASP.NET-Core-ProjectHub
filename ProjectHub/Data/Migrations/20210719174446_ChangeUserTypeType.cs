using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHub.Data.Migrations
{
    public partial class ChangeUserKindType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserKind",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserKindId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserKind",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKind", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserKindId",
                table: "AspNetUsers",
                column: "UserKindId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserKind_UserKindId",
                table: "AspNetUsers",
                column: "UserKindId",
                principalTable: "UserKind",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserKind_UserKindId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserKind");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserKindId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserKindId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserKind",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
