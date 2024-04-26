using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class AddLoanToInstallment_And_InstallmentsToLoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LoanId",
                table: "Installment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Installment_LoanId",
                table: "Installment",
                column: "LoanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Installment_Loan_LoanId",
                table: "Installment",
                column: "LoanId",
                principalTable: "Loan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installment_Loan_LoanId",
                table: "Installment");

            migrationBuilder.DropIndex(
                name: "IX_Installment_LoanId",
                table: "Installment");

            migrationBuilder.DropColumn(
                name: "LoanId",
                table: "Installment");
        }
    }
}
