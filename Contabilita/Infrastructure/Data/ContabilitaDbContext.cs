using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Infrastructure.Models;

namespace Infrastructure.Data
{
    public partial class ContabilitaDbContext : DbContext
    {
        public ContabilitaDbContext()
        {
        }

        public ContabilitaDbContext(DbContextOptions<ContabilitaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Azienda> Aziende { get; set; } = null!;
        public virtual DbSet<Fattura> Fatture { get; set; } = null!;
        public virtual DbSet<Movimento> Movimenti { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Azienda>(entity =>
            {
                entity.HasIndex(e => e.PartitaIva, "IX_Azienda")
                    .IsUnique();

                entity.Property(e => e.PartitaIva)
                    .HasMaxLength(11)
                    .HasColumnName("PartitaIVA")
                    .IsFixedLength();

                entity.Property(e => e.RagioneSociale)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Fattura>(entity =>
            {
                entity.ToTable("Fattura");

                entity.HasIndex(e => e.Numero, "IX_Fattura");

                entity.Property(e => e.Azienda)
                    .HasMaxLength(11)
                    .IsFixedLength();

                entity.Property(e => e.Data).HasColumnType("date");

                entity.Property(e => e.Importo).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Numero)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.AziendaNavigation)
                    .WithMany(p => p.Fatturas)
                    .HasPrincipalKey(p => p.PartitaIva)
                    .HasForeignKey(d => d.Azienda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fattura_Azienda");
            });

            modelBuilder.Entity<Movimento>(entity =>
            {
                entity.ToTable("Movimento");

                entity.Property(e => e.Azienda)
                    .HasMaxLength(11)
                    .IsFixedLength();

                entity.Property(e => e.Descrizione)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Importo).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AziendaNavigation)
                    .WithMany(p => p.Movimentos)
                    .HasPrincipalKey(p => p.PartitaIva)
                    .HasForeignKey(d => d.Azienda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movimento_Azienda");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
