using Infrastructure.Data;
using Infrastructure.Models;
using Infrastructure.Models.Paginations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dal
{
    public class ContabilitaDal : IContabilitaDal
    {
        public readonly ContabilitaDbContext _context;
        public readonly ILogger<ContabilitaDal> _logger;

        public ContabilitaDal (ContabilitaDbContext context, ILogger<ContabilitaDal> logger)
        {
            _context = context;
            _logger =  logger;
        }

        public async Task<List<Azienda>> GetAllAziendeAsync()
        {
            _logger.LogInformation("GetAllAziendeAsync START");
            try
            {
                var aziende = await _context.Azienda.AsNoTracking().ToListAsync();
                return aziende;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllAziendeAsync: " + ex.Message);
                throw;
            }
        }

        public async Task<ResultPaginated<Azienda>> GetAllAziendeAsync(Pagination pagination)
        {
            _logger.LogInformation("GetAllAziendeAsync START");
            try
            {
                var aziende = (await GetAllAziendeAsync()).AsQueryable();
                string filtro = "";
                if (pagination != null)
                {
                    if (pagination.ParametriRicerca.ContainsKey("PartitaIVA"))
                    {
                        filtro = pagination.ParametriRicerca["PartitaIVA"];
                        aziende = aziende.Where(f => f.PartitaIva.Contains(filtro));
                    }
                    if (pagination.ParametriRicerca.ContainsKey("RagioneSociale"))
                    {
                        filtro = pagination.ParametriRicerca["RagioneSociale"];
                        aziende = aziende.Where(f => f.RagioneSociale.Contains(filtro));
                    }
                    if (pagination.ParametriRicerca.ContainsKey("Email"))
                    {
                        filtro = pagination.ParametriRicerca["Email"];
                        aziende = aziende.Where(f => f.Email.Contains(filtro));
                    }
                    if (pagination.ParametriRicerca.ContainsKey("Telefono"))
                    {
                        filtro = pagination.ParametriRicerca["Telefono"];
                        aziende = aziende.Where(f => f.Telefono.Contains(filtro));
                    }
                    if (pagination.ParametriRicerca.ContainsKey("Iban"))
                    {
                        filtro = pagination.ParametriRicerca["Iban"];
                        aziende = aziende.Where(f => f.Iban.Contains(filtro));
                    }

                    pagination.TotalItems = aziende.Count();
                    if (pagination.IsPaginated)
                    {
                        var skip = (pagination.CurrentPage - 1) * pagination.ItemsPerPage;
                        aziende = aziende.Skip(skip).Take(pagination.ItemsPerPage);
                    }
                }
                else
                {
                    pagination = new Pagination()
                    {
                        CurrentPage = 1,
                        TotalPages = 1,
                        ItemsPerPage = 0
                    };
                }
                if (pagination.ItemsPerPage > 0)
                {
                    pagination.TotalPages = (int)Math.Ceiling(pagination.TotalItems / (decimal)pagination.ItemsPerPage);
                }
                var documentList = aziende.ToList();
                var result = new ResultPaginated<Azienda>
                {
                    Pagination = pagination,
                    Result = documentList
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllAziendeAsync: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Fattura>> GetAllFattureAsync()
        {
            _logger.LogInformation("GetAllFattureAsync START");
            try
            {
                var fatture = await _context.Fattura.AsNoTracking().ToListAsync();
                return fatture;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllFattureAsync: " + ex.Message);
                throw;
            }
        }
        public async Task<ResultPaginated<Fattura>> GetAllFattureAsync(Pagination pagination) {

            _logger.LogInformation("GetAllFattureAsync START");
            try
            {
                var fatture = (await GetAllFattureAsync()).AsQueryable();
                string filtro = "";
                if (pagination != null)
                {
                    if (pagination.ParametriRicerca.ContainsKey("Numero"))
                    {
                        filtro = pagination.ParametriRicerca["Numero"];
                        fatture = fatture.Where(f => f.Numero.Contains(filtro));
                    }
                    if (pagination.ParametriRicerca.ContainsKey("Data"))
                    {
                        filtro = pagination.ParametriRicerca["Data"];
                        fatture = fatture.Where(f => f.Data.ToString().Contains(filtro));
                    }

                    if (pagination.ParametriRicerca.ContainsKey("Importo"))
                    {
                        filtro = pagination.ParametriRicerca["Importo"];
                        fatture = fatture.Where(f => f.Importo.ToString().Contains(filtro));
                    }
                    if (pagination.ParametriRicerca.ContainsKey("Tipo"))
                    {
                        filtro = pagination.ParametriRicerca["Tipo"];
                        fatture = fatture.Where(f => f.Tipo.Contains(filtro));
                    }
                    if (pagination.ParametriRicerca.ContainsKey("Azienda"))
                    {
                        filtro = pagination.ParametriRicerca["Azienda"];
                        fatture = fatture.Where(f => f.Azienda.Contains(filtro));
                    }

                    pagination.TotalItems = fatture.Count();
                    if (pagination.IsPaginated)
                    {
                        var skip = (pagination.CurrentPage - 1) * pagination.ItemsPerPage;
                        fatture = fatture.Skip(skip).Take(pagination.ItemsPerPage);
                    }
                }
                else
                {
                    pagination = new Pagination()
                    {
                        CurrentPage = 1,
                        TotalPages = 1,
                        ItemsPerPage = 0
                    };
                }
                if (pagination.ItemsPerPage > 0)
                {
                    pagination.TotalPages = (int)Math.Ceiling(pagination.TotalItems / (decimal)pagination.ItemsPerPage);
                }
                var documentList = fatture.ToList();
                var result = new ResultPaginated<Fattura>
                {
                    Pagination = pagination,
                    Result = documentList
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllFattureAsync: " + ex.Message);
                throw;
            }

        }

        public async Task<List<Pagamento>> GetAllPagamentiAsync()
        {
            _logger.LogInformation("GetAllPagamentiAsync START");
            try
            {
                var pagamenti = await _context.Pagamento.AsNoTracking().ToListAsync();
                return pagamenti;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllPagamentiAsync: " + ex.Message);
                throw;
            }
        }

        public async Task<ResultPaginated<Pagamento>> GetAllPagamentiAsync(Pagination pagination) {

            _logger.LogInformation("GetAllPagamentiAsync START");
            try
            {
                var pagamenti = (await GetAllPagamentiAsync()).AsQueryable();
                string filtro = "";
                if (pagination != null)
                {
                    if (pagination.ParametriRicerca.ContainsKey("Modalita"))
                    {
                        filtro = pagination.ParametriRicerca["Modalita"];
                        pagamenti = pagamenti.Where(f => f.Modalita.Contains(filtro));
                    }
                    if (pagination.ParametriRicerca.ContainsKey("Data"))
                    {
                        filtro = pagination.ParametriRicerca["Data"];
                        pagamenti = pagamenti.Where(f => f.Data.ToString().Contains(filtro));
                    }

                    if (pagination.ParametriRicerca.ContainsKey("Importo"))
                    {
                        filtro = pagination.ParametriRicerca["Importo"];
                        pagamenti = pagamenti.Where(f => f.Importo.ToString().Contains(filtro));
                    }
                    if (pagination.ParametriRicerca.ContainsKey("NumAssegnoBonifico"))
                    {
                        filtro = pagination.ParametriRicerca["NumAssegnoBonifico"];
                        pagamenti = pagamenti.Where(f => f.NumAssegnoBonifico.Contains(filtro));
                    }
                    if (pagination.ParametriRicerca.ContainsKey("Descrizione"))
                    {
                        filtro = pagination.ParametriRicerca["Descrizione"];
                        pagamenti = pagamenti.Where(f => f.Descrizione.Contains(filtro));
                    }
                    if (pagination.ParametriRicerca.ContainsKey("Azienda"))
                    {
                        filtro = pagination.ParametriRicerca["Azienda"];
                        pagamenti = pagamenti.Where(f => f.Azienda.Contains(filtro));
                    }

                    pagination.TotalItems = pagamenti.Count();
                    if (pagination.IsPaginated)
                    {
                        var skip = (pagination.CurrentPage - 1) * pagination.ItemsPerPage;
                        pagamenti = pagamenti.Skip(skip).Take(pagination.ItemsPerPage);
                    }
                }
                else
                {
                    pagination = new Pagination()
                    {
                        CurrentPage = 1,
                        TotalPages = 1,
                        ItemsPerPage = 0
                    };
                }
                if (pagination.ItemsPerPage > 0)
                {
                    pagination.TotalPages = (int)Math.Ceiling(pagination.TotalItems / (decimal)pagination.ItemsPerPage);
                }
                var documentList = pagamenti.ToList();
                var result = new ResultPaginated<Pagamento>
                {
                    Pagination = pagination,
                    Result = documentList
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAllPagamentiAsync: " + ex.Message);
                throw;
            }

        }

        public async Task<Azienda> GetAziendaAsync(string partitaIva, string ragioneSociale)
        {
            _logger.LogInformation("GetAziendaAsync START");
            try 
            {
                var aziendaQuery = _context.Azienda.AsNoTracking().AsQueryable();
                if (partitaIva != null)
                {
                    aziendaQuery = aziendaQuery.Where(Azienda => Azienda.PartitaIva == partitaIva);
                }
                if (ragioneSociale != null)
                {
                    aziendaQuery = aziendaQuery.Where(Azienda => Azienda.RagioneSociale == ragioneSociale);
                }
                var azienda = await aziendaQuery.FirstOrDefaultAsync();
                if (azienda == null)
                {
                    throw new Exception($"azienda in null for partitaIva: {partitaIva}, ragioneSociale: {ragioneSociale}");
                }
                return azienda;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAziendaAsync: " + ex.Message);
                throw;
            }
        }

        public async Task<Fattura> GetFatturaAsync(string numero,string azienda,DateTime? data ) 
        {
            _logger.LogInformation("GetFatturaAsync START");
            try
            {
                var fatturaQuery = _context.Fattura.AsNoTracking().AsQueryable();
                if (numero != null)
                {
                    fatturaQuery = fatturaQuery.Where(Fattura => Fattura.Numero == numero);
                }
                if (azienda != null)
                {
                    fatturaQuery = fatturaQuery.Where(Fattura => Fattura.Azienda == azienda);
                }
                if (data != null) 
                {
                    fatturaQuery = fatturaQuery.Where(Fattura => Fattura.Data == data);
                }
                var fattura = await fatturaQuery.FirstOrDefaultAsync();
                if (fattura == null) 
                {
                    throw new Exception($"fattura in null for numero: {numero}");
                }
                return fattura;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetFatturaAsync: " + ex.Message);
                throw;
            }

        }
        public async Task<Pagamento> GetPagamentoAsync(string azienda, DateTime? data)
        {
            _logger.LogInformation("GetPagamentoAsync START");
            try
            {
                var pagamentoQuery = _context.Pagamento.AsNoTracking().AsQueryable();
                if (azienda != null)
                {
                    pagamentoQuery = pagamentoQuery.Where(Pagamento => Pagamento.Azienda == azienda);
                }
                if (data != null)
                {
                    pagamentoQuery = pagamentoQuery.Where(Pagamento => Pagamento.Data == data);
                }
                
                var pagamento = await pagamentoQuery.FirstOrDefaultAsync();
                if (pagamento == null)
                {
                    throw new Exception($"pagamento in null for azienda: {azienda} e for data:{data}");
                }
                return pagamento;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetPagamentoAsync: " + ex.Message);
                throw;
            }

        }

        public async Task<bool> SaveAziendaAsync(Azienda azienda, TipoCrud tipoCrud)
        {
            _logger.LogInformation("SaveAziendaAsync START");
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert:
                        await _context.Azienda.AddAsync(azienda);
                        break;
                    case TipoCrud.update:
                        _context.Entry(azienda).State = EntityState.Modified;
                        break;
                    case TipoCrud.delete:
                        _context.Azienda.Remove(azienda);
                        break;
                }
                return await _context.SaveChangesAsync() == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError("SaveAziendaAsync: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> SaveFatturaAsync(Fattura fattura, TipoCrud tipoCrud)
        {
            _logger.LogInformation("SaveFatturaAsync START");
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert:
                        await _context.Fattura.AddAsync(fattura);
                        break;
                    case TipoCrud.update:
                        _context.Entry(fattura).State = EntityState.Modified;
                        break;
                    case TipoCrud.delete:
                        _context.Fattura.Remove(fattura);
                        break;
                }
                return await _context.SaveChangesAsync() == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError("SaveFatturaAsync: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> SavePagamentoAsync(Pagamento pagamento, TipoCrud tipoCrud)
        {
            _logger.LogInformation("SavePagamentoAsync START");
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert:
                        await _context.Pagamento.AddAsync(pagamento);
                        break;
                    case TipoCrud.update:
                        _context.Entry(pagamento).State = EntityState.Modified;
                        break;
                    case TipoCrud.delete:
                        _context.Pagamento.Remove(pagamento);
                        break;
                }
                return await _context.SaveChangesAsync() == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError("SavePagamentoAsync: " + ex.Message);
                throw;
            }
        }
    }
}
