using System.ComponentModel.DataAnnotations;

namespace WebApp.DataTransferModels;

public class PersonBase
{
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
}
