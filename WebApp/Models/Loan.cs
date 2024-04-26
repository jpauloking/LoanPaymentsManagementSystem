namespace WebApp.Models;

public class Loan : DataTransferModels.Loan
{
    public Borrower Borrower { get; set; } = null!;
    public List<Installment> Installments { get; set; } = new();

    // Todo  - Add integer to track how many loans are in the database IntegerLoanId
    // Todo  - Add collateral
    // Todo - Add guaranter : first name, last name, relationship, email, phone, address, country, city, county, subcounty, village, image
    // Todo - Add collector : first name, last name, relationship, email, phone, address, country, city, county, subcounty, village, image
    // Todo - Add supervisor : first name, last name, relationship, email, phone, address, country, city, county, subcounty, village, image 
}
