using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Infrastructure.Persistence
{
    public class ExchanGODbContext : DbContext
    {
        public ExchanGODbContext(DbContextOptions<ExchanGODbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasIndex(u => u.Username).IsUnique();
                b.HasIndex(u => u.Email).IsUnique();

                b.Property(u => u.Username).HasMaxLength(100).IsRequired();
                b.Property(u => u.Email).IsRequired();
                b.Property(u => u.Password).IsRequired();
            });

            modelBuilder.Entity<Currency>(b =>
            {
                b.HasIndex(c => c.Code).IsUnique();

                b.Property(c => c.Code).HasMaxLength(3).IsRequired();
                b.Property(c => c.Legend).HasMaxLength(50).IsRequired();
                b.Property(c => c.Symbol).HasMaxLength(3).IsRequired();
                b.Property(c => c.ConvertibilityIndex).IsRequired();
            });
        }
    }
}
