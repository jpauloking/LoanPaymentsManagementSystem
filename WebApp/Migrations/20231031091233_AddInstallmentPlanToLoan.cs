using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class AddInstallmentPlanToLoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InstallmentPlanId",
                table: "Loan",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Loan_InstallmentPlanId",
                table: "Loan",
                column: "InstallmentPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loan_InstallmentPlan_InstallmentPlanId",
                table: "Loan",
                column: "InstallmentPlanId",
                principalTable: "InstallmentPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loan_InstallmentPlan_InstallmentPlanId",
                table: "Loan");

            migrationBuilder.DropIndex(
                name: "IX_Loan_InstallmentPlanId",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "InstallmentPlanId",
                table: "Loan");
        }
    }
}
