namespace WebApp.ViewModels;

public class BorrowerCreateViewModel
{
    public BorrowerViewModel Borrower { get; set; } = default!;
    public IFormFile? DisplayPictureFile { get; set; }
    public IFormFile? IdentificationDocumentFile { get; set; }
}
