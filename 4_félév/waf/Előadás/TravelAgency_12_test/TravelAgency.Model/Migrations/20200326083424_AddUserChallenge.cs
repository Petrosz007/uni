using Microsoft.EntityFrameworkCore.Migrations;

namespace ELTE.TravelAgency.Service.Migrations
{
    public partial class AddUserChallenge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserChallenge",
                table: "Guests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserChallenge",
                table: "Guests");
        }
    }
}
