using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Data;
using WebApp.Utilities.File;
using WebApp.ViewModels;

namespace WebApp.Pages.Borrowers
{
    public class CreateModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;
        private readonly FileUploadUtility fileUploadUtility;

        public CreateModel(WebApp.Data.WebAppContext context, FileUploadUtility fileUploadUtility)
        {
            _context = context;
            this.fileUploadUtility = fileUploadUtility;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BorrowerCreateViewModel BorrowerCreateViewModel { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? ReturnURL { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Borrower == null || BorrowerCreateViewModel == null)
            {
                return Page();
            }

            if (BorrowerCreateViewModel?.IdentificationDocumentFile is not null && NewFileExists(BorrowerCreateViewModel.DisplayPictureFile!.FileName))
            {
                fileUploadUtility.DestinationFolderName = Path.Combine("images", "borrowers");
                string fileName = fileUploadUtility.UploadFile(BorrowerCreateViewModel.DisplayPictureFile!);
                BorrowerCreateViewModel.Borrower.DisplayPictureURL = fileName;
            }

            if (BorrowerCreateViewModel?.IdentificationDocumentFile is not null && NewFileExists(BorrowerCreateViewModel.IdentificationDocumentFile!.FileName))
            {
                fileUploadUtility.DestinationFolderName = Path.Combine("images", "documents");
                string fileName = fileUploadUtility.UploadFile(BorrowerCreateViewModel.IdentificationDocumentFile!);
                BorrowerCreateViewModel.Borrower.IdentificationDocumentURL = fileName;
            }

            _context.Borrower.Add(new()
            {
                BorrowerNumber = $"BO-{BorrowerCreateViewModel!.Borrower.Id.ToString()[..8]}",
                FirstName = BorrowerCreateViewModel.Borrower.FirstName,
                LastName = BorrowerCreateViewModel.Borrower.LastName,
                IdentificationDocumentType = BorrowerCreateViewModel.Borrower.IdentificationDocumentType,
                IdentificationDocumentNumber = BorrowerCreateViewModel.Borrower.IdentificationDocumentNumber,
                IdentificationDocumentURL = BorrowerCreateViewModel.Borrower.IdentificationDocumentURL,
                Email = BorrowerCreateViewModel.Borrower.Email,
                Phone = BorrowerCreateViewModel.Borrower.Phone,
                Address = BorrowerCreateViewModel.Borrower.Address,
                Country = BorrowerCreateViewModel.Borrower.Country,
                City = BorrowerCreateViewModel.Borrower.City,
                County = BorrowerCreateViewModel.Borrower.County,
                Subcounty = BorrowerCreateViewModel.Borrower.Subcounty,
                Village = BorrowerCreateViewModel.Borrower.Village,
                Occupation = BorrowerCreateViewModel.Borrower.Occupation,
                Company = BorrowerCreateViewModel.Borrower.Company,
                DisplayPictureURL = BorrowerCreateViewModel.Borrower.DisplayPictureURL,
            });
            await _context.SaveChangesAsync();
            if (!string.IsNullOrEmpty(ReturnURL) && Url.IsLocalUrl(ReturnURL))
            {
                return LocalRedirect(ReturnURL);
            }

            return RedirectToPage("./Index");
        }

        private bool NewFileExists(string fileName)
        {
            return !string.IsNullOrEmpty(fileName);
        }
    }
}
