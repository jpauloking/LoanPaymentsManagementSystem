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
    public class DeleteModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public DeleteModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LoanDeleteViewModel LoanDeleteViewModel { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Loan == null)
            {
                return NotFound();
            }

            Loan loan = (await _context.Loan
                .Include(loan => loan.Borrower)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id))!;

            if (loan == null)
            {
                return NotFound();
            }
            else
            {
                LoanDeleteViewModel = new()
                {
                    Loan = new()
                    {
                        Id = loan.Id,
                        Borrower = new BorrowerViewModel()
                        {
                            Id = loan.Borrower.Id,
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
                        }
                    }
                };
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Loan == null)
            {
                return NotFound();
            }
            Loan loan = (await _context.Loan.FindAsync(id))!;

            if (loan != null)
            {
                _context.Loan.Remove(loan);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
