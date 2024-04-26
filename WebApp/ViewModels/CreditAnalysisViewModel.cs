namespace WebApp.ViewModels;

public class CreditAnalysisViewModel : DataTransferModels.CreditAnalysis
{
    public BorrowerViewModel? Borrower { get; set; }
}
