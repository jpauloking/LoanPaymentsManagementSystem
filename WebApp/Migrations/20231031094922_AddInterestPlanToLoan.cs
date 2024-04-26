using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class AddInterestPlanToLoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InterestPlanId",
                table: "Loan",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Loan_InterestPlanId",
                table: "Loan",
                column: "InterestPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_InterestPlan_InterestPlanId",
                table: "Loan",
                column: "InterestPlanId",
                principalTable: "InterestPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loan_InterestPlan_InterestPlanId",
                table: "Loan");

            migrationBuilder.DropIndex(
                name: "IX_Loan_InterestPlanId",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "InterestPlanId",
                table: "Loan");
        }
    }
}
