using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.PartnerManagement.MsSqlRepositories.Migrations
{
    public partial class GlobalConversionRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tokens_rate",
                schema: "partner_management",
                table: "partner",
                newName: "amount_in_tokens");

            migrationBuilder.RenameColumn(
                name: "currency_rate",
                schema: "partner_management",
                table: "partner",
                newName: "amount_in_currency");

            migrationBuilder.AlterColumn<string>(
                name: "amount_in_tokens",
                schema: "partner_management",
                table: "partner",
                type: "nvarchar(64)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "amount_in_currency",
                schema: "partner_management",
                table: "partner",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "use_global_currency_rate",
                schema: "partner_management",
                table: "partner",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "amount_in_tokens",
                schema: "partner_management",
                table: "partner",
                nullable: false,
                oldClrType: typeof(string),
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "amount_in_currency",
                schema: "partner_management",
                table: "partner",
                nullable: false,
                oldClrType: typeof(decimal),
                defaultValue: 0);

            migrationBuilder.RenameColumn(
                name: "amount_in_tokens",
                schema: "partner_management",
                table: "partner",
                newName: "tokens_rate");

            migrationBuilder.RenameColumn(
                name: "amount_in_currency",
                schema: "partner_management",
                table: "partner",
                newName: "currency_rate");

            migrationBuilder.DropColumn(
                name: "use_global_currency_rate",
                schema: "partner_management",
                table: "partner");
        }
    }
}
