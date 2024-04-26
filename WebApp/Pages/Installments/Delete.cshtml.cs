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

namespace WebApp.Pages.Installments
{
    public class DeleteModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public DeleteModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InstallmentDeleteViewModel InstallmentDeleteViewModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Installment == null)
            {
                return NotFound();
            }

            Installment installment = (await _context.Installment
                .AsNoTracking()
                .Include(installment => installment.Loan)
                .ThenInclude(loan => loan.Borrower)
                .FirstOrDefaultAsync(m => m.Id == id))!;

            if (installment == null)
            {
                return NotFound();
            }
            else
            {
                InstallmentDeleteViewModel = new()
                {
                    Installment = new()
                    {
                        Id = installment.Id,
                        InstallmentNumber = installment.InstallmentNumber,
                        PaymentNumber = installment.PaymentNumber,
                        Amount = installment.Amount,
                        Loan = new LoanViewModel()
                        {
                            Id = installment.Loan.Id,
                            DateOpened = installment.Loan.DateOpened,
                            Principal = installment.Loan.Principal,
                            DurationInDays = installment.Loan.DurationInDays,
                            InterestRate = installment.Loan.InterestRate,
                            PercentagePenaltyFeePerDayOverDue = installment.Loan.PercentagePenaltyFeePerDayOverDue,
                            IsPaid = installment.Loan.IsPaid,
                            DatePaid = installment.Loan.DatePaid,
                            PaidBy = installment.Loan.PaidBy,
                            IsOverDue = installment.Loan.IsOverDue,
                            DaysOverDueBy = installment.Loan.DaysOverDueBy,
                            IsScrappedOff = installment.Loan.IsScrappedOff,
                            DateScrappedOff = installment.Loan.DateScrappedOff,
                            PercentageScrappedOff = installment.Loan.PercentageScrappedOff,
                            ScrappedOffBy = installment.Loan.ScrappedOffBy,
                            IsWrittenOff = installment.Loan.IsWrittenOff,
                            DateWrittenOff = installment.Loan.DateWrittenOff,
                            WrittenOffBy = installment.Loan.WrittenOffBy,
                            Borrower = new BorrowerViewModel()
                            {
                                Id = installment.Loan.Borrower.Id,
                                FirstName = installment.Loan.Borrower.FirstName,
                                LastName = installment.Loan.Borrower.LastName,
                                Email = installment.Loan.Borrower.Email,
                                Phone = installment.Loan.Borrower.Phone,
                                Address = installment.Loan.Borrower.Address,
                                Country = installment.Loan.Borrower.Country,
                                City = installment.Loan.Borrower.City,
                                County = installment.Loan.Borrower.County,
                                Subcounty = installment.Loan.Borrower.Subcounty,
                                Village = installment.Loan.Borrower.Village,
                                DisplayPictureURL = installment.Loan.Borrower.DisplayPictureURL,
                                Company = installment.Loan.Borrower.Company,
                                Occupation = installment.Loan.Borrower.Occupation,
                            },
                        },
                    }
                };
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Installment == null)
            {
                return NotFound();
            }
            Installment installment = (await _context.Installment.FindAsync(id))!;

            if (installment != null)
            {
                _context.Installment.Remove(installment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
