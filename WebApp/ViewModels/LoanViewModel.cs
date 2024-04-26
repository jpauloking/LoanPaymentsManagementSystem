using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class LoanViewModel : DataTransferModels.Loan
{

    public BorrowerViewModel? Borrower { get; set; }
    public List<InstallmentViewModel> Installments { get; set; } = new();

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

    public decimal Amount
    {
        get { return ComputeAmount(); }
    }

    [Display(Name = "Interest accrued")]
    public decimal InterestAccrued
    {
        get { return ComputeInterestAccrued(); }
    }

    [Display(Name = "Borrower name")]
    public string? BorrowerName
    {
        get { return GetBorrowerName(); }
    }

    [Display(Name = "Borrower id")]
    public Guid BorrowerId
    {
        get { return GetBorrowerId(); }
    }

    private decimal ComputeAmount()
    {
        decimal principal = this.Principal;
        decimal interestAccrued = ComputeInterestAccrued();
        decimal amountDue = interestAccrued + principal;
        return amountDue;
    }

    private decimal ComputeInterestAccrued()
    {
        int durationInDays = this.DurationInDays;
        decimal dailyInterest = ComputeDailyInterest();
        decimal interestAccrued = dailyInterest * durationInDays;
        return interestAccrued;
    }

    private decimal ComputeDailyInterest()
    {
        decimal principal = this.Principal;
        double interestRate = ComputePercentageInterestRate();
        decimal dailyInterest = (principal * (decimal)interestRate) / 30;
        return dailyInterest;
    }

    private double ComputePercentageInterestRate()
    {
        double interestRate = this.InterestRate * 0.01;
        return interestRate;
    }

    private string GetBorrowerName()
    {
        string borrowerName = default!;
        if (this.Borrower is not null)
        {
            borrowerName = $"{this.Borrower.FirstName} {this.Borrower.LastName}";

        }
        return borrowerName;
    }

    private Guid GetBorrowerId()
    {
        Guid borrowerId = default!;
        if (this.Borrower is not null)
        {
            borrowerId = this.Borrower.Id;
        }
        return borrowerId;
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
        decimal amountInInstallmentsOutstanding = this.Installments
            .Where(installment => !installment.IsPaid).Sum(installment => installment.Amount);
        return amountInInstallmentsOutstanding;
    }

}
