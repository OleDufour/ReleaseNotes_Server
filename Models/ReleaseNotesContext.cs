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
        public virtual DbSet<CountryCode> CountryCode { get; set; }
        public virtual DbSet<Environment> Environment { get; set; }
        public virtual DbSet<ReleaseName> ReleaseName { get; set; }
        public virtual DbSet<ReleaseNote> ReleaseNote { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=SGEW0090\\SQLEXPRESS;Initial Catalog=ReleaseNotes;Integrated Security=True");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CleType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<CountryCode>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Environment>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReleaseName>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NickName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReleaseNote>(entity =>
            {
                entity.Property(e => e.CleTypeId).HasColumnName("CleTypeID");

                entity.Property(e => e.CountryCodeId).HasColumnName("CountryCodeID");

                entity.Property(e => e.EnvironmentId).HasColumnName("EnvironmentID");

                entity.Property(e => e.ReleaseNameId).HasColumnName("ReleaseNameID");

                entity.Property(e => e.Value).HasMaxLength(3000);

                entity.HasOne(d => d.CleType)
                    .WithMany(p => p.ReleaseNote)
                    .HasForeignKey(d => d.CleTypeId)
                    .HasConstraintName("FK__ReleaseNo__CleTy__17036CC0");

                entity.HasOne(d => d.CountryCode)
                    .WithMany(p => p.ReleaseNote)
                    .HasForeignKey(d => d.CountryCodeId)
                    .HasConstraintName("FK__ReleaseNo__Count__17F790F9");

                entity.HasOne(d => d.Environment)
                    .WithMany(p => p.ReleaseNote)
                    .HasForeignKey(d => d.EnvironmentId)
                    .HasConstraintName("FK__ReleaseNo__Envir__18EBB532");

                entity.HasOne(d => d.ReleaseName)
                    .WithMany(p => p.ReleaseNote)
                    .HasForeignKey(d => d.ReleaseNameId)
                    .HasConstraintName("FK__ReleaseNo__Relea__19DFD96B");
            });
        }
    }
}
