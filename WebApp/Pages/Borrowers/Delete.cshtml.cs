using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Utilities.File;
using WebApp.ViewModels;

namespace WebApp.Pages.Borrowers
{
    public class DeleteModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;
        private readonly FileRemoveUtility fileRemoveUtility;

        public DeleteModel(WebApp.Data.WebAppContext context, FileRemoveUtility fileRemoveUtility)
        {
            _context = context;
            this.fileRemoveUtility = fileRemoveUtility;
        }

        [BindProperty]
        public BorrowerDeleteViewModel BorrowerDeleteViewModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Borrower == null)
            {
                return NotFound();
            }

            Borrower borrower = (await _context.Borrower.FirstOrDefaultAsync(m => m.Id == id))!;

            if (borrower == null)
            {
                return NotFound();
            }
            else
            {
                BorrowerDeleteViewModel = new()
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
                        DisplayPictureURL = borrower.DisplayPictureURL,
                        Company = borrower.Company,
                        Occupation = borrower.Occupation,
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
                        }).ToList()
                    }
                };
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Borrower == null)
            {
                return NotFound();
            }
            Borrower borrower = (await _context.Borrower.FindAsync(id))!;

            if (borrower != null)
            {

                if (!string.IsNullOrEmpty(borrower.DisplayPictureURL))
                {
                    string destinationFolderName = Path.Combine("images", "borrowers");
                    fileRemoveUtility.DestinationFolderName = destinationFolderName;
                    fileRemoveUtility.RemoveFileFromDestinationFolder(borrower.DisplayPictureURL);
                }

                if (!string.IsNullOrEmpty(borrower.IdentificationDocumentURL))
                {
                    string destinationFolderName = Path.Combine("images", "documents");
                    fileRemoveUtility.DestinationFolderName = destinationFolderName;
                    fileRemoveUtility.RemoveFileFromDestinationFolder(borrower.IdentificationDocumentURL);
                }

                _context.Borrower.Remove(borrower);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

    }
}
