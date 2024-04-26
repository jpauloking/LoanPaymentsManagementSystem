using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.DataTransferModels
{
    public class Loan
    {
        #region Basic Properties
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Display(Name = "Loan number")]
        [MaxLength(50)]
        public string? LoanNumber { get; set; }
        [Display(Name = "Date opened")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOpened { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Required]
        public decimal Principal { get; set; }
        [Display(Name = "Duration of loan (days)")]
        [Range(0, UInt16.MaxValue)]
        public int DurationInDays { get; set; }
        [Display(Name = "Interest rate")]
        [Range(0, float.MaxValue)]
        public float InterestRate { get; set; }
        [Display(Name = "Amount per installment")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal AmountPerInstallment { get; set; } = 0;
        [Display(Name = "Number of installments")]
        [Range(1, UInt16.MaxValue)]
        public int NumberOfInstallments { get; set; }
        [Display(Name = "Penalty fee (% per day over-due)")]
        [Range(0, float.MaxValue)]
        public float PercentagePenaltyFeePerDayOverDue { get; set; }
        #endregion

        #region Status Properties
        [Display(Name = "Has been paid")]
        public bool IsPaid { get; set; } = false;
        [Display(Name = "Was paid by")]
        [MaxLength(50)]
        public string? PaidBy { get; set; }
        [Display(Name = "Paid on")]
        [DataType(DataType.Date)]
        public DateTime? DatePaid { get; set; } = null;
        [Display(Name = "Has been written-off")]
        public bool IsWrittenOff { get; set; } = false;
        [Display(Name = "Was written-off by")]
        [MaxLength(50)]
        public string? WrittenOffBy { get; set; }
        [Display(Name = "Written-off on")]
        [DataType(DataType.Date)]
        public DateTime? DateWrittenOff { get; set; } = null;
        [Display(Name = "Has been scrapped-off")]
        public bool IsScrappedOff { get; set; } = false;
        [Display(Name = "% scrapped-off")]
        [Range(0, float.MaxValue)]
        public float PercentageScrappedOff { get; set; }
        [Display(Name = "Was scrapped-off by")]
        [MaxLength(50)]
        public string? ScrappedOffBy { get; set; }
        [Display(Name = "Scrapped-off on")]
        [DataType(DataType.Date)]
        public DateTime? DateScrappedOff { get; set; } = null;
        [Display(Name = "Is over-due")]
        public bool IsOverDue { get; set; } = false;
        [Display(Name = "Is over-due by (days)")]
        [Range(0, UInt16.MaxValue)]
        public int DaysOverDueBy { get; set; } = 0;
        #endregion
    }
}
