using Microsoft.EntityFrameworkCore;

namespace LicaoDeCasa
{
    public class AplicationDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Car>().Property(p => p.Age).IsRequired(false).HasMaxLength(4);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Server=localhost; Database=Cars; Integrated Security=True; TrustServerCertificate=true");
    }
}
