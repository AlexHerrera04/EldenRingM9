using EldenRingMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EldenRingMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Boss> Bosses { get; set; }
        public DbSet<LoreEntry> LoreEntries { get; set; }
    }
}
