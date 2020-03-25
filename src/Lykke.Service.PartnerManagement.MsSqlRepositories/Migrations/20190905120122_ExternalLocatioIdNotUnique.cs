using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.PartnerManagement.MsSqlRepositories.Migrations
{
    public partial class ExternalLocatioIdNotUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_location_external_id",
                schema: "partner_management",
                table: "location");

            migrationBuilder.CreateIndex(
                name: "IX_location_external_id",
                schema: "partner_management",
                table: "location",
                column: "external_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_location_external_id",
                schema: "partner_management",
                table: "location");

            migrationBuilder.CreateIndex(
                name: "IX_location_external_id",
                schema: "partner_management",
                table: "location",
                column: "external_id",
                unique: true,
                filter: "[external_id] IS NOT NULL");
        }
    }
}
