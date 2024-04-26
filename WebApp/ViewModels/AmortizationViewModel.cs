using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class AmortizationViewModel
    {
        private static Guid key = Guid.NewGuid();
        [Key]
        public Guid Id { get; set; } = key;
        [Display(Name = "Amortization number")]
        [Required]
        public string AmortizationNumber { get; set; } = $"AM-{key.ToString()[..8]}";
        [Display(Name = "First name")]
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; } = null!;
        [Display(Name = "Last name")]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; } = null!;
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? Phone { get; set; }
        [Display(Name = "Date opened")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOpened { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Required]
        public double Principal { get; set; }
        [Display(Name = "Duration of loan (days)")]
        [Range(0, UInt16.MaxValue)]
        public int DurationInDays { get; set; }
        [Display(Name = "Interest rate (%)")]
        [Range(0.0, float.MaxValue)]
        public float InterestRate { get; set; } = 0.0F;
        [Display(Name = "Number of installments")]
        [Range(1, UInt16.MaxValue)]
        public int NumberOfInstallments { get; set; }
        [Display(Name = "Amount per installment")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal AmountPerInstallment { get; set; } = 0;
    }
}
