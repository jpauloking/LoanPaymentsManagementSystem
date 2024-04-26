using System.ComponentModel.DataAnnotations;
using WebApp.DataTransferModels;

namespace WebApp.ViewModels
{
    public class BorrowerViewModel : DataTransferModels.Borrower
    {
        [Display(Name = "Name")]
        public string DisplayName => $"{FirstName} {LastName}";
        public List<LoanViewModel> Loans { get; set; } = new();
        public List<InstallmentViewModel> Installments { get; set; } = new();
        public List<CreditAnalysisViewModel> CreditAnalyses { get; set; } = new();

        public static BorrowerViewModel GetViewModelFromModel(WebApp.Models.Borrower borrower)
        {
            return new BorrowerViewModel()
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
                }).ToList(),
                Installments = borrower.Loans
                        .SelectMany(loan => loan.Installments)
                        .Select(installment => new InstallmentViewModel
                        {
                            Id = installment.Id,
                            InstallmentNumber = installment.InstallmentNumber,
                            PaymentNumber = installment.PaymentNumber,
                            DateDue = installment.DateDue,
                            Amount = installment.Amount,
                            BalanceAfterInstallment = installment.BalanceAfterInstallment,
                            IsPaid = installment.IsPaid,
                            DatePaid = installment.DatePaid,
                            PaidBy = installment.PaidBy,
                            IsOverDue = installment.IsOverDue,
                            DaysOverDueBy = installment.DaysOverDueBy,
                            IsScrappedOff = installment.IsScrappedOff,
                            DateScrappedOff = installment.DateScrappedOff,
                            ScrappedOffBy = installment.ScrappedOffBy,
                            IsWrittenOff = installment.IsWrittenOff,
                            DateWrittenOff = installment.DateWrittenOff,
                            WrittenOffBy = installment.WrittenOffBy,
                            PercentagePenaltyFeePerDayOverDue = installment.PercentagePenaltyFeePerDayOverDue,
                        }).ToList()
            };
        }

        public int NumberOfLoans
        {
            get { return GetNumberOfLoans(); }
        }

        public int NumberOfLoansOutstanding
        {
            get { return GetNumberOfLoansOutstanding(); }
        }

        public decimal AmountInLoans
        {
            get { return GetAmountInLoans(); }
        }

        public decimal AmountInLoansOutstanding
        {
            get { return GetAmountInLoansOutstanding(); }
        }

        public int NumberOfInstallments
        {
            get { return GetNumberOfInstallments(); }
        }

        public int NumberOfInstallmentsOustanding
        {
            get { return GetNumberOfInstallmentsOustanding(); }
        }

        public decimal AmountInInstallments
        {
            get { return GetAmountInInstallments(); }
        }

        public decimal AmountInInstallmentsOutstanding
        {
            get { return GetAmountInInstallmentsOutstanding(); }
        }

        private int GetNumberOfLoans()
        {
            int numberOfLoans = this.Loans.Count;
            return numberOfLoans;
        }

        private decimal GetAmountInLoans()
        {
            decimal amountInLoans = this.Loans.Sum(loan => loan.Amount);
            return amountInLoans;
        }

        private int GetNumberOfLoansOutstanding()
        {
            int numberOfLoans = this.Loans.Where(loan => !loan.IsPaid).Count();
            return numberOfLoans;
        }

        private decimal GetAmountInLoansOutstanding()
        {
            decimal amountInLoansOutstanding = this.Loans
                .Where(loan => !loan.IsPaid).Sum(loan => loan.Amount);
            return amountInLoansOutstanding;
        }

        private int GetNumberOfInstallments()
        {
            int numberOfInstallments = this.Installments.Count;
            return numberOfInstallments;
        }

        private decimal GetAmountInInstallments()
        {
            decimal amountInInstallments = this.Installments.Sum(installment => installment.Amount);
            return amountInInstallments;
        }

        private int GetNumberOfInstallmentsOustanding()
        {
            int numberOfInstallmentsOutstanding = this.Installments.Where(installment => !installment.IsPaid).Count();
            return numberOfInstallmentsOutstanding;
        }

        private decimal GetAmountInInstallmentsOutstanding()
        {
            decimal amountInInstallmentsOutstanding = this.Installments.Where(installment => !installment.IsPaid).Sum(installment => installment.Amount);
            return amountInInstallmentsOutstanding;
        }
    }
}
