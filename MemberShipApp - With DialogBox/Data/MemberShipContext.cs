
using MemberShipApp.Models;
using Microsoft.EntityFrameworkCore;


namespace UniversityApp.Data
{
    public class MemberShipContext : DbContext
    {
        public MemberShipContext (DbContextOptions<MemberShipContext> options)
            : base(options)
        {
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<ContributionMethod> ContributionMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Region>().ToTable("Region");
            modelBuilder.Entity<State>().ToTable("State");
            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<Position>().ToTable("Position");
            modelBuilder.Entity<Member>().ToTable("Member");
            modelBuilder.Entity<Contribution>().ToTable("Contribution");
            modelBuilder.Entity<ContributionMethod>().ToTable("ContributionMethod");

        }
    }
}
