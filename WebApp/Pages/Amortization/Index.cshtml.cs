using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Pages.Amortization
{
    public class IndexModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public IndexModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AmortizationViewModel AmortizationViewModel { get; set; } = new()
        {
            DateOpened = DateTime.Now,
        };

        public void OnGet()
        {
            
        }

        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Loan == null || AmortizationViewModel == null)
            {
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
