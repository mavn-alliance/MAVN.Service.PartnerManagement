using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.PartnerManagement.MsSqlRepositories.Migrations
{
    public partial class ExternalLocatioIdUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "external_id",
                schema: "partner_management",
                table: "location",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_location_external_id",
                schema: "partner_management",
                table: "location",
                column: "external_id",
                unique: true,
                filter: "[external_id] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_location_external_id",
                schema: "partner_management",
                table: "location");

            migrationBuilder.AlterColumn<string>(
                name: "external_id",
                schema: "partner_management",
                table: "location",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
