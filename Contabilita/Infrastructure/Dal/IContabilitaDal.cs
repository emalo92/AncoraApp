using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dal
{
    public interface IContabilitaDal
    {
        /// <summary>
        /// Metodo che restituisce tutte le aziende presenti nella tabella Aziende
        /// </summary>
        /// <returns></returns>
        Task<List<Azienda>> GetAllAziendeAsync();
        Task<List<Fattura>> GetAllFattureAsync();
        Task<List<Pagamento>> GetAllPagamentiAsync();
        Task<Azienda> GetAziendaAsync(string partitaIva, string ragioneSociale);
        Task<Fattura> GetFatturaAsync(string numero, DateTime? data);
        Task<bool> SaveAziendaAsync(Azienda azienda, TipoCrud tipoCrud);
        Task<bool> SaveFatturaAsync(Fattura fattura, TipoCrud tipoCrud);
        Task<bool> SavePagamentoAsync(Pagamento pagamento, TipoCrud tipoCrud);
    }
}

