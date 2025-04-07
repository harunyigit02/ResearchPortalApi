using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectWebApi.Migrations
{
    public partial class AlterContentColumnToBytea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "Articles",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Articles");
        }
    }
}
