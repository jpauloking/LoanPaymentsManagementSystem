using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Pages.Installments
{
    public class CreateModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public CreateModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(Guid loanId)
        {
            if (!ModelState.IsValid || _context.Installment == null || InstallmentCreateViewModel == null)
            {
                return Page();
            }

            IQueryable<Installment> quereableInstallmentsForLoan = _context.Installment
                            .Where(installment => installment.Loan.Id == loanId);
            if (quereableInstallmentsForLoan.Any())
            {
                InstallmentCreateViewModel.Installment = new();
                InstallmentCreateViewModel.Installment.PaymentNumber = await quereableInstallmentsForLoan
                .Select(installment => installment.PaymentNumber).MaxAsync() + 1;
            }
            else
            {
                InstallmentCreateViewModel.Installment.PaymentNumber = 1;
            }
            InstallmentCreateViewModel.Installment.DateDue = DateTime.Today;
            InstallmentCreateViewModel.Installment.Loan = await _context.Loan.Select(loan => new LoanViewModel
            {
                Id = loan.Id,
                DateOpened = loan.DateOpened,
                Principal = loan.Principal,
                DurationInDays = loan.DurationInDays,
                InterestRate = loan.InterestRate,
                PercentagePenaltyFeePerDayOverDue = loan.PercentagePenaltyFeePerDayOverDue,
                IsPaid = loan.IsPaid,
                DatePaid = loan.DatePaid,
                PaidBy = loan.PaidBy,
                IsOverDue = loan.IsOverDue,
                DaysOverDueBy = loan.DaysOverDueBy,
                IsScrappedOff = loan.IsScrappedOff,
                DateScrappedOff = loan.DateScrappedOff,
                PercentageScrappedOff = loan.PercentageScrappedOff,
                ScrappedOffBy = loan.ScrappedOffBy,
                IsWrittenOff = loan.IsWrittenOff,
                DateWrittenOff = loan.DateWrittenOff,
                WrittenOffBy = loan.WrittenOffBy,
            }).FirstOrDefaultAsync(borrower => borrower.Id == LoanId);
            return Page();
        }

        [BindProperty]
        public InstallmentCreateViewModel InstallmentCreateViewModel { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public Guid? LoanId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Installment == null || InstallmentCreateViewModel == null)
            {
                return Page();
            }

            Loan loanToAddInstallmentTo = (await _context.Loan.FirstOrDefaultAsync(loan => loan.Id == LoanId))!;
            if (loanToAddInstallmentTo is not null)
            {
                _context.Installment.Add(new()
                {
                    Id = InstallmentCreateViewModel.Installment.Id,
                    InstallmentNumber = $"IN-{InstallmentCreateViewModel.Installment.Id.ToString()[..8]}",
                    PaymentNumber = InstallmentCreateViewModel.Installment.PaymentNumber,
                    DateDue = InstallmentCreateViewModel.Installment.DateDue,
                    Amount = InstallmentCreateViewModel.Installment.Amount,
                    BalanceAfterInstallment = InstallmentCreateViewModel.Installment.BalanceAfterInstallment,
                    PercentagePenaltyFeePerDayOverDue = InstallmentCreateViewModel.Installment.PercentagePenaltyFeePerDayOverDue,
                    Loan = loanToAddInstallmentTo
                });
                await _context.SaveChangesAsync();

                return RedirectToPage("/Loans/Details", new { Id = LoanId });
            }

            return Page();
        }
    }
}
