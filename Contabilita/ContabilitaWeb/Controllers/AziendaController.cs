using AutoMapper;
using ContabilitaWeb.DTO;
using Infrastructure.Dal;
using Microsoft.AspNetCore.Mvc;

namespace ContabilitaWeb.Controllers
{
    public class AziendaController : Controller
    {

        private readonly ILogger<AziendaController> log;
        private readonly IContabilitaDal _contabilitaDal;
        private readonly IMapper _mapper;

        public AziendaController(ILogger<AziendaController> log, IContabilitaDal contabilitaDal, IMapper mapper)
        {
            this.log = log;
            _contabilitaDal = contabilitaDal;
            _mapper = mapper;
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
