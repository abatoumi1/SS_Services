﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversityApp.Data;

namespace MemberShipApp.Migrations
{
    [DbContext(typeof(MemberShipContext))]
    partial class MemberShipContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MemberShipApp.Models.Contribution", b =>
                {
                    b.Property<int>("ContributionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("ContributionDate");

                    b.Property<int>("ContributionMethodID");

                    b.Property<int>("MemberID");

                    b.HasKey("ContributionID");

                    b.HasIndex("ContributionMethodID");

                    b.HasIndex("MemberID");

                    b.ToTable("Contribution");
                });

            modelBuilder.Entity("MemberShipApp.Models.ContributionMethod", b =>
                {
                    b.Property<int>("ContributionMethodID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ContributionMethodID");

                    b.ToTable("ContributionMethod");
                });

            modelBuilder.Entity("MemberShipApp.Models.Country", b =>
                {
                    b.Property<int>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("CountryID");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("MemberShipApp.Models.Member", b =>
                {
                    b.Property<int>("MemberID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(156);

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("PositionID");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("StateID");

                    b.HasKey("MemberID");

                    b.HasIndex("PositionID");

                    b.HasIndex("StateID");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("MemberShipApp.Models.Position", b =>
                {
                    b.Property<int>("PositionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(256);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("PositionID");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("MemberShipApp.Models.Region", b =>
                {
                    b.Property<int>("RegionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryID");

                    b.Property<string>("Description")
                        .HasMaxLength(256);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("RegionID");

                    b.HasIndex("CountryID");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("MemberShipApp.Models.State", b =>
                {
                    b.Property<int>("StateID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("RegionID");

                    b.HasKey("StateID");

                    b.HasIndex("RegionID");

                    b.ToTable("State");
                });

            modelBuilder.Entity("MemberShipApp.Models.Contribution", b =>
                {
                    b.HasOne("MemberShipApp.Models.ContributionMethod", "ContributionMethod")
                        .WithMany("Contributions")
                        .HasForeignKey("ContributionMethodID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MemberShipApp.Models.Member", "Member")
                        .WithMany("Contributions")
                        .HasForeignKey("MemberID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MemberShipApp.Models.Member", b =>
                {
                    b.HasOne("MemberShipApp.Models.Position", "Position")
                        .WithMany("Members")
                        .HasForeignKey("PositionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MemberShipApp.Models.State", "State")
                        .WithMany("Members")
                        .HasForeignKey("StateID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MemberShipApp.Models.Region", b =>
                {
                    b.HasOne("MemberShipApp.Models.Country", "Country")
                        .WithMany("Regions")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MemberShipApp.Models.State", b =>
                {
                    b.HasOne("MemberShipApp.Models.Region", "Region")
                        .WithMany("States")
                        .HasForeignKey("RegionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}