using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class CreateInstallmentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Installment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    DateDue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    BalanceAfterInstallment = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    PenaltyFeePerDayOverDue = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    PaidBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DatePaid = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsWrittenOff = table.Column<bool>(type: "bit", nullable: false),
                    WrittenOffBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateWrittenOff = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsScrappedOff = table.Column<bool>(type: "bit", nullable: false),
                    AmountScrappedOff = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    ScrappedOffBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateScrappedOff = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsOverDue = table.Column<bool>(type: "bit", nullable: false),
                    DaysOverDueBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installment", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Installment");
        }
    }
}
