using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Trabajo.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configura los tipos de datos para MySQL
            builder.Entity<IdentityRole>(entity =>
            {
                entity.Property(e => e.ConcurrencyStamp).HasColumnType("TEXT");
                entity.Property(e => e.Name).HasColumnType("VARCHAR(256)");
                entity.Property(e => e.NormalizedName).HasColumnType("VARCHAR(256)");
            });

            builder.Entity<IdentityUser>(entity =>
            {
                entity.Property(e => e.ConcurrencyStamp).HasColumnType("TEXT");
                entity.Property(e => e.UserName).HasColumnType("VARCHAR(256)");
                entity.Property(e => e.NormalizedUserName).HasColumnType("VARCHAR(256)");
                entity.Property(e => e.Email).HasColumnType("VARCHAR(256)");
                entity.Property(e => e.NormalizedEmail).HasColumnType("VARCHAR(256)");
            });
        }
    }
}
