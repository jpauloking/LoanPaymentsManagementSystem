using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class AddBorrowerToLoan_And_LoansToBorrower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BorrowerId",
                table: "Loan",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Loan_BorrowerId",
                table: "Loan",
                column: "BorrowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_Borrower_BorrowerId",
                table: "Loan",
                column: "BorrowerId",
                principalTable: "Borrower",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loan_Borrower_BorrowerId",
                table: "Loan");

            migrationBuilder.DropIndex(
                name: "IX_Loan_BorrowerId",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "BorrowerId",
                table: "Loan");
        }
    }
}
