using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class RemoveInterestPlan_And_RemoveInstallmentPlan_And_ChangeDoubleToDecimalsForCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loan_InstallmentPlan_InstallmentPlanId",
                table: "Loan");

            migrationBuilder.DropForeignKey(
                name: "FK_Loan_InterestPlan_InterestPlanId",
                table: "Loan");

            migrationBuilder.DropTable(
                name: "InstallmentPlan");

            migrationBuilder.DropTable(
                name: "InterestPlan");

            migrationBuilder.DropIndex(
                name: "IX_Loan_InstallmentPlanId",
                table: "Loan");

            migrationBuilder.DropIndex(
                name: "IX_Loan_InterestPlanId",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "InstallmentPlanId",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "InterestPlanId",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "PenaltyFeePerDayOverDue",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "AmountScrappedOff",
                table: "Installment");

            migrationBuilder.DropColumn(
                name: "PenaltyFeePerDayOverDue",
                table: "Installment");

            migrationBuilder.AlterColumn<decimal>(
                name: "Principal",
                table: "Loan",
                type: "money",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "AmountScrappedOff",
                table: "Loan",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountPerInstallment",
                table: "Loan",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AddColumn<int>(
                name: "PercentagePenaltyFeePerDayOverDue",
                table: "Loan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "BalanceAfterInstallment",
                table: "Installment",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Installment",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AddColumn<int>(
                name: "PercentagePenaltyFeePerDayOverDue",
                table: "Installment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PercentageScrappedOff",
                table: "Installment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentagePenaltyFeePerDayOverDue",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "PercentagePenaltyFeePerDayOverDue",
                table: "Installment");

            migrationBuilder.DropColumn(
                name: "PercentageScrappedOff",
                table: "Installment");

            migrationBuilder.AlterColumn<double>(
                name: "Principal",
                table: "Loan",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountScrappedOff",
                table: "Loan",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountPerInstallment",
                table: "Loan",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AddColumn<Guid>(
                name: "InstallmentPlanId",
                table: "Loan",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InterestPlanId",
                table: "Loan",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "PenaltyFeePerDayOverDue",
                table: "Loan",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "BalanceAfterInstallment",
                table: "Installment",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Installment",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AddColumn<decimal>(
                name: "AmountScrappedOff",
                table: "Installment",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PenaltyFeePerDayOverDue",
                table: "Installment",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "InstallmentPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DaysBetweenPayments = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterestPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DaysBetweenCharges = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestPlan", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loan_InstallmentPlanId",
                table: "Loan",
                column: "InstallmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_InterestPlanId",
                table: "Loan",
                column: "InterestPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_InstallmentPlan_InstallmentPlanId",
                table: "Loan",
                column: "InstallmentPlanId",
                principalTable: "InstallmentPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_InterestPlan_InterestPlanId",
                table: "Loan",
                column: "InterestPlanId",
                principalTable: "InterestPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
