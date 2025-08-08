using Microsoft.EntityFrameworkCore;
using System.Data;
using VehicleServiceCenter.Domain.Entities;

namespace VehicleServiceCenter.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Unique Composite key for UserRole
            modelBuilder.Entity<UserRole>()
            .HasIndex(ur => new { ur.UserId, ur.RoleId })
            .IsUnique();

            // Store UserType as string
            modelBuilder.Entity<User>()
                .Property(u => u.UserType)
                .HasConversion<string>();

            // Store ServiceStatus as string
            modelBuilder.Entity<ServiceRequest>()
                .Property(sr => sr.Status)
                .HasConversion<string>();

            modelBuilder.Entity<ServiceRequest>()
            .Property(r => r.Cost)
            .HasPrecision(12, 2);

            // Unique RegistrationNumber for Vehicle
            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.RegistrationNumber)
                .IsUnique();

            // prevent delete user if they own multiple vehicles
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Owner)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(v => v.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevent delete of customer if they have service requests deleted
            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Customer)
                .WithMany(u => u.ServiceRequestsAsCustomer)
                .HasForeignKey(sr => sr.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}