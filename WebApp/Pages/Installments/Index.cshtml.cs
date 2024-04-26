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
    public class IndexModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public IndexModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        public InstallmentListViewModel InstallmentListViewModel { get; set; } = new();

        public async Task OnGetAsync()
        {
            if (_context.Installment != null)
            {
                List<Installment> installments = await _context.Installment
                    .Include(installment => installment.Loan)
                    .ThenInclude(loan => loan.Borrower)
                    .AsNoTracking()
                    .ToListAsync();
                InstallmentListViewModel.Installments = installments.Select(installment => new InstallmentViewModel()
                {
                    Id = installment.Id,
                    DateDue = installment.DateDue,
                    InstallmentNumber = installment.InstallmentNumber,
                    PaymentNumber = installment.PaymentNumber,
                    Amount = installment.Amount,
                    BalanceAfterInstallment = installment.BalanceAfterInstallment,
                    PercentagePenaltyFeePerDayOverDue = installment.PercentagePenaltyFeePerDayOverDue,
                    IsOverDue = installment.IsOverDue,
                    DaysOverDueBy = installment.DaysOverDueBy,
                    IsPaid = installment.IsPaid,
                    DatePaid = installment.DatePaid,
                    PaidBy = installment.PaidBy,
                    IsScrappedOff = installment.IsScrappedOff,
                    DateScrappedOff = installment.DateScrappedOff,
                    PercentageScrappedOff = installment.PercentageScrappedOff,
                    ScrappedOffBy = installment.ScrappedOffBy,
                    IsWrittenOff = installment.IsWrittenOff,
                    DateWrittenOff = installment.DateWrittenOff,
                    WrittenOffBy = installment.WrittenOffBy,
                    Loan = new LoanViewModel
                    {
                        Id = installment.Loan.Id,
                        LoanNumber = installment.Loan.LoanNumber,
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
                            Id = installment.Loan!.Borrower.Id,
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
                    }
                }).ToList();
            }
        }
    }
}
