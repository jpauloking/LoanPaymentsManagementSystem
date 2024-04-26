﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Data;

#nullable disable

namespace WebApp.Migrations
{
    [DbContext(typeof(WebAppContext))]
    [Migration("20231120105252_RemoveInterestPlan_And_RemoveInstallmentPlan_And_ChangeDoubleToDecimalsForCurrency")]
    partial class RemoveInterestPlan_And_RemoveInstallmentPlan_And_ChangeDoubleToDecimalsForCurrency
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApp.Models.Borrower", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("City")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Company")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("County")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DisplayPictureURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("IdentificationDocumentNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IdentificationDocumentType")
                        .HasColumnType("int");

                    b.Property<string>("IdentificationDocumentURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Occupation")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subcounty")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Village")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Borrower");
                });

            modelBuilder.Entity("WebApp.Models.Installment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<decimal>("BalanceAfterInstallment")
                        .HasColumnType("money");

                    b.Property<DateTime>("DateDue")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DatePaid")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateScrappedOff")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateWrittenOff")
                        .HasColumnType("datetime2");

                    b.Property<int>("DaysOverDueBy")
                        .HasColumnType("int");

                    b.Property<bool>("IsOverDue")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<bool>("IsScrappedOff")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWrittenOff")
                        .HasColumnType("bit");

                    b.Property<Guid>("LoanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("PaidBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PercentagePenaltyFeePerDayOverDue")
                        .HasColumnType("int");

                    b.Property<int>("PercentageScrappedOff")
                        .HasColumnType("int");

                    b.Property<string>("ScrappedOffBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("WrittenOffBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("LoanId");

                    b.ToTable("Installment");
                });

            modelBuilder.Entity("WebApp.Models.Loan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AmountPerInstallment")
                        .HasColumnType("money");

                    b.Property<int>("AmountScrappedOff")
                        .HasColumnType("int");

                    b.Property<Guid>("BorrowerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOpened")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DatePaid")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateScrappedOff")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateWrittenOff")
                        .HasColumnType("datetime2");

                    b.Property<int>("DaysOverDueBy")
                        .HasColumnType("int");

                    b.Property<int>("DurationInDays")
                        .HasColumnType("int");

                    b.Property<float>("InterestRate")
                        .HasColumnType("real");

                    b.Property<bool>("IsOverDue")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<bool>("IsScrappedOff")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWrittenOff")
                        .HasColumnType("bit");

                    b.Property<string>("LoanNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfInstallments")
                        .HasColumnType("int");

                    b.Property<string>("PaidBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PercentagePenaltyFeePerDayOverDue")
                        .HasColumnType("int");

                    b.Property<decimal>("Principal")
                        .HasColumnType("money");

                    b.Property<string>("ScrappedOffBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("WrittenOffBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("BorrowerId");

                    b.ToTable("Loan");
                });

            modelBuilder.Entity("WebApp.Models.Installment", b =>
                {
                    b.HasOne("WebApp.Models.Loan", "Loan")
                        .WithMany("Installments")
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Loan");
                });

            modelBuilder.Entity("WebApp.Models.Loan", b =>
                {
                    b.HasOne("WebApp.Models.Borrower", "Borrower")
                        .WithMany("Loans")
                        .HasForeignKey("BorrowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Borrower");
                });

            modelBuilder.Entity("WebApp.Models.Borrower", b =>
                {
                    b.Navigation("Loans");
                });

            modelBuilder.Entity("WebApp.Models.Loan", b =>
                {
                    b.Navigation("Installments");
                });
#pragma warning restore 612, 618
        }
    }
}
