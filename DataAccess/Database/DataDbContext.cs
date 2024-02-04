using Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class DataDbContext: DbContext
    {
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Users> Users { get; set; }

        public DataDbContext(DbContextOptions<DataDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.HasIndex(el => el.Nickname).IsUnique();
                entity.HasIndex(el => el.Email).IsUnique();
                entity.Property(el => el.CreateDate).HasDefaultValue(DateTime.UtcNow);
            });
        }
    }
}
