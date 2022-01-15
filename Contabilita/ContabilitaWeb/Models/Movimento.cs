using System.ComponentModel.DataAnnotations;

namespace ContabilitaWeb.Models
{
    public class Movimento
    {
        [Display(Name = "Partita IVA")]
        public string PartitaIva { get; set; } = null!;

        [Display(Name = "Ragione sociale")]
        public string RagioneSociale { get; set; } = null!;

        public DateTime Data { get; set; }

        [Display(Name = "Tipo")]
        public string? Tipo { get; set; }

        [Display(Name = "Numero")]
        public string? Numero { get; set; }
        public string? Descrizione { get; set; }

        [Display(Name = "Importo")]
        public decimal Importo { get; set; }
    }
}
