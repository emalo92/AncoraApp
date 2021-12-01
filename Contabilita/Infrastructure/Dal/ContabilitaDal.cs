using Infrastructure.Data;
using Infrastructure.Models;
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
            _logger.LogInformation("GetAziende START");
            try
            {
                var aziende = await _context.Aziende.AsNoTracking().ToListAsync();
                return aziende;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAziende: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Fattura>> GetAllFattureAsync()
        {
            _logger.LogInformation("GetFattura START");
            try
            {
                var fatture = await _context.Fatture.AsNoTracking().ToListAsync();
                return fatture;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetFattura: " + ex.Message);
                throw;
            }
        }

        public async Task<List<Pagamento>> GetAllPagamentiAsync()
        {
            _logger.LogInformation("GetPagamento START");
            try
            {
                var pagamenti = await _context.Pagamenti.AsNoTracking().ToListAsync();
                return pagamenti;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetPagamento: " + ex.Message);
                throw;
            }
        }

        public async Task<Azienda> GetAziendaAsync(string partitaIva,string ragioneSociale)
        {
            _logger.LogInformation("GetAzienda START");
            try 
            {
                var aziendaQuery = _context.Aziende.AsNoTracking().AsQueryable();
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
                _logger.LogError("GetAzienda: " + ex.Message);
                throw;
            }
        }

        public async Task<Fattura> GetFatturaAsync(string numero, DateTime? data) 
        {
            _logger.LogInformation("GetFattura START");
            try
            {
                var fatturaQuery = _context.Fatture.AsNoTracking().AsQueryable();
                if (numero != null) 
                { 
                    fatturaQuery = fatturaQuery.Where(Fattura => Fattura.Numero == numero);
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
                _logger.LogError("GetFattura: " + ex.Message);
                throw;
            }

        }

        public async Task<bool> SaveAziendaAsync(Azienda azienda, TipoCrud tipoCrud)
        {
            _logger.LogInformation("GetAziende START");
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert:
                        await _context.Aziende.AddAsync(azienda);
                        break;
                    case TipoCrud.update:
                        _context.Entry(azienda).State = EntityState.Modified;
                        break;
                    case TipoCrud.delete:
                        _context.Aziende.Remove(azienda);
                        break;
                }
                return await _context.SaveChangesAsync() == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAziende: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> SaveFatturaAsync(Fattura fattura, TipoCrud tipoCrud)
        {
            _logger.LogInformation("GetFatture START");
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert:
                        await _context.Fatture.AddAsync(fattura);
                        break;
                    case TipoCrud.update:
                        _context.Entry(fattura).State = EntityState.Modified;
                        break;
                    case TipoCrud.delete:
                        _context.Fatture.Remove(fattura);
                        break;
                }
                return await _context.SaveChangesAsync() == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetFatture: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> SavePagamentoAsync(Pagamento pagamento, TipoCrud tipoCrud)
        {
            _logger.LogInformation("GetPagamenti START");
            try
            {
                switch (tipoCrud)
                {
                    case TipoCrud.insert:
                        await _context.Pagamenti.AddAsync(pagamento);
                        break;
                    case TipoCrud.update:
                        _context.Entry(pagamento).State = EntityState.Modified;
                        break;
                    case TipoCrud.delete:
                        _context.Pagamenti.Remove(pagamento);
                        break;
                }
                return await _context.SaveChangesAsync() == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetPagamenti: " + ex.Message);
                throw;
            }
        }
    }
}
