using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories.Migrations
{
    public partial class VerticalEntityToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_partner_vertical_business_vertical_id",
                schema: "partner_management",
                table: "partner");

            migrationBuilder.DropTable(
                name: "vertical",
                schema: "partner_management");

            migrationBuilder.DropIndex(
                name: "IX_partner_business_vertical_id",
                schema: "partner_management",
                table: "partner");

            migrationBuilder.DropColumn(
                name: "business_vertical_id",
                schema: "partner_management",
                table: "partner");

            migrationBuilder.AddColumn<int>(
                name: "business_vertical",
                schema: "partner_management",
                table: "partner",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "business_vertical",
                schema: "partner_management",
                table: "partner");

            migrationBuilder.AddColumn<Guid>(
                name: "business_vertical_id",
                schema: "partner_management",
                table: "partner",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.CreateIndex(
                name: "IX_partner_business_vertical_id",
                schema: "partner_management",
                table: "partner",
                column: "business_vertical_id");

            migrationBuilder.AddForeignKey(
                name: "FK_partner_vertical_business_vertical_id",
                schema: "partner_management",
                table: "partner",
                column: "business_vertical_id",
                principalSchema: "partner_management",
                principalTable: "vertical",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
