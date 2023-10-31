using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TourismMVC.Models;

public partial class TourismContext : DbContext
{
    public TourismContext()
    {
    }
    public TourismContext(DbContextOptions<TourismContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Tourist> Tourists { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-FQ6KEJ3\\SQLEXPRESS;Database=Tourism; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.Property(e => e.CloseTime)
                .IsRequired()
                .HasMaxLength(8);
            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.OpenTime)
                .IsRequired()
                .HasMaxLength(8);

            entity.HasOne(d => d.Type).WithMany(p => p.Places)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Places_Types");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Table_1");

            entity.Property(e => e.Text).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Place).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.PlaceId)
                .HasConstraintName("FK_Table_1_Places");

            entity.HasOne(d => d.Tourist).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.TouristId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Tourists");
        });

        modelBuilder.Entity<Tourist>(entity =>
        {
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Surname)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Country).WithMany(p => p.Tourists)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tourists_Countries");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_History");

            entity.Property(e => e.VisitDate).HasColumnType("date");

            entity.HasOne(d => d.Place).WithMany(p => p.Visits)
                .HasForeignKey(d => d.PlaceId)
                .HasConstraintName("FK_History_Places");

            entity.HasOne(d => d.Tourist).WithMany(p => p.Visits)
                .HasForeignKey(d => d.TouristId)
                .HasConstraintName("FK_History_Tourists");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
