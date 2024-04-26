namespace WebApp.ViewModels;

public class InstallmentListViewModel
{
    public List<InstallmentViewModel> Installments { get; set; } = new();
    //public DataTransferModels.Loan Loan { get; set; } = default!;
    //public DataTransferModels.Borrower Borrower { get; set; } = default!;

    //public Guid LoanId
    //{
    //    get { return GetLoanId(); }
    //}

    //public string LoanNumber
    //{
    //    get { return GetLoanNumber(); }
    //}

    //public Guid BorrowerId
    //{
    //    get { return GetBorrowerId(); }
    //}

    //public string BorrowerName
    //{
    //    get { return GetBorrowerName(); }
    //}

    //public decimal LoanBalance
    //{
    //    get { return GetLoanBalance(); }
    //}

    //private Guid GetLoanId()
    //{
    //    Guid loanId = this.Loan.Id;
    //    return loanId;
    //}

    //private string GetLoanNumber()
    //{
    //    string loanNumber = this.Loan.LoanNumber;
    //    return loanNumber;
    //}

    //private Guid GetBorrowerId()
    //{
    //    Guid borrowerId = this.Borrower.Id;
    //    return borrowerId;
    //}

    //private string GetBorrowerName()
    //{
    //    string borrowerName = $"{this.Borrower.FirstName} {this.Borrower.LastName}";
    //    return borrowerName;
    //}

    //private decimal GetLoanBalance()
    //{
    //    decimal amountPaid = 0;
    //    decimal loanBalance = this.Loan.Principal - amountPaid;
    //    return loanBalance;
    //}
}
