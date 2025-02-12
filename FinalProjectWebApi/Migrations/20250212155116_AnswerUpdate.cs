using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectWebApi.Migrations
{
    public partial class AnswerUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParticipantId",
                table: "Answers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantId",
                table: "Answers");
        }
    }
}
