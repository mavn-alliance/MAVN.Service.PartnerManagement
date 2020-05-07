using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.PartnerManagement.MsSqlRepositories.Migrations
{
    public partial class MakeLongitudeAndLatitudeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "longitude",
                schema: "partner_management",
                table: "location",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "latitude",
                schema: "partner_management",
                table: "location",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "longitude",
                schema: "partner_management",
                table: "location",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "latitude",
                schema: "partner_management",
                table: "location",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
