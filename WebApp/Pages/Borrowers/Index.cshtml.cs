using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.DataTransferModels;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Pages.Borrowers
{
    public class IndexModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public IndexModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        public BorrowerListViewModel BorrowerListViewModel { get; set; } = new();

        public async Task OnGetAsync()
        {
            if (_context.Borrower != null)
            {

                var borrowers = await _context.Borrower
                    .Include(borrower => borrower.Loans)
                    .ThenInclude(loan => loan.Installments)
                    .AsNoTracking()
                    .ToListAsync();

                BorrowerListViewModel.Borrowers = borrowers
                    .Select(borrower => BorrowerViewModel.GetViewModelFromModel(borrower)).ToList();

            }
        }
    }
}
