using AutoMapper;
using Infrastructure.Dal;
using Microsoft.AspNetCore.Mvc;

namespace ContabilitaWeb.Controllers
{
    public class PagamentoController : Controller
    {
        private readonly ILogger<PagamentoController> _logger;
        private readonly IContabilitaDal _contabilitaDal;
        private readonly IMapper _mapper;

        public PagamentoController(ILogger<PagamentoController> log, IContabilitaDal contabilitaDal, IMapper mapper)
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
