using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WINTEX.Models;

#nullable disable

namespace WINTEX.DAL
{
    public partial class FagElGamousDbContext : DbContext
    {
        public FagElGamousDbContext()
        {
        }

        public FagElGamousDbContext(DbContextOptions<FagElGamousDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bowler> Bowlers { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:IntexPracticeSqlServer");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("bowlers")
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bowler>(entity =>
            {
                entity.Property(e => e.BowlerId).ValueGeneratedNever();

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Bowlers)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bowlers_TeamID_Teams");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.TeamId).ValueGeneratedNever();

                entity.HasOne(d => d.Captain)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.CaptainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teams_Captain_Bowlers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
