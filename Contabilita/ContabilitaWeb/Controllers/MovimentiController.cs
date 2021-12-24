using AutoMapper;
using ContabilitaWeb.Models;
using ContabilitaWeb.Utility;
using Infrastructure.Dal;
using Microsoft.AspNetCore.Mvc;

namespace ContabilitaWeb.Controllers
{
    public class MovimentiController : Controller
    {
        private readonly ILogger<MovimentiController> _logger;
        private readonly IContabilitaDal _contabilitaDal;
        private readonly IMapper _mapper;

        public MovimentiController(ILogger<MovimentiController> log, IContabilitaDal contabilitaDal, IMapper mapper)
        {
            _logger = log;
            _contabilitaDal = contabilitaDal;
            _mapper = mapper;
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(InputRicercaMovimenti input)
        {
            _logger.LogInformation("SearchAsync START");
            try
            {
                var inputDal = _mapper.Map<Infrastructure.Models.InputRicercaMovimenti>(input);
                var result = await _contabilitaDal.SearchAsync(inputDal);
                if (result.Count == 0)
                {
                    var responseFailed = new Response
                    {
                        IsSuccess = false,
                        Message = "Non ci sono movimenti da visualizzare"
                    };
                    ViewMessage.Show(this, responseFailed);
                    return View(input);
                }
                var resultWeb = _mapper.Map<List<Movimento>>(result);
                ViewBag.ResultList = resultWeb;
                return View(input);
            }
            catch (Exception ex)
            {
                _logger.LogError("SearchAsync: " + ex.Message);
                var responseFailed = new Response
                {
                    IsSuccess = false,
                    Message = "Si è verificato un errore durante il recupero dei dati"
                };
                ViewMessage.Show(this, responseFailed);
                return View(input);
            }
        }
    }
}
