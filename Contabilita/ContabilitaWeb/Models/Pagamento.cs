using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContabilitaWeb.Models
{
    public partial class Pagamento
    {
        public int Id { get; set; }

        [Display(Name = "Modalita")]
        public string Modalita { get; set; } = null!;

        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Display(Name = "Importo")]
        public decimal Importo { get; set; }

        [Display(Name = "NumAssegnoBonifico")]
        public string? NumAssegnoBonifico { get; set; }

        [Display(Name = "Descrizione")]
        public string? Descrizione { get; set; }

        [Display(Name = "Azienda")]
        public string Azienda { get; set; } = null!;
    }
}
