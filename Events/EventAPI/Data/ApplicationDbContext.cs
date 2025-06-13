using EventAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Client configuration
            builder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CompanyName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.CeoFirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CeoLastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
                entity.Property(e => e.AttendeeCount).IsRequired();
                entity.Property(e => e.IsVip).IsRequired();
                entity.Property(e => e.RegisterDate).IsRequired();

                // Relationship with ApplicationUser
                entity.HasOne(e => e.User)
                      .WithOne(u => u.Client)
                      .HasForeignKey<Client>(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Index for email
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // ApplicationUser configuration
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CreatedDate).IsRequired();
            });
        }
    }
}
