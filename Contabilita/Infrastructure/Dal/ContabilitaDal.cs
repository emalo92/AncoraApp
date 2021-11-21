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
                _logger.LogError("GetAziende:" + ex.Message);
                throw;
            }
        }

        public async Task<bool> SaveAziendaAsync(Azienda azienda)
        {
            _logger.LogInformation("GetAziende START");
            try
            {
                await _context.Aziende.AddAsync(azienda);
                return await _context.SaveChangesAsync() == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetAziende:" + ex.Message);
                throw;
            }
        }
    }
}
