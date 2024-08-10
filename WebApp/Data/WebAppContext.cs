using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Data
{
    public class WebAppContext : IdentityDbContext<WebApp.Models.ApplicationUser>
    {
        public WebAppContext (DbContextOptions<WebAppContext> options)
            : base(options)
        {
        }

        public DbSet<WebApp.Models.Borrower> Borrower { get; set; } = default!;
        public DbSet<WebApp.Models.Loan> Loan { get; set; } = default!;
        public DbSet<WebApp.Models.Installment> Installment { get; set; } = default!;
        public DbSet<WebApp.Models.CreditAnalysis> CreditAnalysis { get; set; } = default!;

    }
}
