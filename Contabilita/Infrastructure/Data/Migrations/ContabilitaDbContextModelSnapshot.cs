﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(ContabilitaDbContext))]
    partial class ContabilitaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Infrastructure.Models.Azienda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength();

                    b.Property<string>("Iban")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .IsFixedLength();

                    b.Property<string>("PartitaIva")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nchar(11)")
                        .HasColumnName("PartitaIVA")
                        .IsFixedLength();

                    b.Property<string>("RagioneSociale")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex(new[] { "PartitaIva" }, "IX_Azienda")
                        .IsUnique();

                    b.ToTable("Aziende");
                });

            modelBuilder.Entity("Infrastructure.Models.Fattura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Azienda")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nchar(11)")
                        .IsFixedLength();

                    b.Property<DateTime>("Data")
                        .HasColumnType("date");

                    b.Property<decimal>("Importo")
                        .HasColumnType("decimal(18,0)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength();

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("Azienda");

                    b.HasIndex(new[] { "Numero" }, "IX_Fattura");

                    b.ToTable("Fattura", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Models.Pagamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Azienda")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nchar(11)")
                        .IsFixedLength();

                    b.Property<DateTime>("Data")
                        .HasColumnType("date");

                    b.Property<string>("Descrizione")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<decimal>("Importo")
                        .HasColumnType("decimal(18,0)");

                    b.Property<string>("Modalita")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NumAssegnoBonifico")
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("Azienda");

                    b.ToTable("Pagamento", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Models.Fattura", b =>
                {
                    b.HasOne("Infrastructure.Models.Azienda", "AziendaNavigation")
                        .WithMany("Fatturas")
                        .HasForeignKey("Azienda")
                        .HasPrincipalKey("PartitaIva")
                        .IsRequired()
                        .HasConstraintName("FK_Fattura_Azienda");

                    b.Navigation("AziendaNavigation");
                });

            modelBuilder.Entity("Infrastructure.Models.Pagamento", b =>
                {
                    b.HasOne("Infrastructure.Models.Azienda", "AziendaNavigation")
                        .WithMany("Pagamentos")
                        .HasForeignKey("Azienda")
                        .HasPrincipalKey("PartitaIva")
                        .IsRequired()
                        .HasConstraintName("FK_Movimento_Azienda");

                    b.Navigation("AziendaNavigation");
                });

            modelBuilder.Entity("Infrastructure.Models.Azienda", b =>
                {
                    b.Navigation("Fatturas");

                    b.Navigation("Pagamentos");
                });
#pragma warning restore 612, 618
        }
    }
}