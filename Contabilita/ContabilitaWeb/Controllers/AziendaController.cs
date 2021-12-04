using AutoMapper;
using ContabilitaWeb.Models;
using ContabilitaWeb.Utility;
using Infrastructure.Dal;
using Microsoft.AspNetCore.Mvc;

namespace ContabilitaWeb.Controllers
{
    public class AziendaController : Controller
    {

        private readonly ILogger<AziendaController> _logger;
        private readonly IContabilitaDal _contabilitaDal;
        private readonly IMapper _mapper;

        public AziendaController(ILogger<AziendaController> log, IContabilitaDal contabilitaDal, IMapper mapper)
        {
            _logger = log;
            _contabilitaDal = contabilitaDal;
            _mapper = mapper;
        }

        public async Task<IActionResult> ViewAllAsync()
        {
            _logger.LogInformation("ViewAllAsync START");
            try
            {
                var result = await _contabilitaDal.GetAllAziendeAsync();
                if (result.Count == 0)
                {
                    var responseFailed = new Response
                    {
                        IsSuccess = false,
                        Message = "Non ci sono aziende da visualizzare"
                    };
                    ViewMessage.Show(this, responseFailed);
                    return View();
                }
                var resultWeb = _mapper.Map<List<Azienda>>(result);
                return View(resultWeb);
            }
            catch (Exception ex)
            {
                _logger.LogError("ViewAllAsync: " + ex.Message);
                var responseFailed = new Response
                {
                    IsSuccess = false,
                    Message = "Si è verificato un errore durante il recupero dei dati"
                };
                ViewMessage.Show(this, responseFailed);
                return View();
            }
        }

        public IActionResult NewAzienda()
        {
            _logger.LogInformation("NewAzienda START");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewAziendaAsync(Azienda azienda)
        {
            _logger.LogInformation("NewAziendaAsync START");
            try
            {
                if (azienda == null)
                {
                    throw new Exception("azienda is null");
                }
                var aziendaInfr = _mapper.Map<Infrastructure.Models.Azienda>(azienda);
                var result = await _contabilitaDal.SaveAziendaAsync(aziendaInfr, Infrastructure.Models.TipoCrud.insert);
                var responseSuccess = new Response
                {
                    IsSuccess = true,
                    Message = "Azienda registrata correttamente"
                };
                ViewMessage.Show(this, responseSuccess);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("NewAziendaAsync: " + ex.Message);
                var responseFailed = new Response
                {
                    IsSuccess = false,
                    Message = "Si è verificato un errore durante il salvataggio dell'azienda"
                };
                ViewMessage.Show(this, responseFailed);
                return View();
            }
        }

        public IActionResult EditAzienda()
        {
            _logger.LogInformation("EditAzienda START");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditAziendaAsync(Azienda azienda, TipoCrud tipoCrud)
        {
            _logger.LogInformation("EditAziendaAsync START");
            try
            {
                if (azienda == null)
                {
                    throw new Exception("azienda is null");
                }
                var aziendaInfr = _mapper.Map<Infrastructure.Models.Azienda>(azienda);
                var tipoCrudInfr = _mapper.Map<Infrastructure.Models.TipoCrud>(tipoCrud);
                var result = await _contabilitaDal.SaveAziendaAsync(aziendaInfr, tipoCrudInfr);
                var message = tipoCrud == TipoCrud.update ? "modificata" : "eliminata";
                var responseSuccess = new Response
                {
                    IsSuccess = true,
                    Message = $"Azienda {message} correttamente"
                };
                ViewMessage.Show(this, responseSuccess);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("EditAziendaAsync: " + ex.Message);
                var message = tipoCrud == TipoCrud.update ? "la modifica" : "l'eliminazione";
                var responseFailed = new Response
                {
                    IsSuccess = false,
                    Message = $"Si è verificato un errore durante {message} dell'azienda"
                };
                ViewMessage.Show(this, responseFailed);
                return View();
            }
        }
    }
}
