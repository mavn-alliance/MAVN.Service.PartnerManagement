using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories.Migrations
{
    public partial class RemoveContactPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_location_contact_person_contact_person_id",
                schema: "partner_management",
                table: "location");

            migrationBuilder.DropTable(
                name: "contact_person",
                schema: "partner_management");

            migrationBuilder.DropIndex(
                name: "IX_location_contact_person_id",
                schema: "partner_management",
                table: "location");

            migrationBuilder.DropColumn(
                name: "contact_person_id",
                schema: "partner_management",
                table: "location");

            migrationBuilder.AlterColumn<string>(
                name: "client_id",
                schema: "partner_management",
                table: "partner",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_partner_business_vertical",
                schema: "partner_management",
                table: "partner");

            migrationBuilder.DropIndex(
                name: "IX_partner_client_id",
                schema: "partner_management",
                table: "partner");

            migrationBuilder.AlterColumn<string>(
                name: "client_id",
                schema: "partner_management",
                table: "partner",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "contact_person_id",
                schema: "partner_management",
                table: "location",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "contact_person",
                schema: "partner_management",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    created_by = table.Column<Guid>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_person", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_location_contact_person_id",
                schema: "partner_management",
                table: "location",
                column: "contact_person_id");

            migrationBuilder.AddForeignKey(
                name: "FK_location_contact_person_contact_person_id",
                schema: "partner_management",
                table: "location",
                column: "contact_person_id",
                principalSchema: "partner_management",
                principalTable: "contact_person",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
