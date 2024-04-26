using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.DataTransferModels
{
    public class Borrower
    {
        #region Basic Properties
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Display(Name = "Borrower number")]
        [MaxLength(50)]
        public string? BorrowerNumber { get; set; }
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
        [DataType(DataType.MultilineText)]
        [MaxLength(100)]
        public string? Address { get; set; }
        [MaxLength(100)]
        public string? Country { get; set; }
        [MaxLength(100)]
        public string? City { get; set; }
        [MaxLength(100)]
        public string? County { get; set; }
        [MaxLength(100)]
        public string? Subcounty { get; set; }
        [MaxLength(100)]
        public string? Village { get; set; }
        [Display(Name = "Display image")]
        public string? DisplayPictureURL { get; set; }
        [MaxLength(50)]
        public string? Occupation { get; set; }
        [MaxLength(50)]
        public string? Company { get; set; }
        [Display(Name = "Office at company")]
        [MaxLength(50)]
        public string? OfficeAtCompany { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Notes { get; set; }
        [Display(Name = "Date created")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [Display(Name = "Identification document type")]
        [EnumDataType(typeof(IdentificationDocumentType))]
        public IdentificationDocumentType IdentificationDocumentType { get; set; } = IdentificationDocumentType.NATIONAL_IDENTITY_CARD;
        [Display(Name = "Identification document number")]
        [MaxLength(100)]
        [Required]
        public string IdentificationDocumentNumber { get; set; } = null!;
        [Display(Name = "Scanned image of identification document")]
        public string? IdentificationDocumentURL { get; set; }
        #endregion
    }
}
