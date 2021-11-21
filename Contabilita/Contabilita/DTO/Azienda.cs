using System;
using System.Collections.Generic;

namespace Contabilita.DTO
{
    public class Azienda
    {
        public int Id { get; set; }
        public string PartitaIva { get; set; } = null!;
        public string RagioneSociale { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Iban { get; set; }
    }
}
