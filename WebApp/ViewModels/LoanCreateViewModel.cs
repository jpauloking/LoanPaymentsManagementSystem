namespace WebApp.ViewModels;

public class LoanCreateViewModel
{
    public LoanViewModel Loan { get; set; } = default!;
    public Guid BorrowerId { get; set; }
}
