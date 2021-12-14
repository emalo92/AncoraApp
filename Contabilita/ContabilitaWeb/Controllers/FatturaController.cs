using AutoMapper;
using ContabilitaWeb.Models;
using ContabilitaWeb.Models.Paginate;
using ContabilitaWeb.Models.TableToExport;
using ContabilitaWeb.Utility;
using Infrastructure.Dal;
using Microsoft.AspNetCore.Mvc;
using Route = ContabilitaWeb.Models.Paginate.Route;

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

        public async Task<IActionResult> ViewAllAsync()
        {
            _logger.LogInformation("ViewAllAsync START");
            try
            {
                var result = await _contabilitaDal.GetAllFattureAsync();
                if (result.Count == 0)
                {
                    var responseFailed = new Response
                    {
                        IsSuccess = false,
                        Message = "Non ci sono fatture da visualizzare"
                    };
                    ViewMessage.Show(this, responseFailed);
                    return View();
                }
                var resultWeb = _mapper.Map<List<Fattura>>(result);
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

        public IActionResult NewFattura()
        {
            _logger.LogInformation("NewFattura START");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewFatturaAsync(Fattura fattura)
        {
            _logger.LogInformation("NewFatturaAsync START");
            try
            {
                if (fattura == null)
                {
                    throw new Exception("fattura is null");
                }
                var fatturaInfr = _mapper.Map<Infrastructure.Models.Fattura>(fattura);
                var result = await _contabilitaDal.SaveFatturaAsync(fatturaInfr, Infrastructure.Models.TipoCrud.insert);
                var responseSuccess = new Response
                {
                    IsSuccess = true,
                    Message = "Fattura registrata correttamente"
                };
                ViewMessage.Show(this, responseSuccess);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("NewFatturaAsync: " + ex.Message);
                var responseFailed = new Response
                {
                    IsSuccess = false,
                    Message = "Si è verificato un errore durante il salvataggio della fattura"
                };
                ViewMessage.Show(this, responseFailed);
                return View();
            }
        }

        public IActionResult EditFattura()
        {
            _logger.LogInformation("EditFattura START");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditFatturaAsync(Fattura fattura, TipoCrud tipoCrud)
        {
            _logger.LogInformation("EditFatturaAsync START");
            try
            {
                if (fattura == null)
                {
                    throw new Exception("fattura is null");
                }
                var fatturaInfr = _mapper.Map<Infrastructure.Models.Fattura>(fattura);
                var tipoCrudInfr = _mapper.Map<Infrastructure.Models.TipoCrud>(tipoCrud);
                var result = await _contabilitaDal.SaveFatturaAsync(fatturaInfr, tipoCrudInfr);
                var message = tipoCrud == TipoCrud.update ? "modificata" : "eliminata";
                var responseSuccess = new Response
                {
                    IsSuccess = true,
                    Message = $"Fattura {message} correttamente"
                };
                ViewMessage.Show(this, responseSuccess);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("EditFatturaAsync: " + ex.Message);
                var message = tipoCrud == TipoCrud.update ? "la modifica" : "l'eliminazione";
                var responseFailed = new Response
                {
                    IsSuccess = false,
                    Message = $"Si è verificato un errore durante {message} della Fattura"
                };
                ViewMessage.Show(this, responseFailed);
                return View();
            }
        }
        public async Task<IActionResult> GetAllFattureAsync(string nameSearchFilter, string valueSearchFilter, int page = 1)
        {
            _logger.LogInformation("GetAllFattureAsync START");
            var pagination = new Pagination()
            {
                CurrentPage = page,
                ItemsPerPage = 100,
                ParametriRicerca = new Dictionary<string, dynamic>
                {
                },
                Route = new Route { Controller = "Fattura", Action = "GetAllFatture" },
                IsPaginated = true
            };
            valueSearchFilter = nameSearchFilter != null && valueSearchFilter == null ? "" : valueSearchFilter;
            if (nameSearchFilter != null)
            {
                pagination.ParametriRicerca.Add(nameSearchFilter, valueSearchFilter);
            }
            ViewBag.Pagination = pagination;

            var genericTable = new Table()
            {
                Title = "Elenco fatture",
                ColumnNames = new List<string> { "Id", "Numero", "Data", "Importo", "Tipo", "Azienda" },
                Elements = new List<List<object>>()
            };
            try
            {
                var paginationInfr = _mapper.Map<Infrastructure.Models.Paginations.Pagination>(pagination);
                var responseAppCore = await _contabilitaDal.GetAllFattureAsync(paginationInfr);
                var result = _mapper.Map<ResultPaginated<Fattura>>(responseAppCore);
                if (result == null)
                {
                    var responseFailed = new Response
                    {

                        IsSuccess = false,
                        Message = "Si è verificato un errore durante il recupero delle Fatture ",
                    };
                    ViewMessage.ShowLocal(this, responseFailed);
                    return PartialView("_GenericTable", genericTable);
                }

                for (var i = 0; i < result.Result.Count; ++i)
                {
                    genericTable.Elements.Add(new List<object>());
                    genericTable.Elements[i].Add(result.Result[i].Id);
                    genericTable.Elements[i].Add(result.Result[i].Numero);
                    genericTable.Elements[i].Add(result.Result[i].Data);
                    genericTable.Elements[i].Add(result.Result[i].Importo);
                    genericTable.Elements[i].Add(result.Result[i].Tipo);
                    genericTable.Elements[i].Add(result.Result[i].Azienda);
                }

                result.Pagination.ParametriRicerca = new Dictionary<string, dynamic> { { "page", page } };
                if (nameSearchFilter != null && valueSearchFilter != null)
                {
                    result.Pagination.ParametriRicerca.Add("nameSearchFilter", nameSearchFilter);
                    result.Pagination.ParametriRicerca.Add("valueSearchFilter", valueSearchFilter);
                }
                result.Pagination.Route = pagination.Route;
                ViewBag.Pagination = result.Pagination;
                result.Pagination.JavascriptNavigationMethod = "ShowDynamicViewAllFatture";
                ViewBag.ExistsValueFilter = true;
                ViewBag.ParameterNameSearchFilter = "nameSearchFilter";
                ViewBag.ParameterValueSearchFilter = "valueSearchFilter";
                ViewBag.LabelSearchFilter = nameSearchFilter ?? "Numero";
                ViewBag.InputSearchFilter = valueSearchFilter ?? "";
                ViewBag.SizeModal = "modal-xl";
                return PartialView("_GenericTable", genericTable);
            }
            catch (Exception ex)
            {
                var responseFailed = new Response
                {
                    IsSuccess = false,
                    Message = "Si è verificato un errore durante il recupero delle Fatture",
                };
                genericTable.Elements = new List<List<object>>();
                ViewMessage.ShowLocal(this, responseFailed);
                _logger.LogError(ex, "GetAllFattureAsync");
                return PartialView("_GenericTable", genericTable);
            }
        }

        public async Task<IActionResult> GetFatturaAsync(string numero,string azienda)
        {
            _logger.LogInformation("VerificaFatturaAsync START");
            try
            {
                if (numero == null)
                {
                    throw new Exception("numero is null");
                }
                if (azienda == null) 
                {
                    throw new Exception("azienda is null");
                }
                var resultInfr = await _contabilitaDal.GetFatturaAsync(numero,azienda);
                var result = _mapper.Map<Fattura>(resultInfr);
                var response = new Response
                {
                    IsSuccess = result != null,
                    Message = result != null ? "" : "Nessun fattura trovata",
                    Result = result
                };
                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("VerificaFatturaAsync: " + ex.Message);
                var responseFailed = new Response
                {
                    IsSuccess = false,
                    Message = "Si è verificato un errore durante la verifica della fattura"
                };
                return Json(responseFailed);
            }
        }
    }
    
}
