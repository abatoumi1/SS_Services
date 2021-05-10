using System;
using System.Collections.Generic;
using CustomIdentityProvider.CustomProvider;
//using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace CustomIdentityProvider.Data
{
    public class ApplicationDbContext : IdentityDbContext//<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>(b =>
            {
                b.ToTable("CustomUser");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.ToTable("CustomUserClaim");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.ToTable("CustomUserLogin");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.ToTable("CustomUserToken");
            });

            modelBuilder.Entity<IdentityRole>(b =>
            {
                b.ToTable("MyRole");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("MyRoleClaim");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.ToTable("CustomUserRole");
            });

            //modelBuilder.Entity<ApplicationUser>(b =>
            //{
            //    // Each User can have many UserClaims
            //    b.HasMany(e => e..Claims)
            //        .WithOne()
            //        .HasForeignKey(uc => uc.UserId)
            //        .IsRequired();

            //    // Each User can have many UserLogins
            //    b.HasMany(e => e.Logins)
            //        .WithOne()
            //        .HasForeignKey(ul => ul.UserId)
            //        .IsRequired();

            //    // Each User can have many UserTokens
            //    b.HasMany(e => e.Tokens)
            //        .WithOne()
            //        .HasForeignKey(ut => ut.UserId)
            //        .IsRequired();

            //    // Each User can have many entries in the UserRole join table
            //    b.HasMany(e => e.UserRoles)
            //        .WithOne(e => e.User)
            //        .HasForeignKey(ur => ur.UserId)
            //        .IsRequired();
            //});

            //modelBuilder.Entity<ApplicationRole>(b =>
            //{
            //    // Each Role can have many entries in the UserRole join table
            //    b.HasMany(e => e.UserRoles)
            //        .WithOne(e => e.Role)
            //        .HasForeignKey(ur => ur.RoleId)
            //        .IsRequired();
            //});
        }
    }

}