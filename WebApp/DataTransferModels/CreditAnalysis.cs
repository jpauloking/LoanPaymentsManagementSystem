using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApp.DataTransferModels;

public class CreditAnalysis
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Display(Name = "Date created")]
    [DataType(DataType.Date)]
    [Required]
    public DateTime DateCreated { get; set; } = DateTime.Now;
    [Display(Name = "Loan principal")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    [Required]
    public decimal LoanPrincipal { get; set; }
    [Display(Name = "Interest rate")]
    [Range(0, float.MaxValue)]
    public float InterestRate { get; set; }
    [Display(Name = "Duration of loan (days)")]
    [Range(0, UInt16.MaxValue)]
    public int DurationInDays { get; set; }
    [Display(Name = "Loan amount")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    [Required]
    public decimal LoanAmount { get; set; }
    [Display(Name = "Average income")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    [Required]
    public decimal AverageIncome { get; set; }
    [Display(Name = "Average expenditure")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    [Required]
    public decimal AvereageExpenditure { get; set; }
    [Display(Name = "Average savings")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    [Required]
    public decimal AverageSavings { get; set; }
    [Display(Name = "Extra income")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    [Required]
    public decimal ExtraIncome { get; set; }
    [DataType(DataType.MultilineText)]
    public string? Notes { get; set; }

    // Todo - Add bank statement

}
