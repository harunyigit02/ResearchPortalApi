using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectWebApi.Migrations
{
    public partial class AnswerUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users_UserId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_UserId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Answers");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ParticipantId",
                table: "Answers",
                column: "ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users_ParticipantId",
                table: "Answers",
                column: "ParticipantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users_ParticipantId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_ParticipantId",
                table: "Answers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Answers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserId",
                table: "Answers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users_UserId",
                table: "Answers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
