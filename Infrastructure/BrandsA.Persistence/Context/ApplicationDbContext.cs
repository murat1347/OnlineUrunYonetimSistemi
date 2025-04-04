using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.Dtos;
using BrandsA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BrandsA.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
      
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<Token> Tokens { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);

            // Composite key definition
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleNumber });

            // Relationship between User and UserRole
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Remove or comment out the seed data to avoid foreign key conflict
            // modelBuilder.Entity<UserRole>().HasData(
            //     new UserRole { RoleNumber = 1, RoleName = "Admin", UserId = Guid.Parse("d9f0ff72-7ad3-4d0f-b2a4-31f8c409f5a1") },
            //     new UserRole { RoleNumber = 2, RoleName = "User", UserId = Guid.Parse("d9f0ff72-7ad4-4d0f-b2a4-31f2c409f251") }
            // );
        }

    }
}
