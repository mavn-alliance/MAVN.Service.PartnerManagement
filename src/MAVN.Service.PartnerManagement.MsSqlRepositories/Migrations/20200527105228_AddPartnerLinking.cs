using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories.Migrations
{
    public partial class AddPartnerLinking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_linked_partners_partner_id",
                schema: "partner_management",
                table: "linked_partners",
                column: "partner_id");

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
                name: "partner_linking_info",
                schema: "partner_management");
        }
    }
}
