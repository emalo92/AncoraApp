using System;
using System.Collections.Generic;

namespace ContabilitaWeb.DTO
{
    public class Fattura
    {
        public int Id { get; set; }
        public string Numero { get; set; } = null!;
        public DateTime Data { get; set; }
        public decimal Importo { get; set; }
        public string Tipo { get; set; } = null!;
        public string Azienda { get; set; } = null!;
    }
}
