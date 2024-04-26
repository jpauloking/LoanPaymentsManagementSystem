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

namespace WebApp.Pages.Loans
{
    public class DetailsModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public DetailsModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        public LoanDetailsViewModel LoanDetailsViewModel { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Loan == null)
            {
                return NotFound();
            }

            Loan loan = (await _context.Loan
                .Include(loan => loan.Borrower)
                .Include(loan => loan.Installments)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id))!;
            if (loan == null)
            {
                return NotFound();
            }
            else
            {
                LoanDetailsViewModel = new()
                {
                    Loan = new()
                    {
                        Id = loan.Id,
                        LoanNumber = loan.LoanNumber,
                        DateOpened = loan.DateOpened,
                        Principal = loan.Principal,
                        DurationInDays = loan.DurationInDays,
                        InterestRate = loan.InterestRate,
                        PercentagePenaltyFeePerDayOverDue = loan.PercentagePenaltyFeePerDayOverDue,
                        IsPaid = loan.IsPaid,
                        DatePaid = loan?.DatePaid,
                        PaidBy = loan?.PaidBy,
                        IsOverDue = loan!.IsOverDue,
                        DaysOverDueBy = loan.DaysOverDueBy,
                        IsScrappedOff = loan.IsScrappedOff,
                        DateScrappedOff = loan.DateScrappedOff,
                        PercentageScrappedOff = loan.PercentageScrappedOff,
                        ScrappedOffBy = loan?.ScrappedOffBy,
                        IsWrittenOff = loan!.IsWrittenOff,
                        DateWrittenOff = loan?.DateWrittenOff,
                        WrittenOffBy = loan?.WrittenOffBy,
                        Borrower = new BorrowerViewModel()
                        {
                            Id = loan!.Borrower.Id,
                            FirstName = loan.Borrower.FirstName,
                            LastName = loan.Borrower.LastName,
                            Email = loan.Borrower.Email,
                            Phone = loan.Borrower.Phone,
                            Address = loan.Borrower.Address,
                            Country = loan.Borrower.Country,
                            City = loan.Borrower.City,
                            County = loan.Borrower.County,
                            Subcounty = loan.Borrower.Subcounty,
                            Village = loan.Borrower.Village,
                            DisplayPictureURL = loan.Borrower.DisplayPictureURL,
                            Company = loan.Borrower.Company,
                            Occupation = loan.Borrower.Occupation,
                        },
                        Installments = loan.Installments
                        .Select(installment => new InstallmentViewModel()
                        {
                            Id = installment.Id,
                            InstallmentNumber = installment.InstallmentNumber,
                            PaymentNumber = installment.PaymentNumber,
                            DateDue = installment.DateDue,
                            Amount = installment.Amount,
                            BalanceAfterInstallment = installment.BalanceAfterInstallment,
                            PercentagePenaltyFeePerDayOverDue = installment.PercentagePenaltyFeePerDayOverDue,
                        }).ToList()
                    }
                };
            }
            return Page();
        }
    }
}
