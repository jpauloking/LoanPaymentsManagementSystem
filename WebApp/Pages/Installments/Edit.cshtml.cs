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
    public class EditModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public EditModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InstallmentEditViewModel InstallmentEditViewModel { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? ReturnURL { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id, Guid? loanId)
        {
            if (id == null || _context.Installment == null)
            {
                return NotFound();
            }

            Installment installment = (await _context.Installment.FirstOrDefaultAsync(m => m.Id == id))!;
            if (installment == null)
            {
                return NotFound();
            }
            InstallmentEditViewModel = new()
            {
                Installment = new()
                {
                    Id = installment.Id,
                    InstallmentNumber = installment.InstallmentNumber,
                    PaymentNumber = installment.PaymentNumber,
                    Amount = installment.Amount,
                    DateDue = installment.DateDue,
                    BalanceAfterInstallment = installment.BalanceAfterInstallment,
                    IsOverDue = installment.IsOverDue,
                    DaysOverDueBy = installment.DaysOverDueBy,
                    PercentagePenaltyFeePerDayOverDue = installment.PercentagePenaltyFeePerDayOverDue,
                    IsPaid = installment.IsPaid,
                    DatePaid = installment.DatePaid,
                    PaidBy = installment.PaidBy,
                    IsScrappedOff = installment.IsScrappedOff,
                    PercentageScrappedOff = installment.PercentageScrappedOff,
                    DateScrappedOff = installment.DateScrappedOff,
                    ScrappedOffBy = installment.ScrappedOffBy,
                    IsWrittenOff = installment.IsWrittenOff,
                    DateWrittenOff = installment.DateWrittenOff,
                    WrittenOffBy = installment.WrittenOffBy,
                }
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id, Guid? loanId)
        {
            if (!ModelState.IsValid || _context.Installment == null)
            {
                return Page();
            }
            Installment installment = (await _context.Installment.FirstOrDefaultAsync(m => m.Id == id))!;
            if (installment == null)
            {
                return NotFound();
            }

            installment.InstallmentNumber = InstallmentEditViewModel.Installment.InstallmentNumber;
            installment.PaymentNumber = InstallmentEditViewModel.Installment.PaymentNumber;
            installment.Amount = InstallmentEditViewModel.Installment.Amount;
            installment.DateDue = InstallmentEditViewModel.Installment.DateDue;
            installment.BalanceAfterInstallment = InstallmentEditViewModel.Installment.BalanceAfterInstallment;
            installment.IsOverDue = InstallmentEditViewModel.Installment.IsOverDue;
            installment.DaysOverDueBy = InstallmentEditViewModel.Installment.DaysOverDueBy;
            installment.PercentagePenaltyFeePerDayOverDue = InstallmentEditViewModel.Installment.PercentagePenaltyFeePerDayOverDue;
            installment.IsPaid = InstallmentEditViewModel.Installment.IsPaid;
            installment.DatePaid = InstallmentEditViewModel.Installment.DatePaid;
            installment.PaidBy = InstallmentEditViewModel.Installment.PaidBy;
            installment.IsScrappedOff = InstallmentEditViewModel.Installment.IsScrappedOff;
            installment.PercentageScrappedOff = InstallmentEditViewModel.Installment.PercentageScrappedOff;
            installment.DateScrappedOff = InstallmentEditViewModel.Installment.DateScrappedOff;
            installment.ScrappedOffBy = InstallmentEditViewModel.Installment.ScrappedOffBy;
            installment.IsWrittenOff = InstallmentEditViewModel.Installment.IsWrittenOff;
            installment.DateWrittenOff = InstallmentEditViewModel.Installment.DateWrittenOff;
            installment.WrittenOffBy = InstallmentEditViewModel.Installment.WrittenOffBy;

            _context.Attach(installment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstallmentEditViewModelExists(InstallmentEditViewModel.Installment.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            if (!string.IsNullOrEmpty(ReturnURL) && Url.IsLocalUrl(ReturnURL))
            {
                return LocalRedirect(ReturnURL);
            }
            return RedirectToPage("./Index");
        }

        private bool InstallmentEditViewModelExists(Guid id)
        {
            return (_context.Installment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
