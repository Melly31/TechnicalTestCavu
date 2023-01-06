using CavuTechTest.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CavuTechTest.DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Pricing> Pricing { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB; Database=CavuCarParking; Trusted_Connection=True; MultipleActiveResultSets=true");
        }
    }
}