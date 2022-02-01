using Infrastructure.Models;
using Infrastructure.Models.Paginations;
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
        Task<Azienda> GetAziendaAsync(string partitaIva, string ragioneSociale = null);
        Task<Fattura> GetFatturaAsync(string numero,string azienda, DateTime? data = null, string tipo = null);
        Task<Fattura> GetFatturaAsync(Fattura fattura);
        Task<Pagamento> GetPagamentoAsync( string azienda, DateTime? data = null);
        Task<bool> SaveAziendaAsync(Azienda azienda, TipoCrud tipoCrud);
        Task<bool> SaveFatturaAsync(Fattura fattura, TipoCrud tipoCrud);
        Task<bool> SavePagamentoAsync(Pagamento pagamento, TipoCrud tipoCrud);
        Task<ResultPaginated<Azienda>> GetAllAziendeAsync(Pagination paginationInfr);
        Task<List<Movimento>> SearchAsync(InputRicercaMovimenti inputDal);
        Task<ResultPaginated<Fattura>> GetAllFattureAsync(Pagination paginationInfr);
        Task<ResultPaginated<Pagamento>> GetAllPagamentiAsync(Pagination paginationInfr);
    }
}

