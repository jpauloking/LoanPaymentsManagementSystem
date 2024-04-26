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

namespace WebApp.Pages.Loans
{
    public class EditModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public EditModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LoanEditViewModel LoanEditViewModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Loan == null)
            {
                return NotFound();
            }

            Loan loan = (await _context.Loan.FirstOrDefaultAsync(m => m.Id == id))!;
            if (loan == null)
            {
                return NotFound();
            }
            LoanEditViewModel = new()
            {
                Loan = new()
                {
                    Id = loan.Id,
                    DateOpened = loan.DateOpened,
                    LoanNumber = loan.LoanNumber,
                    Principal = loan.Principal,
                    DurationInDays = loan.DurationInDays,
                    InterestRate = loan.InterestRate,
                    IsOverDue = loan.IsOverDue,
                    DaysOverDueBy = loan.DaysOverDueBy,
                    NumberOfInstallments = loan.NumberOfInstallments,
                    PercentagePenaltyFeePerDayOverDue = loan.PercentagePenaltyFeePerDayOverDue,
                    IsPaid = loan.IsPaid,
                    DatePaid = loan.DatePaid,
                    PaidBy = loan.PaidBy,
                    IsScrappedOff = loan.IsScrappedOff,
                    PercentageScrappedOff = loan.PercentageScrappedOff,
                    DateScrappedOff = loan.DateScrappedOff,
                    ScrappedOffBy = loan.ScrappedOffBy,
                    IsWrittenOff = loan.IsWrittenOff,
                    DateWrittenOff = loan.DateWrittenOff,
                    WrittenOffBy = loan.WrittenOffBy,
                }
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Loan loan = (await _context.Loan.FirstOrDefaultAsync(m => m.Id == id))!;
            if (loan == null)
            {
                return NotFound();
            }

            loan.DateOpened = LoanEditViewModel.Loan.DateOpened;
            loan.Principal = LoanEditViewModel.Loan.Principal;
            loan.DurationInDays = LoanEditViewModel.Loan.DurationInDays;
            loan.InterestRate = LoanEditViewModel.Loan.InterestRate;
            loan.IsOverDue = LoanEditViewModel.Loan.IsOverDue;
            loan.DaysOverDueBy = LoanEditViewModel.Loan.DaysOverDueBy;
            loan.NumberOfInstallments = LoanEditViewModel.Loan.NumberOfInstallments;
            loan.PercentagePenaltyFeePerDayOverDue = LoanEditViewModel.Loan.PercentagePenaltyFeePerDayOverDue;
            loan.IsPaid = LoanEditViewModel.Loan.IsPaid;
            loan.DatePaid = LoanEditViewModel.Loan.DatePaid;
            loan.PaidBy = LoanEditViewModel.Loan.PaidBy;
            loan.IsScrappedOff = LoanEditViewModel.Loan.IsScrappedOff;
            loan.PercentageScrappedOff = LoanEditViewModel.Loan.PercentageScrappedOff;
            loan.DateScrappedOff = LoanEditViewModel.Loan.DateScrappedOff;
            loan.ScrappedOffBy = LoanEditViewModel.Loan.ScrappedOffBy;
            loan.IsWrittenOff = LoanEditViewModel.Loan.IsWrittenOff;
            loan.DateWrittenOff = LoanEditViewModel.Loan.DateWrittenOff;
            loan.WrittenOffBy = LoanEditViewModel.Loan.WrittenOffBy;

            _context.Attach(loan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanEditViewModelExists(LoanEditViewModel.Loan.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LoanEditViewModelExists(Guid id)
        {
            return (_context.Loan?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
