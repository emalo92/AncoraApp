using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class PrimaMigrazione : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aziende",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartitaIVA = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false),
                    RagioneSociale = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true),
                    Iban = table.Column<string>(type: "nchar(30)", fixedLength: true, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aziende", x => x.Id);
                    table.UniqueConstraint("AK_Aziende_PartitaIVA", x => x.PartitaIVA);
                });

            migrationBuilder.CreateTable(
                name: "Fattura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: false),
                    Data = table.Column<DateTime>(type: "date", nullable: false),
                    Importo = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Tipo = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: false),
                    Azienda = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fattura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fattura_Azienda",
                        column: x => x.Azienda,
                        principalTable: "Aziende",
                        principalColumn: "PartitaIVA");
                });

            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modalita = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Data = table.Column<DateTime>(type: "date", nullable: false),
                    Importo = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    NumAssegnoBonifico = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    Descrizione = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Azienda = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimento_Azienda",
                        column: x => x.Azienda,
                        principalTable: "Aziende",
                        principalColumn: "PartitaIVA");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Azienda",
                table: "Aziende",
                column: "PartitaIVA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fattura",
                table: "Fattura",
                column: "Numero");

            migrationBuilder.CreateIndex(
                name: "IX_Fattura_Azienda",
                table: "Fattura",
                column: "Azienda");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_Azienda",
                table: "Pagamento",
                column: "Azienda");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fattura");

            migrationBuilder.DropTable(
                name: "Pagamento");

            migrationBuilder.DropTable(
                name: "Aziende");
        }
    }
}
