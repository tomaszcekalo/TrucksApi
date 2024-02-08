using Microsoft.EntityFrameworkCore;

namespace TrucksApi.Data
{
    public class TrucksDb : DbContext
    {
        public DbSet<Truck> Trucks { get; set; }

        public TrucksDb(DbContextOptions<TrucksDb> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Truck>().ToTable("Trucks");
            modelBuilder.Entity<Truck>()
                .HasIndex(u => u.AlphanumericCode)
                .IsUnique();
        }
    }
}