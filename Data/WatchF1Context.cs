using System;
using System.Collections.Generic;
using F1Project.Models;
using Microsoft.EntityFrameworkCore;

namespace F1Project.Data;

public partial class WatchF1Context : DbContext
{
    public WatchF1Context()
    {
    }

    public WatchF1Context(DbContextOptions<WatchF1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<ConstructorStanding> ConstructorStandings { get; set; }

    public virtual DbSet<DriverStanding> DriverStandings { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:PostgreSQL");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConstructorStanding>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("constructor_standings_pkey");
        });

        modelBuilder.Entity<DriverStanding>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("driver_standings_pkey");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("events_pkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("videos_pkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
