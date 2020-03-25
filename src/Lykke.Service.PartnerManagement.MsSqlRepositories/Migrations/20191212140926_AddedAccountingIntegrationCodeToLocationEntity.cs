using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.PartnerManagement.MsSqlRepositories.Migrations
{
    public partial class AddedAccountingIntegrationCodeToLocationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "accounting_integration_code",
                schema: "partner_management",
                table: "location",
                maxLength: 80,
                nullable: false,
                defaultValue: "000000");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accounting_integration_code",
                schema: "partner_management",
                table: "location");
        }
    }
}
