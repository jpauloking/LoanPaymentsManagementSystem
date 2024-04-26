using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Pages.Borrowers
{
    public class CreditAnalysisModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public CreditAnalysisModel(WebAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(Guid borrowerId)
        {
            if (_context.Borrower == null)
            {
                return NotFound();
            }

            Borrower borrower = (await _context.Borrower
                .Include(borrower => borrower.Loans)
                .ThenInclude(loan => loan.Installments)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == borrowerId))!;

            if (borrower == null)
            {
                return NotFound();
            }
            else
            {
                CreditAnalysisCreateViewModel.CreditAnalysis = new()
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
                    }
                };
            }

            return Page();
        }

        [BindProperty]
        public CreditAnalysisCreateViewModel CreditAnalysisCreateViewModel { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public Guid BorrowerId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.CreditAnalysis is null || CreditAnalysisCreateViewModel is null)
            {
                return Page();
            }

            Borrower borrower = (await _context.Borrower.FirstOrDefaultAsync(borrower => borrower.Id == BorrowerId))!;
            if (borrower is null)
            {
                return Page();
            }

            CreditAnalysis creditAnalysis = new();
            creditAnalysis.AverageIncome = CreditAnalysisCreateViewModel.CreditAnalysis.AverageIncome;
            creditAnalysis.AvereageExpenditure = CreditAnalysisCreateViewModel.CreditAnalysis.AvereageExpenditure;
            creditAnalysis.AverageSavings = CreditAnalysisCreateViewModel.CreditAnalysis.AverageSavings;
            creditAnalysis.ExtraIncome = CreditAnalysisCreateViewModel.CreditAnalysis.ExtraIncome;
            creditAnalysis.LoanPrincipal = CreditAnalysisCreateViewModel.CreditAnalysis.LoanPrincipal;
            creditAnalysis.InterestRate = CreditAnalysisCreateViewModel.CreditAnalysis.InterestRate;
            creditAnalysis.DurationInDays = CreditAnalysisCreateViewModel.CreditAnalysis.DurationInDays;
            creditAnalysis.LoanAmount = CreditAnalysisCreateViewModel.CreditAnalysis.LoanAmount;
            creditAnalysis.Notes = CreditAnalysisCreateViewModel.CreditAnalysis.Notes;
            creditAnalysis.Borrower = borrower;

            _context.CreditAnalysis.Add(creditAnalysis);

            await _context.SaveChangesAsync();
            return RedirectToPage("Details", new { Id = BorrowerId });
        }

    }
}
