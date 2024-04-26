namespace WebApp.Models;

public class CreditAnalysis : DataTransferModels.CreditAnalysis
{
    public Borrower Borrower { get; set; } = null!;
}
