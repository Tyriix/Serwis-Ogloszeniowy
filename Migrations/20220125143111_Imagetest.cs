using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SerwisOgloszeniowy.Migrations
{
    public partial class Imagetest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageTitle",
                table: "Auctions");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Auctions",
                type: "image",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Auctions",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "image",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageTitle",
                table: "Auctions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
