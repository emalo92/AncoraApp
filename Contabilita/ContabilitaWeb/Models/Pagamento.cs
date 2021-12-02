using System;
using System.Collections.Generic;

namespace ContabilitaWeb.Models
{
    public partial class Pagamento
    {
        public int Id { get; set; }
        public string Modalita { get; set; } = null!;
        public DateTime Data { get; set; }
        public decimal Importo { get; set; }
        public string? NumAssegnoBonifico { get; set; }
        public string? Descrizione { get; set; }
        public string Azienda { get; set; } = null!;
    }
}
