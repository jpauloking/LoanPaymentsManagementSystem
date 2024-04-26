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
    public class IndexModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public IndexModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        public LoanListViewModel LoanListViewModel { get; set; } = new();

        public async Task OnGetAsync()
        {
            if (_context.Loan != null)
            {

                List<Loan> loans = await _context.Loan
                    .Include(loan => loan.Borrower)
                    .AsNoTracking()
                    .ToListAsync();

                LoanListViewModel.Loans = loans.Select(loan => new LoanViewModel()
                {
                    Id = loan.Id,
                    LoanNumber = loan.LoanNumber,
                    DateOpened = loan.DateOpened,
                    Principal = loan.Principal,
                    DurationInDays = loan.DurationInDays,
                    InterestRate = loan.InterestRate,
                    IsOverDue = loan.IsOverDue,
                    DaysOverDueBy = loan.DaysOverDueBy,
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
                }).ToList();

            }
        }
    }
}
