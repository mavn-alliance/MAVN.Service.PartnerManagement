using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories.Migrations
{
    public partial class AddCountryIso3CodeToLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "сountry_iso3_code",
                schema: "partner_management",
                table: "location",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_location_сountry_iso3_code",
                schema: "partner_management",
                table: "location",
                column: "сountry_iso3_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_location_сountry_iso3_code",
                schema: "partner_management",
                table: "location");

            migrationBuilder.DropColumn(
                name: "сountry_iso3_code",
                schema: "partner_management",
                table: "location");
        }
    }
}
