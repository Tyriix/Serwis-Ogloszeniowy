using Microsoft.EntityFrameworkCore.Migrations;

namespace SerwisOgloszeniowy.Migrations
{
    public partial class AuctionDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AspNetUsers_userId",
                table: "Auctions");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Auctions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CurrentUserId",
                table: "Auctions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AspNetUsers_userId",
                table: "Auctions",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AspNetUsers_userId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "CurrentUserId",
                table: "Auctions");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Auctions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AspNetUsers_userId",
                table: "Auctions",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
