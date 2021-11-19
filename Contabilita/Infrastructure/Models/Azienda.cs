using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class Azienda
    {
        public Azienda()
        {
            Fatturas = new HashSet<Fattura>();
            Movimentos = new HashSet<Movimento>();
        }

        public int Id { get; set; }
        public string PartitaIva { get; set; } = null!;
        public string RagioneSociale { get; set; } = null!;

        public virtual ICollection<Fattura> Fatturas { get; set; }
        public virtual ICollection<Movimento> Movimentos { get; set; }
    }
}
