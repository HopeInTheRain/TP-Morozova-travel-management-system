using Microsoft.EntityFrameworkCore;
using TravelManagement.Models;

namespace TravelManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        public DbSet<Country> Countries { get; set; }

        public DbSet<Place> Places { get; set; }
    }
}
