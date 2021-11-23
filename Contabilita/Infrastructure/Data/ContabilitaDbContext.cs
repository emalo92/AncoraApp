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
        public virtual DbSet<Pagamento> Pagamenti { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=.; initial catalog = Contabilita; persist security info=True; Integrated Security = SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Azienda>(entity =>
            {
                entity.HasIndex(e => e.PartitaIva, "IX_Azienda")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Iban)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.PartitaIva)
                    .HasMaxLength(11)
                    .HasColumnName("PartitaIVA")
                    .IsFixedLength();

                entity.Property(e => e.RagioneSociale)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsFixedLength();
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
                    .IsFixedLength();

                entity.Property(e => e.Tipo)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.HasOne(d => d.AziendaNavigation)
                    .WithMany(p => p.Fatturas)
                    .HasPrincipalKey(p => p.PartitaIva)
                    .HasForeignKey(d => d.Azienda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fattura_Azienda");
            });

            modelBuilder.Entity<Pagamento>(entity =>
            {
                entity.ToTable("Pagamento");

                entity.Property(e => e.Azienda)
                    .HasMaxLength(11)
                    .IsFixedLength();

                entity.Property(e => e.Data).HasColumnType("date");

                entity.Property(e => e.Descrizione)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Importo).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Modalita)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumAssegnoBonifico)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.HasOne(d => d.AziendaNavigation)
                    .WithMany(p => p.Pagamentos)
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
