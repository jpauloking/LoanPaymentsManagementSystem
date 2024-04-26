namespace WebApp.ViewModels;

public class InstallmentViewModel : DataTransferModels.Installment
{
    public LoanViewModel? Loan { get; set; }
}
