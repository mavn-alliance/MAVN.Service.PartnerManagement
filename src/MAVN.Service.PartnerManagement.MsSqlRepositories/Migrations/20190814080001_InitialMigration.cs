using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "partner_management");

            migrationBuilder.CreateTable(
                name: "contact_person",
                schema: "partner_management",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_person", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vertical",
                schema: "partner_management",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vertical", x => x.id);
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
                    business_vertical_id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    client_id = table.Column<string>(nullable: true),
                    tokens_rate = table.Column<int>(nullable: false),
                    currency_rate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partner", x => x.id);
                    table.ForeignKey(
                        name: "FK_partner_vertical_business_vertical_id",
                        column: x => x.business_vertical_id,
                        principalSchema: "partner_management",
                        principalTable: "vertical",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    contact_person_id = table.Column<Guid>(nullable: false),
                    partner_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location", x => x.id);
                    table.ForeignKey(
                        name: "FK_location_contact_person_contact_person_id",
                        column: x => x.contact_person_id,
                        principalSchema: "partner_management",
                        principalTable: "contact_person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_location_partner_partner_id",
                        column: x => x.partner_id,
                        principalSchema: "partner_management",
                        principalTable: "partner",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_location_contact_person_id",
                schema: "partner_management",
                table: "location",
                column: "contact_person_id");

            migrationBuilder.CreateIndex(
                name: "IX_location_partner_id",
                schema: "partner_management",
                table: "location",
                column: "partner_id");

            migrationBuilder.CreateIndex(
                name: "IX_partner_business_vertical_id",
                schema: "partner_management",
                table: "partner",
                column: "business_vertical_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "location",
                schema: "partner_management");

            migrationBuilder.DropTable(
                name: "contact_person",
                schema: "partner_management");

            migrationBuilder.DropTable(
                name: "partner",
                schema: "partner_management");

            migrationBuilder.DropTable(
                name: "vertical",
                schema: "partner_management");
        }
    }
}
