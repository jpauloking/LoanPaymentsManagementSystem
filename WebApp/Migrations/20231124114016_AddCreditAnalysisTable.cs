using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class AddCreditAnalysisTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditAnalysis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BorrowerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoanPrincipal = table.Column<decimal>(type: "money", nullable: false),
                    InterestRate = table.Column<float>(type: "real", nullable: false),
                    DurationInDays = table.Column<int>(type: "int", nullable: false),
                    LoanAmount = table.Column<decimal>(type: "money", nullable: false),
                    AverageIncome = table.Column<decimal>(type: "money", nullable: false),
                    AvereageExpenditure = table.Column<decimal>(type: "money", nullable: false),
                    AverageSavings = table.Column<decimal>(type: "money", nullable: false),
                    ExtraIncome = table.Column<decimal>(type: "money", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditAnalysis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditAnalysis_Borrower_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: "Borrower",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditAnalysis_BorrowerId",
                table: "CreditAnalysis",
                column: "BorrowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditAnalysis");
        }
    }
}
