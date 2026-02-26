using Microsoft.EntityFrameworkCore;
using ProductListingWebsite.Models;

namespace ProductListingWebsite.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
