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
        public async Task<IActionResult> ViewAllAsync()
        {
            _logger.LogInformation("ViewAllAsync START");
            try
            {
                var result = await _contabilitaDal.GetAllPagamentiAsync();
                if (result.Count == 0)
                {
                    var responseFailed = new Response
                    {
                        IsSuccess = false,
                        Message = "Non ci sono Pagamenti da visualizzare"
                    };
                    ViewMessage.Show(this, responseFailed);
                    return View();
                }
                var resultWeb = _mapper.Map<List<Pagamento>>(result);
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

        public IActionResult NewPagamento()
        {
            _logger.LogInformation("NewPagamento START");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewPagamentoAsync(Pagamento pagamento, string importo)
        {
            _logger.LogInformation("NewPagamentoAsync START");
            try
            {
                if (pagamento == null)
                {
                    throw new Exception("pagamento is null");
                }
                pagamento.Importo = decimal.Parse(importo, System.Globalization.CultureInfo.GetCultureInfo("it-IT"));
                var pagamentoInfr = _mapper.Map<Infrastructure.Models.Pagamento>(pagamento);
                var result = await _contabilitaDal.SavePagamentoAsync(pagamentoInfr, Infrastructure.Models.TipoCrud.insert);
                var responseSuccess = new Response
                {
                    IsSuccess = true,
                    Message = "Pagamento registrato correttamente"
                };
                ViewMessage.Show(this, responseSuccess);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("NewPagamentoAsync: " + ex.Message);
                var responseFailed = new Response
                {
                    IsSuccess = false,
                    Message = "Si è verificato un errore durante il salvataggio del pagamento"
                };
                ViewMessage.Show(this, responseFailed);
                return View();
            }
        }

        public IActionResult EditPagamento()
        {
            _logger.LogInformation("EditPagamento START");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditPagamentoAsync(Pagamento pagamento, TipoCrud tipoCrud, string importo)
        {
            _logger.LogInformation("EditPagamentoAsync START");
            try
            {
                if (pagamento == null)
                {
                    throw new Exception("pagamento is null");
                }
                pagamento.Importo = decimal.Parse(importo, System.Globalization.CultureInfo.GetCultureInfo("it-IT"));
                var pagamentoInfr = _mapper.Map<Infrastructure.Models.Pagamento>(pagamento);
                var tipoCrudInfr = _mapper.Map<Infrastructure.Models.TipoCrud>(tipoCrud);
                var result = await _contabilitaDal.SavePagamentoAsync(pagamentoInfr, tipoCrudInfr);
                var message = tipoCrud == TipoCrud.update ? "modificato" : "eliminato";
                var responseSuccess = new Response
                {
                    IsSuccess = true,
                    Message = $"Pagamento {message} correttamente"
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
                    Message = $"Si è verificato un errore durante {message} del Pagamento"
                };
                ViewMessage.Show(this, responseFailed);
                return View();
            }
        }
        public async Task<IActionResult> GetAllPagamentiAsync(string nameSearchFilter, string valueSearchFilter, int page = 1)
        {
            _logger.LogInformation("GetAllPagamentiAsync START");
            var pagination = new Pagination()
            {
                CurrentPage = page,
                ItemsPerPage = 100,
                ParametriRicerca = new Dictionary<string, dynamic>
                {
                },
                Route = new Route { Controller = "Pagamento", Action = "GetAllPagamenti" },
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
                Title = "Elenco pagamenti",
                ColumnNames = new List<string> { "Id", "Modalita", "Data", "Importo", "NumAssegnoBonifico","Descrizione", "Azienda" , "PIVA"},
                Elements = new List<List<object>>()
            };
            try
            {
                var paginationInfr = _mapper.Map<Infrastructure.Models.Paginations.Pagination>(pagination);
                var responseAppCore = await _contabilitaDal.GetAllPagamentiAsync(paginationInfr);
                var result = _mapper.Map<ResultPaginated<Pagamento>>(responseAppCore);
                if (result == null)
                {
                    var responseFailed = new Response
                    {

                        IsSuccess = false,
                        Message = "Si è verificato un errore durante il recupero dei Pagamenti ",
                    };
                    ViewMessage.ShowLocal(this, responseFailed);
                    return PartialView("_GenericTable", genericTable);
                }

                for (var i = 0; i < result.Result.Count; ++i)
                {
                    genericTable.Elements.Add(new List<object>());
                    genericTable.Elements[i].Add(result.Result[i].Id);
                    genericTable.Elements[i].Add(result.Result[i].Modalita);
                    genericTable.Elements[i].Add(result.Result[i].Data.ToString("dd/MM/yyyy"));
                    genericTable.Elements[i].Add(result.Result[i].Importo);
                    genericTable.Elements[i].Add(result.Result[i].NumAssegnoBonifico);
                    genericTable.Elements[i].Add(result.Result[i].Descrizione);
                    genericTable.Elements[i].Add(result.Result[i].AziendaNavigation.RagioneSociale);
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
                result.Pagination.JavascriptNavigationMethod = "ShowDynamicViewAllPagamenti";
                ViewBag.ExistsValueFilter = true;
                ViewBag.ParameterNameSearchFilter = "nameSearchFilter";
                ViewBag.ParameterValueSearchFilter = "valueSearchFilter";
                ViewBag.LabelSearchFilter = nameSearchFilter ?? "azienda";//vedere cosa mettere qua
                ViewBag.InputSearchFilter = valueSearchFilter ?? "";
                ViewBag.SizeModal = "modal-xl";
                return PartialView("_GenericTable", genericTable);
            }
            catch (Exception ex)
            {
                var responseFailed = new Response
                {
                    IsSuccess = false,
                    Message = "Si è verificato un errore durante il recupero dei pagamenti",
                };
                genericTable.Elements = new List<List<object>>();
                ViewMessage.ShowLocal(this, responseFailed);
                _logger.LogError(ex, "GetAllPagamentiAsync");
                return PartialView("_GenericTable", genericTable);
            }
        }

        public async Task<IActionResult> GetPagamentoAsync(string azienda,DateTime? data)
        {
            _logger.LogInformation("VerificaPagamentoAsync START");
            try
            {
                
                if (azienda == null)
                {
                    throw new Exception("azienda is null");
                }
                if (data == null)
                {
                    throw new Exception("data is null");
                }
                var resultInfr = await _contabilitaDal.GetPagamentoAsync(azienda,data);
                var result = _mapper.Map<Pagamento>(resultInfr);
                var response = new Response
                {
                    IsSuccess = result != null,
                    Message = result != null ? "" : "Nessun pagamento trovato",
                    Result = result
                };
                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("VerificaPagamentoAsync: " + ex.Message);
                var responseFailed = new Response
                {
                    IsSuccess = false,
                    Message = "Si è verificato un errore durante la verifica del pagamento"
                };
                return Json(responseFailed);
            }
        }
    }

}