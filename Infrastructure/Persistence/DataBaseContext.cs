using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class DataBaseContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<UserInsurance> UserInsurances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInsurance>()
                .HasKey(ui => new { ui.UserId, ui.InsuranceId });

            modelBuilder.Entity<UserInsurance>()
                .HasOne(ui => ui.User)
                .WithMany(u => u.UserInsurances)
                .HasForeignKey(ui => ui.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserInsurance>()
                .HasOne(ui => ui.Insurance)
                .WithMany(i => i.UserInsurances)
                .HasForeignKey(ui => ui.InsuranceId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost\\SQLEXPRESS;Database=LukaSimonishviliArdiDB;Trusted_Connection=True;MultipleActiveResultSets=True"
            );
        }
    }
}
