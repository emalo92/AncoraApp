using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class Azienda
    {
        public Azienda()
        {
            Fatturas = new HashSet<Fattura>();
            Pagamentos = new HashSet<Pagamento>();
        }

        public int Id { get; set; }
        public string PartitaIva { get; set; } = null!;
        public string RagioneSociale { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Iban { get; set; }

        public virtual ICollection<Fattura> Fatturas { get; set; }
        public virtual ICollection<Pagamento> Pagamentos { get; set; }
    }
}
