using AutoMapper;
using Infrastructure.Dal;
using Microsoft.AspNetCore.Mvc;

namespace ContabilitaWeb.Controllers
{
    public class FatturaController : Controller
    {
        private readonly ILogger<FatturaController> _logger;
        private readonly IContabilitaDal _contabilitaDal;
        private readonly IMapper _mapper;

        public FatturaController(ILogger<FatturaController> log, IContabilitaDal contabilitaDal, IMapper mapper)
        {
            _logger = log;
            _contabilitaDal = contabilitaDal;
            _mapper = mapper;
        }

        public IActionResult ViewAll()
        {
            return View();
        }
    }
}
