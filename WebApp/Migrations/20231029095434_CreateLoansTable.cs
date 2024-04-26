using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class CreateLoansTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOpened = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Principal = table.Column<double>(type: "float", nullable: false),
                    DurationInDays = table.Column<int>(type: "int", nullable: false),
                    InterestRate = table.Column<float>(type: "real", nullable: false),
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
                    table.PrimaryKey("PK_Loan", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loan");
        }
    }
}
