using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContabilitaWeb.Models
{
    public class Azienda
    {
        public int Id { get; set; }

        [Display(Name = "Partita IVA")]
        public string PartitaIva { get; set; } = null!;

        [Display(Name = "Ragione sociale")]
        public string RagioneSociale { get; set; } = null!;

        [Display(Name = "Telefono")]
        public string? Telefono { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Iban")]
        public string? Iban { get; set; }
    }
}
