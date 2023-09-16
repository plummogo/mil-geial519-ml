using LicensePlate.Domain;
using Microsoft.EntityFrameworkCore;

namespace LicensePlate.Repository.Context
{
    public class LicensePlateContext : DbContext
    {
        public LicensePlateContext() { }
        public LicensePlateContext(DbContextOptions<LicensePlateContext> options) : base(options) { }

        public DbSet<Plate> LicensePlates { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=PLM\\SQLEXPRESS;Database=plm-licenseplates;Trusted_Connection=True;");
            }
        }
    }
}
