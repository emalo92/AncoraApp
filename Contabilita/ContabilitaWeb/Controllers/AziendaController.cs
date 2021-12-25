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

        public async Task<IActionResult> GetAllAziendeAsync(string nameSearchFilter, string valueSearchFilter, int page = 1)
        {
            _logger.LogInformation("GetAllAziendeAsync START");
            var pagination = new Pagination()
            {
                CurrentPage = page,
                ItemsPerPage = 100,
                ParametriRicerca = new Dictionary<string, dynamic>
                {
                },
                Route = new Route { Controller = "Azienda", Action = "GetAllAziende" },
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
                Title = "Elenco aziende",
                ColumnNames = new List<string> { "Id", "Partita IVA", "Ragione Sociale", "Email", "Telefono", "Iban" },
                Elements = new List<List<object>>()
            };
            try
            {
                var paginationInfr = _mapper.Map<Infrastructure.Models.Paginations.Pagination>(pagination);
                var responseAppCore = await _contabilitaDal.GetAllAziendeAsync(paginationInfr);
                var result = _mapper.Map<ResultPaginated<Azienda>>(responseAppCore);
                if (result == null)
                {
                    var responseFailed = new Response
                    {

                        IsSuccess = false,
                        Message = "Si è verificato un errore durante il recupero delle aziende ",
                    };
                    ViewMessage.ShowLocal(this, responseFailed);
                    return PartialView("_GenericTable", genericTable);
                }

                for (var i = 0; i < result.Result.Count; ++i)
                {
                    genericTable.Elements.Add(new List<object>());
                    genericTable.Elements[i].Add(result.Result[i].Id);
                    genericTable.Elements[i].Add(result.Result[i].PartitaIva);
                    genericTable.Elements[i].Add(result.Result[i].RagioneSociale);
                    genericTable.Elements[i].Add(result.Result[i].Email);
                    genericTable.Elements[i].Add(result.Result[i].Telefono);
                    genericTable.Elements[i].Add(result.Result[i].Iban);
                }

                result.Pagination.ParametriRicerca = new Dictionary<string, dynamic> { { "page", page } };
                if (nameSearchFilter != null && valueSearchFilter != null)
                {
                    result.Pagination.ParametriRicerca.Add("nameSearchFilter", nameSearchFilter);
                    result.Pagination.ParametriRicerca.Add("valueSearchFilter", valueSearchFilter);
                }
                result.Pagination.Route = pagination.Route;
                ViewBag.Pagination = result.Pagination;
                result.Pagination.JavascriptNavigationMethod = "ShowDynamicViewAllAziende";
                ViewBag.ExistsValueFilter = true;
                ViewBag.ParameterNameSearchFilter = "nameSearchFilter";
                ViewBag.ParameterValueSearchFilter = "valueSearchFilter";
                ViewBag.LabelSearchFilter = nameSearchFilter ?? "RagioneSociale";
                ViewBag.InputSearchFilter = valueSearchFilter ?? "";
                ViewBag.SizeModal = "modal-ultraxl";
                return PartialView("_GenericTable", genericTable);
            }
            catch (Exception ex)
            {
                var responseFailed = new Response
                {
                    IsSuccess = false,
                    Message = "Si è verificato un errore durante il recupero delle aziende",
                };
                genericTable.Elements = new List<List<object>>();
                ViewMessage.ShowLocal(this, responseFailed);
                _logger.LogError(ex, "GetAllAziendeAsync");
                return PartialView("_GenericTable", genericTable);
            }
        }

        public async Task<IActionResult> GetAziendaAsync(string partitaIva)
        {
            _logger.LogInformation("VerificaAziendaAsync START");
            try
            {
                if (partitaIva == null)
                {
                    throw new Exception("partitaIva is null");
                }
                var resultInfr = await _contabilitaDal.GetAziendaAsync(partitaIva);
                var result = _mapper.Map<Azienda>(resultInfr);
                var response = new Response
                {
                    IsSuccess = result != null,
                    Message = result != null ? "" : "Nessun azienda trovata",
                    Result = result
                };
                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("VerificaAziendaAsync: " + ex.Message);
                var responseFailed = new Response
                {
                    IsSuccess = false,
                    Message = "Si è verificato un errore durante la verifica dell'azienda"
                };
                return Json(responseFailed);
            }
        }
    }
}
