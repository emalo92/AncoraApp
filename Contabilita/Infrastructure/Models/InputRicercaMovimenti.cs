using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class InputRicercaMovimenti
    {
        public DateTime DataDal { get; set; }
        public DateTime DataAl { get; set; }
        public string TipoMovimento { get; set; } = null;
        public string? Azienda { get; set; } = null;
    }
}
