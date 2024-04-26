using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class ChangeRangeForPaercentagesToFloat_And_CreateDataTransferObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountScrappedOff",
                table: "Loan");

            migrationBuilder.AlterColumn<float>(
                name: "PercentagePenaltyFeePerDayOverDue",
                table: "Loan",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<float>(
                name: "PercentageScrappedOff",
                table: "Loan",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<float>(
                name: "PercentageScrappedOff",
                table: "Installment",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "PercentagePenaltyFeePerDayOverDue",
                table: "Installment",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentageScrappedOff",
                table: "Loan");

            migrationBuilder.AlterColumn<int>(
                name: "PercentagePenaltyFeePerDayOverDue",
                table: "Loan",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "AmountScrappedOff",
                table: "Loan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PercentageScrappedOff",
                table: "Installment",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "PercentagePenaltyFeePerDayOverDue",
                table: "Installment",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
