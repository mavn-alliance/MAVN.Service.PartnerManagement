using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories.Migrations
{
    public partial class AddIndexToGeohash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "geohash",
                schema: "partner_management",
                table: "location",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_location_geohash",
                schema: "partner_management",
                table: "location",
                column: "geohash");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_location_geohash",
                schema: "partner_management",
                table: "location");

            migrationBuilder.AlterColumn<string>(
                name: "geohash",
                schema: "partner_management",
                table: "location",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
