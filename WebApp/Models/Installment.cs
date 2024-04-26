namespace WebApp.Models
{
    public class Installment : DataTransferModels.Installment
    {
        public Loan Loan { get; set; } = null!;
    }
}
