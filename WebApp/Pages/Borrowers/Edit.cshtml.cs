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
using WebApp.Utilities.File;
using WebApp.ViewModels;

namespace WebApp.Pages.Borrowers
{
    public class EditModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;
        private readonly FileUploadUtility fileUploadUtility;
        private readonly FileRemoveUtility fileRemoveUtility;

        public EditModel(WebApp.Data.WebAppContext context, FileUploadUtility fileUploadUtility, FileRemoveUtility fileRemoveUtility)
        {
            _context = context;
            this.fileUploadUtility = fileUploadUtility;
            this.fileRemoveUtility = fileRemoveUtility;
        }

        [BindProperty]
        public BorrowerEditViewModel BorrowerEditViewModel { get; set; } = default!;

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

            BorrowerEditViewModel = new()
            {
                Borrower = new()
                {
                    Id = borrower.Id,
                    FirstName = borrower.FirstName,
                    LastName = borrower.LastName,
                    Email = borrower.Email,
                    Phone = borrower.Phone,
                    Address = borrower.Address,
                    IdentificationDocumentType = borrower.IdentificationDocumentType,
                    IdentificationDocumentNumber = borrower.IdentificationDocumentNumber,
                    IdentificationDocumentURL = borrower.IdentificationDocumentURL,
                    Country = borrower.Country,
                    City = borrower.City,
                    County = borrower.County,
                    Subcounty = borrower.Subcounty,
                    Village = borrower.Village,
                    Company = borrower.Company,
                    Occupation = borrower.Occupation,
                    DisplayPictureURL = borrower.DisplayPictureURL,
                }
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Borrower borrower = (await _context.Borrower
                .FirstOrDefaultAsync(borrower => borrower.Id == id))!;

            if (borrower is null)
            {
                return NotFound();
            }

            if (NewFileExists(BorrowerEditViewModel.DisplayPictureFile?.FileName))
            {
                UploadDisplayImageFile();
            }

            if (NewFileExists(BorrowerEditViewModel.IdentificationDocumentFile?.FileName))
            {
                UploadIdentitficationDocumentFile();
            }

            AssignViewModelToModel(borrower);

            _context.Attach(borrower).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowerEditViewModelExists(BorrowerEditViewModel.Borrower.Id))
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

        private void AssignViewModelToModel(Borrower borrower)
        {
            borrower.FirstName = BorrowerEditViewModel.Borrower.FirstName;
            borrower.LastName = BorrowerEditViewModel.Borrower.LastName;
            borrower.Email = BorrowerEditViewModel.Borrower.Email;
            borrower.Phone = BorrowerEditViewModel.Borrower.Phone;
            borrower.Address = BorrowerEditViewModel.Borrower.Address;
            borrower.Country = BorrowerEditViewModel.Borrower.Country;
            borrower.City = BorrowerEditViewModel.Borrower.City;
            borrower.County = BorrowerEditViewModel.Borrower.County;
            borrower.Subcounty = BorrowerEditViewModel.Borrower.Subcounty;
            borrower.Village = BorrowerEditViewModel.Borrower.Village;
            borrower.DisplayPictureURL = BorrowerEditViewModel.Borrower.DisplayPictureURL;
            borrower.IdentificationDocumentType = BorrowerEditViewModel.Borrower.IdentificationDocumentType;
            borrower.IdentificationDocumentNumber = BorrowerEditViewModel.Borrower.IdentificationDocumentNumber;
            borrower.IdentificationDocumentURL = BorrowerEditViewModel.Borrower.IdentificationDocumentURL;
            borrower.Occupation = BorrowerEditViewModel.Borrower.Occupation;
            borrower.Company = BorrowerEditViewModel.Borrower.Company;
        }

        private void UploadIdentitficationDocumentFile()
        {
            string destinationFolderName = Path.Combine("images", "documents");
            fileUploadUtility.DestinationFolderName = destinationFolderName;
            if (!string.IsNullOrEmpty(BorrowerEditViewModel.Borrower.IdentificationDocumentURL))
            {
                fileRemoveUtility.DestinationFolderName = destinationFolderName;
                fileRemoveUtility.RemoveFileFromDestinationFolder(BorrowerEditViewModel.Borrower.IdentificationDocumentURL);
            }
            string fileName = fileUploadUtility.UploadFile(BorrowerEditViewModel.IdentificationDocumentFile!);
            BorrowerEditViewModel.Borrower.IdentificationDocumentURL = fileName;
        }

        private void UploadDisplayImageFile()
        {
            string destinationFolderName = Path.Combine("images", "borrowers");
            fileUploadUtility.DestinationFolderName = destinationFolderName;
            if (!string.IsNullOrEmpty(BorrowerEditViewModel.Borrower.DisplayPictureURL))
            {
                fileRemoveUtility.DestinationFolderName = destinationFolderName;
                fileRemoveUtility.RemoveFileFromDestinationFolder(BorrowerEditViewModel.Borrower.DisplayPictureURL);
            }
            string fileName = fileUploadUtility.UploadFile(BorrowerEditViewModel.DisplayPictureFile!);
            BorrowerEditViewModel.Borrower.DisplayPictureURL = fileName;
        }

        private bool NewFileExists(string? fileName)
        {
            return !string.IsNullOrEmpty(fileName);
        }

        private bool BorrowerEditViewModelExists(Guid id)
        {
            return (_context.Borrower?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
