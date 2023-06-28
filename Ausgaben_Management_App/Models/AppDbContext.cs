using Microsoft.EntityFrameworkCore;

namespace Ausgaben_Management_App.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options) 
        {
            
        }
        public DbSet< TbTransaktion> TbTransaktionen { get; set; }
        public DbSet<TbKategorie> TbKategorien { get; set; }
    }
}
