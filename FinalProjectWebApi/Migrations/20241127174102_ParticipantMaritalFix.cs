using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectWebApi.Migrations
{
    public partial class ParticipantMaritalFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MartialStatus",
                table: "ParticipantInfos",
                newName: "MaritalStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaritalStatus",
                table: "ParticipantInfos",
                newName: "MartialStatus");
        }
    }
}
