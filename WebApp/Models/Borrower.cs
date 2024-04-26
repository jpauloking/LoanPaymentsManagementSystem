namespace WebApp.Models
{
    public class Borrower : DataTransferModels.Borrower
    {
        public List<Loan> Loans { get; set; } = new();
        public List<CreditAnalysis> CreditAnalyses { get; set; } = new();

        // Todo - Add next of kin : first name, last name, relationship, email, phone, address, country, city, county, subcounty, village, image 
    }
}
