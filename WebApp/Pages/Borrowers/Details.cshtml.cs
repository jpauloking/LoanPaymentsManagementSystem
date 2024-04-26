using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Pages.Borrowers
{
    public class DetailsModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public DetailsModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        public BorrowerDetailsViewModel BorrowerDetailsViewModel { get; set; } = default!;
        public List<Installment> Installments { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Borrower == null)
            {
                return NotFound();
            }
            Borrower borrower = (await _context.Borrower
                .Include(borrower => borrower.Loans)
                .ThenInclude(loan => loan.Installments)
                .Include(borrower => borrower.CreditAnalyses)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id))!;
            if (borrower == null)
            {
                return NotFound();
            }
            else
            {
                BorrowerDetailsViewModel = new()
                {
                    Borrower = new()
                    {
                        Id = borrower.Id,
                        FirstName = borrower.FirstName,
                        LastName = borrower.LastName,
                        Email = borrower.Email,
                        Phone = borrower.Phone,
                        Address = borrower.Address,
                        Country = borrower.Country,
                        City = borrower.City,
                        County = borrower.County,
                        Subcounty = borrower.Subcounty,
                        Village = borrower.Village,
                        Occupation = borrower.Occupation,
                        Company = borrower.Company,
                        DisplayPictureURL = borrower.DisplayPictureURL,
                        IdentificationDocumentType = borrower.IdentificationDocumentType,
                        IdentificationDocumentNumber = borrower.IdentificationDocumentNumber,
                        IdentificationDocumentURL = borrower.IdentificationDocumentURL,
                        Loans = borrower.Loans.Select(loan => new LoanViewModel()
                        {
                            Id = loan.Id,
                            LoanNumber = loan.LoanNumber,
                            DateOpened = loan.DateOpened,
                            Principal = loan.Principal,
                            DurationInDays = loan.DurationInDays,
                            InterestRate = loan.InterestRate,
                            NumberOfInstallments = loan.NumberOfInstallments,
                            AmountPerInstallment = loan.AmountPerInstallment,
                            IsPaid = loan.IsPaid,
                            DatePaid = loan.DatePaid,
                            PaidBy = loan.PaidBy,
                            IsOverDue = loan.IsOverDue,
                            DaysOverDueBy = loan.DaysOverDueBy,
                            IsScrappedOff = loan.IsScrappedOff,
                            PercentageScrappedOff = loan.PercentageScrappedOff,
                            DateScrappedOff = loan.DateScrappedOff,
                            ScrappedOffBy = loan.ScrappedOffBy,
                            IsWrittenOff = loan.IsWrittenOff,
                            DateWrittenOff = loan.DateWrittenOff,
                            WrittenOffBy = loan.WrittenOffBy,
                            PercentagePenaltyFeePerDayOverDue = loan.PercentagePenaltyFeePerDayOverDue,
                        }).ToList(),
                        Installments = borrower.Loans.SelectMany(loan => loan.Installments)
                        .Select(installment => new InstallmentViewModel()
                        {
                            Id = installment.Id,
                            InstallmentNumber = installment.InstallmentNumber,
                            PaymentNumber = installment.PaymentNumber,
                            DateDue = installment.DateDue,
                            Amount = installment.Amount,
                            BalanceAfterInstallment = installment.BalanceAfterInstallment,
                            PercentagePenaltyFeePerDayOverDue = installment.PercentagePenaltyFeePerDayOverDue,
                            IsPaid = installment.IsPaid,
                            DatePaid = installment.DatePaid,
                            PaidBy = installment.PaidBy,
                            IsOverDue = installment.IsOverDue,
                            DaysOverDueBy = installment.DaysOverDueBy,
                            IsScrappedOff = installment.IsScrappedOff,
                            PercentageScrappedOff = installment.PercentageScrappedOff,
                            DateScrappedOff = installment.DateScrappedOff,
                            ScrappedOffBy = installment.ScrappedOffBy,
                            IsWrittenOff = installment.IsWrittenOff,
                            DateWrittenOff = installment.DateWrittenOff,
                            WrittenOffBy = installment.WrittenOffBy,
                            Loan = new LoanViewModel()
                            {
                                Id = installment.Loan.Id,
                                LoanNumber = installment.Loan.LoanNumber,
                                DateOpened = installment.Loan.DateOpened,
                                Principal = installment.Loan.Principal,
                                DurationInDays = installment.Loan.DurationInDays,
                                InterestRate = installment.Loan.InterestRate,
                                NumberOfInstallments = installment.Loan.NumberOfInstallments,
                                AmountPerInstallment = installment.Loan.AmountPerInstallment,
                                IsPaid = installment.Loan.IsPaid,
                                DatePaid = installment.Loan.DatePaid,
                                PaidBy = installment.Loan.PaidBy,
                                IsOverDue = installment.Loan.IsOverDue,
                                DaysOverDueBy = installment.Loan.DaysOverDueBy,
                                IsScrappedOff = installment.Loan.IsScrappedOff,
                                PercentageScrappedOff = installment.Loan.PercentageScrappedOff,
                                DateScrappedOff = installment.Loan.DateScrappedOff,
                                ScrappedOffBy = installment.Loan.ScrappedOffBy,
                                IsWrittenOff = installment.Loan.IsWrittenOff,
                                DateWrittenOff = installment.Loan.DateWrittenOff,
                                WrittenOffBy = installment.Loan.WrittenOffBy,
                                PercentagePenaltyFeePerDayOverDue = installment.Loan.PercentagePenaltyFeePerDayOverDue,
                            }
                        }).ToList(),
                        CreditAnalyses = borrower.CreditAnalyses
                        .Select(creditAnalysis => new CreditAnalysisViewModel()
                        {
                            Id = creditAnalysis.Id,
                            DateCreated = creditAnalysis.DateCreated,
                            AverageIncome = creditAnalysis.AverageIncome,
                            AvereageExpenditure = creditAnalysis.AvereageExpenditure,
                            AverageSavings = creditAnalysis.AverageSavings,
                            ExtraIncome = creditAnalysis.ExtraIncome,
                            LoanPrincipal = creditAnalysis.LoanPrincipal,
                            DurationInDays = creditAnalysis.DurationInDays,
                            InterestRate = creditAnalysis.InterestRate,
                            LoanAmount = creditAnalysis.LoanAmount,
                            Notes = creditAnalysis.Notes
                        }).ToList()
                    }
                };
            }
            return Page();
        }
    }
}
