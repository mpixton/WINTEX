using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WINTEX.Models;

#nullable disable

namespace WINTEX.DAL
{
    public partial class FEGBExcavationContext : DbContext
    {
        public FEGBExcavationContext()
        {
        }

        public FEGBExcavationContext(DbContextOptions<FEGBExcavationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BioSamplesNote> BioSamplesNotes { get; set; }
        public virtual DbSet<BiologicalSample> BiologicalSamples { get; set; }
        public virtual DbSet<CarbonDating> CarbonDatings { get; set; }
        public virtual DbSet<Fegbdatum> Fegbdata { get; set; }
        public virtual DbSet<FegbmummyStorage> FegbmummyStorages { get; set; }
        public virtual DbSet<FegbstorageLocation> FegbstorageLocations { get; set; }
        public virtual DbSet<Gisdatum> Gisdata { get; set; }
        public virtual DbSet<Mummy> Mummies { get; set; }
        public virtual DbSet<MummyNote> MummyNotes { get; set; }
        public virtual DbSet<OsteologicalMummyDatum> OsteologicalMummyData { get; set; }
        public virtual DbSet<PostExhumationDatum> PostExhumationData { get; set; }
        public virtual DbSet<ShaftLocation> ShaftLocations { get; set; }
        public virtual DbSet<TombLocation> TombLocations { get; set; }
        public virtual DbSet<HairColorCodes> HairColorCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Name=ConnectionStrings:FagElGamousPostGres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<BioSamplesNote>(entity =>
            {
                entity.HasKey(e => e.BioNoteId)
                    .HasName("BioSamplesNotes_pkey");

                entity.Property(e => e.BioNoteId)
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(2L, null, 0L, null, null, null);

                entity.HasOne(d => d.BioSample)
                    .WithMany(p => p.BioSamplesNotes)
                    .HasForeignKey(d => d.BioSampleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("BioSamplesNotes_BioSampleId_BiologicalSamples");
            });

            modelBuilder.Entity<BiologicalSample>(entity =>
            {
                entity.HasKey(e => e.BioSampleId)
                    .HasName("BiologicalSamples_pkey");

                entity.Property(e => e.BioSampleId).HasIdentityOptions(1887L, null, 0L, null, null, null);

                entity.HasOne(d => d.Mummy)
                    .WithMany(p => p.BiologicalSamples)
                    .HasForeignKey(d => d.MummyId)
                    .HasConstraintName("BiologicalSamples_MummyId_Mummies");

                entity.HasOne(d => d.Shaft)
                    .WithMany(p => p.BiologicalSamples)
                    .HasForeignKey(d => d.ShaftId)
                    .HasConstraintName("BiologicalSamples_LocationId_ShaftLocations");
            });

            modelBuilder.Entity<CarbonDating>(entity =>
            {
                entity.Property(e => e.CarbonDatingId).HasIdentityOptions(25L, null, 0L, null, null, null);

                entity.HasOne(d => d.Mummy)
                    .WithMany(p => p.CarbonDatings)
                    .HasForeignKey(d => d.MummyId)
                    .HasConstraintName("CarbonDating_MummyId_Mummies");

                entity.HasOne(d => d.ShaftLocation)
                    .WithMany(p => p.CarbonDatings)
                    .HasForeignKey(d => d.ShaftLocationId)
                    .HasConstraintName("CarbonDating_ShaftLocationIId_ShaftLocations");
            });

            modelBuilder.Entity<Fegbdatum>(entity =>
            {
                entity.HasKey(e => e.MummyId)
                    .HasName("FEGBData_pkey");

                entity.Property(e => e.MummyId).ValueGeneratedNever();

                entity.HasOne(d => d.Mummy)
                    .WithOne(p => p.Fegbdatum)
                    .HasForeignKey<Fegbdatum>(d => d.MummyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FEGBData_MummyId_Mummies");
            });

            modelBuilder.Entity<FegbmummyStorage>(entity =>
            {
                entity.HasKey(e => new { e.MummyId, e.ShelfId })
                    .HasName("FEGBMummyStorage_pkey");

                entity.HasOne(d => d.Mummy)
                    .WithMany(p => p.FegbmummyStorages)
                    .HasForeignKey(d => d.MummyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FEGBMummyStorage_MummyID_Mummies");

                entity.HasOne(d => d.Shelf)
                    .WithMany(p => p.FegbmummyStorages)
                    .HasForeignKey(d => d.ShelfId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FEGBMummyStorage_StorageId_FEGBStorage");
            });

            modelBuilder.Entity<FegbstorageLocation>(entity =>
            {
                entity.HasKey(e => e.ShelfId)
                    .HasName("FEGBStorageLocations_pkey");

                entity.Property(e => e.ShelfId)
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(474L, null, 0L, null, null, null);
            });

            modelBuilder.Entity<Gisdatum>(entity =>
            {
                entity.HasKey(e => e.MummyId)
                    .HasName("GISData_pkey");

                entity.Property(e => e.MummyId).ValueGeneratedNever();

                entity.HasOne(d => d.Mummy)
                    .WithOne(p => p.Gisdatum)
                    .HasForeignKey<Gisdatum>(d => d.MummyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GISData_MummyId_Mummies");
            });

            modelBuilder.Entity<Mummy>(entity =>
            {
                entity.Property(e => e.MummyId).HasIdentityOptions(1254L, null, 0L, null, null, null);

                entity.HasOne(d => d.Shaft)
                    .WithMany(p => p.Mummies)
                    .HasForeignKey(d => d.ShaftId)
                    .HasConstraintName("Mummies_ShaftId_ShaftLocations");

                entity.HasOne(d => d.Tomb)
                    .WithMany(p => p.Mummies)
                    .HasForeignKey(d => d.TombId)
                    .HasConstraintName("Mummies_TombId_TombLocations");
            });

            modelBuilder.Entity<MummyNote>(entity =>
            {
                entity.HasKey(e => e.NoteId)
                    .HasName("MummyNotes_pkey");

                entity.Property(e => e.NoteId)
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(2239L, null, 0L, null, null, null);

                entity.HasOne(d => d.Mummy)
                    .WithMany(p => p.MummyNotes)
                    .HasForeignKey(d => d.MummyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MummyNotes_MummyId_Mummies");
            });

            modelBuilder.Entity<OsteologicalMummyDatum>(entity =>
            {
                entity.HasKey(e => e.MummyId)
                    .HasName("OsteologicalMummydata_pkey");

                entity.Property(e => e.MummyId).ValueGeneratedNever();

                entity.Property(e => e.BasionBregmaHeight).HasPrecision(8, 3);

                entity.Property(e => e.BasionNasion).HasPrecision(8, 3);

                entity.Property(e => e.BasionProstionLength).HasPrecision(8, 3);

                entity.Property(e => e.BizygomaticDiameter).HasPrecision(8, 3);

                entity.Property(e => e.FemurHead).HasPrecision(8, 3);

                entity.Property(e => e.FemurLength).HasPrecision(8, 3);

                entity.Property(e => e.HumerusHead).HasPrecision(8, 3);

                entity.Property(e => e.HumerusLength).HasPrecision(8, 3);

                entity.Property(e => e.InterorbitalBreadth).HasPrecision(8, 3);

                entity.Property(e => e.MaximumCranialBreadth).HasPrecision(8, 3);

                entity.Property(e => e.MaximumCranialLength).HasPrecision(8, 3);

                entity.Property(e => e.MaximumNasalBreadth).HasPrecision(8, 3);

                entity.Property(e => e.NasionProsthion).HasPrecision(8, 3);

                entity.Property(e => e.TibiaLength).HasPrecision(8, 3);

                entity.HasOne(d => d.Mummy)
                    .WithOne(p => p.OsteologicalMummyDatum)
                    .HasForeignKey<OsteologicalMummyDatum>(d => d.MummyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OsteologicalMummyData_MummyId_Mummies");
            });

            modelBuilder.Entity<PostExhumationDatum>(entity =>
            {
                entity.HasKey(e => e.MummyId)
                    .HasName("PostExhumationData_pkey");

                entity.Property(e => e.MummyId).ValueGeneratedNever();

                entity.Property(e => e.EstimateLivingStature).HasPrecision(6, 3);

                entity.Property(e => e.GefunctionTotal).HasPrecision(10, 5);

                entity.HasOne(d => d.Mummy)
                    .WithOne(p => p.PostExhumationDatum)
                    .HasForeignKey<PostExhumationDatum>(d => d.MummyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PostExhumationData_MummyId_Mummies");
            });

            modelBuilder.Entity<ShaftLocation>(entity =>
            {
                entity.HasKey(e => e.ShaftId)
                    .HasName("ShaftLocations_pkey");

                entity.Property(e => e.ShaftId).HasIdentityOptions(82L, null, 0L, null, null, null);

                entity.Property(e => e.Lookup).IsFixedLength(true);

                entity.Property(e => e.Subplot).IsFixedLength(true);
            });

            modelBuilder.Entity<TombLocation>(entity =>
            {
                entity.Property(e => e.TombLocationId).HasIdentityOptions(9L, null, 0L, null, null, null);

                entity.Property(e => e.Tomb).IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
