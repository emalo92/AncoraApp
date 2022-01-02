using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContabilitaWeb.Models
{
    public class Fattura
    {
        public int Id { get; set; }

        [Display(Name = "Numero")]
        public string Numero { get; set; } = null!;

        [Display(Name ="Data")]
        public DateTime Data { get; set; }

        [Display(Name ="Importo")]
        public decimal Importo { get; set; }

        [Display(Name ="Tipo")]
        public string Tipo { get; set; } = null!;

        [Display(Name ="Azienda")]
        public string Azienda { get; set; } = null!;
        public virtual Azienda AziendaNavigation { get; set; } = null!;
    }
}
