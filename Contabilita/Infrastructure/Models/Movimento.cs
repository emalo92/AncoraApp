using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Movimento
    {
        public string PartitaIva { get; set; } = null!;
        public string RagioneSociale { get; set; } = null!;
        public DateTime Data { get; set; }
        public string? Tipo { get; set; }
        public string? Numero { get; set; }
        public string? Descrizione { get; set; }
        public decimal Importo { get; set; }
    }
}
