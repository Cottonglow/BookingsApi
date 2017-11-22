using BookingsApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookingsApi.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options) {}

        public DbSet<Booking> Booking { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>().ToTable("Booking");
        }

        public void DetachAllEntities()
        {
            foreach (var entity in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
            {
                Entry(entity.Entity).State = EntityState.Detached;
            }
        }
    }
}
