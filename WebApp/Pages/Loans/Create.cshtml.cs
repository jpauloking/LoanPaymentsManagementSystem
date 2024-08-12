using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Migrations;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Pages.Loans
{
    public class CreateModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public CreateModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LoanCreateViewModel LoanCreateViewModel { get; set; } = new();
        public List<SelectListItem> Borrowers { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public Guid? BorrowerId { get; set; }

        public async Task<IActionResult> OnGet(Guid? BorrowerId = default)
        {
            await CreateBorrowerSelectList();
            if (BorrowerId is not null)
            {
                LoanCreateViewModel.BorrowerId = (Guid)BorrowerId;
                LoanCreateViewModel.Loan = new()
                {
                    DateOpened = DateTime.Now,
                    Borrower = (await _context.Borrower.Select(borrower => new BorrowerViewModel()
                    {
                        BorrowerNumber = borrower.BorrowerNumber,
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
                    }).FirstOrDefaultAsync(borrower => borrower.Id == LoanCreateViewModel.BorrowerId))!
                };
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await CreateBorrowerSelectList();
            if (BorrowerId is not null)
            {
                LoanCreateViewModel.Loan.Borrower = (await _context.Borrower
                    .Select(borrower => new BorrowerViewModel()
                    {
                        BorrowerNumber = borrower.BorrowerNumber,
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
                    }).FirstOrDefaultAsync(borrower => borrower.Id == BorrowerId))!;
            }

            if (!ModelState.IsValid || _context.Loan == null || LoanCreateViewModel == null)
            {
                return Page();
            }

            Borrower selectedBorrower = (await _context.Borrower.FirstOrDefaultAsync(borrower => borrower.Id == LoanCreateViewModel.BorrowerId))!;
            if (selectedBorrower is not null)
            {
                LoanCreateViewModel.Loan.Borrower = new()
                {
                    BorrowerNumber = selectedBorrower.BorrowerNumber,
                    Id = selectedBorrower.Id,
                    FirstName = selectedBorrower.FirstName,
                    LastName = selectedBorrower.LastName,
                    Email = selectedBorrower.Email,
                    Phone = selectedBorrower.Phone,
                    Address = selectedBorrower.Address,
                    Country = selectedBorrower.Country,
                    City = selectedBorrower.City,
                    County = selectedBorrower.County,
                    Subcounty = selectedBorrower.Subcounty,
                    Village = selectedBorrower.Village,
                    DisplayPictureURL = selectedBorrower.DisplayPictureURL,
                    Company = selectedBorrower.Company,
                    Occupation = selectedBorrower.Occupation,
                };
            }

            LoanCreateViewModel.Loan.Installments = CreateInstallments();
            _context.Loan.Add(new()
            {
                Id = LoanCreateViewModel.Loan.Id,
                LoanNumber = $"LO-{LoanCreateViewModel.Loan.Id.ToString()[..8]}",
                DateOpened = LoanCreateViewModel.Loan.DateOpened,
                Principal = LoanCreateViewModel.Loan.Principal,
                InterestRate = LoanCreateViewModel.Loan.InterestRate,
                DurationInDays = LoanCreateViewModel.Loan.DurationInDays,
                AmountPerInstallment = LoanCreateViewModel.Loan.AmountPerInstallment,
                NumberOfInstallments = LoanCreateViewModel.Loan.NumberOfInstallments,
                PercentagePenaltyFeePerDayOverDue = LoanCreateViewModel.Loan.PercentagePenaltyFeePerDayOverDue,
                Borrower = (await _context.Borrower.FindAsync(LoanCreateViewModel.Loan.Borrower?.Id))!,
                Installments = LoanCreateViewModel.Loan.Installments.Select(installment => new Installment()
                {
                    Id = installment.Id,
                    InstallmentNumber = $"IN-{installment.Id.ToString()[..8]}",
                    PaymentNumber = installment.PaymentNumber,
                    DateDue = installment.DateDue,
                    Amount = installment.Amount,
                    BalanceAfterInstallment = installment.BalanceAfterInstallment,
                    PercentagePenaltyFeePerDayOverDue = installment.PercentagePenaltyFeePerDayOverDue,
                }).ToList()
            });
            await _context.SaveChangesAsync();

            if (BorrowerId.HasValue)
            {
                return RedirectToPage("/Borrowers/Details", new { Id = BorrowerId });
            }

            return RedirectToPage("./Index");
        }

        private List<InstallmentViewModel> CreateInstallments()
        {
            List<InstallmentViewModel> installments = new();
            int durationInDays = LoanCreateViewModel.Loan.DurationInDays;
            decimal principal = LoanCreateViewModel.Loan.Principal;
            decimal amountPerInstallment = LoanCreateViewModel.Loan.AmountPerInstallment;
            DateTime dateOpened = LoanCreateViewModel.Loan.DateOpened;
            float percentagePenaltyFeePerBayOverDue = LoanCreateViewModel.Loan.PercentagePenaltyFeePerDayOverDue;
            double interestRate = LoanCreateViewModel.Loan.InterestRate * 0.01;

            decimal dailyInterest = (principal * (decimal)interestRate) / 30;
            decimal interestAccrued = dailyInterest * durationInDays;
            decimal amountDue = interestAccrued + principal;
            int numberOfInstallments = (int)Math.Ceiling(amountDue / amountPerInstallment);
            decimal runningBalance = amountDue;
            decimal cummulativeInterest;
            decimal interestPayablePerInstallment;
            decimal amountPayablePerInstallment;
            int daysBetweenPayments = durationInDays / numberOfInstallments;
            int daysSinceDateOpened = 0;
            int numberOfInstallmentsCreated = 0;

            do
            {
                decimal balanceBeforePayment;
                decimal balanceAfterPayment;
                daysSinceDateOpened += daysBetweenPayments;
                numberOfInstallmentsCreated++;
                interestPayablePerInstallment = Math.Ceiling(interestAccrued / numberOfInstallments);
                amountPayablePerInstallment = Math.Ceiling(amountDue / numberOfInstallments);
                cummulativeInterest = interestPayablePerInstallment * numberOfInstallmentsCreated;

                if (amountPerInstallment < runningBalance)
                {
                    balanceBeforePayment = Math.Ceiling(runningBalance);
                    balanceAfterPayment = Math.Ceiling(balanceBeforePayment - amountPerInstallment);
                }
                else
                {
                    balanceBeforePayment = Math.Ceiling(runningBalance);
                    amountPerInstallment = Math.Ceiling(balanceBeforePayment);
                    balanceAfterPayment = 0;
                }

                installments.Add(new()
                {
                    Id = Guid.NewGuid(),
                    PaymentNumber = numberOfInstallmentsCreated,
                    DateDue = dateOpened.AddDays(daysSinceDateOpened),
                    Amount = amountPayablePerInstallment,
                    BalanceAfterInstallment = balanceAfterPayment,
                    PercentagePenaltyFeePerDayOverDue = percentagePenaltyFeePerBayOverDue
                });

                runningBalance = balanceAfterPayment;
            } while (runningBalance > 0);
            return installments;
        }

        private async Task CreateBorrowerSelectList()
        {
            List<Borrower> borrowers = await _context.Borrower.AsNoTracking().ToListAsync();
            foreach (Borrower borrower in borrowers)
            {
                Borrowers.Add(new()
                {
                    Text = $"{borrower.FirstName} {borrower.LastName}",
                    Value = borrower.Id.ToString(),
                    Selected = borrower.Id == LoanCreateViewModel.BorrowerId || borrower.Id == BorrowerId
                });
            }
        }
    }
}
