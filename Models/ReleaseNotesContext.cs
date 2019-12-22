using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApi.Models
{
    public partial class ReleaseNotesContext : DbContext
    {
        public ReleaseNotesContext()
        {
        }

        public ReleaseNotesContext(DbContextOptions<ReleaseNotesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CleType> CleType { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<CountryCode> CountryCode { get; set; }
        public virtual DbSet<CountryCodeReleaseNote> CountryCodeReleaseNote { get; set; }
        public virtual DbSet<Environment> Environment { get; set; }
        public virtual DbSet<EnvironmentReleaseNote> EnvironmentReleaseNote { get; set; }
        public virtual DbSet<Release> Release { get; set; }
        public virtual DbSet<ReleaseNote> ReleaseNote { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
          
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CleType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CountryCode>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CountryCodeReleaseNote>(entity =>
            {
                entity.HasKey(e => new { e.CountryCodeId, e.ReleaseNoteId });

                entity.HasOne(d => d.CountryCode)
                    .WithMany(p => p.CountryCodeReleaseNote)
                    .HasForeignKey(d => d.CountryCodeId)
                    .HasConstraintName("FK__CountryCo__Count__7F4BDEC0");

                entity.HasOne(d => d.ReleaseNote)
                    .WithMany(p => p.CountryCodeReleaseNote)
                    .HasForeignKey(d => d.ReleaseNoteId)
                    .HasConstraintName("FK__CountryCo__Relea__01342732");
            });

            modelBuilder.Entity<Environment>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EnvironmentReleaseNote>(entity =>
            {
                entity.HasKey(e => new { e.EnvironmentId, e.ReleaseNoteId });

                entity.HasOne(d => d.Environment)
                    .WithMany(p => p.EnvironmentReleaseNote)
                    .HasForeignKey(d => d.EnvironmentId)
                    .HasConstraintName("FK__Environme__Envir__004002F9");

                entity.HasOne(d => d.ReleaseNote)
                    .WithMany(p => p.EnvironmentReleaseNote)
                    .HasForeignKey(d => d.ReleaseNoteId)
                    .HasConstraintName("FK__Environme__Relea__02284B6B");
            });

            modelBuilder.Entity<Release>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NickName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ReleaseNote>(entity =>
            {
                entity.HasIndex(e => new { e.CleTypeId, e.ReleaseId, e.KeyName })
                    .HasName("IX_ReleaseNotes")
                    .IsUnique();

                entity.Property(e => e.CleTypeId).HasColumnName("CleTypeID");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.KeyName).HasMaxLength(1000);

                entity.Property(e => e.ReleaseId).HasColumnName("ReleaseID");

                entity.Property(e => e.Value).HasMaxLength(1000);

                entity.HasOne(d => d.CleType)
                    .WithMany(p => p.ReleaseNote)
                    .HasForeignKey(d => d.CleTypeId)
                    .HasConstraintName("FK__ReleaseNo__CleTy__7D63964E");

                entity.HasOne(d => d.Release)
                    .WithMany(p => p.ReleaseNote)
                    .HasForeignKey(d => d.ReleaseId)
                    .HasConstraintName("FK__ReleaseNo__Relea__7E57BA87");
            });
        }
    }
}
