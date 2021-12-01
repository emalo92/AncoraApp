using AutoMapper;
using ContabilitaWeb.DTO;
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
                return View(result);
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

        public IActionResult Inserisci()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Inserisci(Azienda azienda)
        {
            return View();
        }
    }
}
