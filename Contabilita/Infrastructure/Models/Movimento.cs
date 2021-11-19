using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class Movimento
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public decimal Importo { get; set; }
        public string? Descrizione { get; set; }
        public string Azienda { get; set; } = null!;

        public virtual Azienda AziendaNavigation { get; set; } = null!;
    }
}
