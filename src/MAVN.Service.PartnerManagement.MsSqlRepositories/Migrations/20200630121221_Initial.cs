using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "partner_management");

            migrationBuilder.CreateTable(
                name: "linked_partners",
                schema: "partner_management",
                columns: table => new
                {
                    customer_id = table.Column<Guid>(nullable: false),
                    partner_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_linked_partners", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "partner",
                schema: "partner_management",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    business_vertical = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    client_id = table.Column<string>(nullable: true),
                    amount_in_tokens = table.Column<string>(nullable: true),
                    amount_in_currency = table.Column<decimal>(nullable: true),
                    use_global_currency_rate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partner", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "partner_linking_info",
                schema: "partner_management",
                columns: table => new
                {
                    partner_id = table.Column<Guid>(nullable: false),
                    partner_code = table.Column<string>(nullable: false),
                    partner_linking_code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partner_linking_info", x => x.partner_id);
                });

            migrationBuilder.CreateTable(
                name: "location",
                schema: "partner_management",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    partner_id = table.Column<Guid>(nullable: false),
                    external_id = table.Column<string>(nullable: true),
                    accounting_integration_code = table.Column<string>(maxLength: 80, nullable: false, defaultValue: "000000"),
                    longitude = table.Column<double>(nullable: true),
                    latitude = table.Column<double>(nullable: true),
                    geohash = table.Column<string>(nullable: true),
                    сountry_iso3_code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location", x => x.id);
                    table.ForeignKey(
                        name: "FK_location_partner_partner_id",
                        column: x => x.partner_id,
                        principalSchema: "partner_management",
                        principalTable: "partner",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_linked_partners_partner_id",
                schema: "partner_management",
                table: "linked_partners",
                column: "partner_id");

            migrationBuilder.CreateIndex(
                name: "IX_location_сountry_iso3_code",
                schema: "partner_management",
                table: "location",
                column: "сountry_iso3_code");

            migrationBuilder.CreateIndex(
                name: "IX_location_external_id",
                schema: "partner_management",
                table: "location",
                column: "external_id");

            migrationBuilder.CreateIndex(
                name: "IX_location_geohash",
                schema: "partner_management",
                table: "location",
                column: "geohash");

            migrationBuilder.CreateIndex(
                name: "IX_location_partner_id",
                schema: "partner_management",
                table: "location",
                column: "partner_id");

            migrationBuilder.CreateIndex(
                name: "IX_partner_business_vertical",
                schema: "partner_management",
                table: "partner",
                column: "business_vertical");

            migrationBuilder.CreateIndex(
                name: "IX_partner_client_id",
                schema: "partner_management",
                table: "partner",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_partner_linking_info_partner_code",
                schema: "partner_management",
                table: "partner_linking_info",
                column: "partner_code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "linked_partners",
                schema: "partner_management");

            migrationBuilder.DropTable(
                name: "location",
                schema: "partner_management");

            migrationBuilder.DropTable(
                name: "partner_linking_info",
                schema: "partner_management");

            migrationBuilder.DropTable(
                name: "partner",
                schema: "partner_management");
        }
    }
}
