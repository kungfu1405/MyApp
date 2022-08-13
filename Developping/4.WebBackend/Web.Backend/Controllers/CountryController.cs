using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using AutoMapper;
using System.Collections.Generic;
using Mic.Core.Entities;
using Mic.Core.Website;
using Web.Backend.Models;
using DbData.Protos;
using DbData.Entities;

namespace Web.Backend.Controllers
{
    public class CountryController : BaseController<CountryController>
    {
        private GrpcChannel channel;
        private CountryServices.CountryServicesClient countryServicesClient;
        private ContinentServices.ContinentServicesClient continentServicesClient;

        public CountryController(IMapper mapper, ILogger<CountryController> logger) : base(logger, mapper)
        {
        }

        public async Task<IActionResult> Index(string msg = "")
        {
            var model = new CountryListViewModel();

            try
            {
                initChanel();
                var continents = await continentServicesClient.GetAllAsync(new EmptyRequest());
                if (continents.Data.Any())
                    model.Continents = mapper.Map<List<EContinent>>(continents.Data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Country List failed!. ERROR: {ex.Message}");
            }

            if (!string.IsNullOrWhiteSpace(msg))
                model.LoadMessageCache(msg);
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetDatatable(CountryListViewModel model)
        {
            try
            {
                initChanel();
                var request = mapper.Map<CountryFilter>(model);
                request.Paging = mapper.Map<PagingType>(model.Pagination);

                var response = await countryServicesClient.GetListAsync(request);
                model.Pagination.Total = response.TotalRecords;
                model.Pagination.Pages = (int)Math.Ceiling(response.TotalRecords * 1.0 / model.Pagination.Perpage);
                if (model.Sort != null)
                {
                    model.Pagination.Field = model.Sort.Field;
                    model.Pagination.Sort = model.Sort.Sort;
                }

                var result = new
                {
                    meta = model.Pagination,
                    data = mapper.Map<List<ECountry>>(response.Data)
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Country List failed!. ERROR: {ex.Message}");
            }
            var emptyResult = new
            {
                meta = new KTPagination(),
                data = new List<ECountry>()
            };
            return Json(emptyResult);
        }

        public async Task<IActionResult> AddEdit(int? id = 0, string msg = "")
        {
            var model = new CountryViewModel();
            try
            {
                initChanel();
                var continents = await continentServicesClient.GetAllAsync(new EmptyRequest());
                if (continents.Data.Any())
                    model.Continents = mapper.Map<List<EContinent>>(continents.Data);

                if (id.HasValue && id > 0)
                {
                    var entry = await countryServicesClient.GetAsync(new IdRequest { Id = id.ToString() });
                    if (entry.Id > 0)
                    {
                        model.Country = mapper.Map<ECountry>(entry);
                        model.ActionMode = FormActionMode.Edit;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"AddEdit:: Get RecordID '{id}' failed!");
                model.AlertError("Data not found!");
            }

            if (!string.IsNullOrWhiteSpace(msg))
                model.LoadMessageCache(msg);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(CountryViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                try
                {
                    initChanel();
                    ResponseMessage response = null;

                    if (FormActionMode.Edit.Equals(model.ActionMode)
                        && "delete".Equals(model.CommandName))
                    {
                        model.ActionMode = FormActionMode.Delete;
                        response = await countryServicesClient.DeleteAsync(new IdRequest { Id = model.Country.Id.ToString() });
                    }
                    else
                    {
                        var request = mapper.Map<CountryStruct>(model.Country);
                        if (FormActionMode.Edit.Equals(model.ActionMode))
                        {
                            response = await countryServicesClient.EditAsync(request);
                        }
                        else
                        {
                            response = await countryServicesClient.AddAsync(request);
                        }
                    }

                    if (response != null)
                    {
                        var alertmsg = mapper.Map<AlertMessage>(response);
                        model.AlertMessages.Add(alertmsg);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"{model.ActionMode} Country failed!");
                    model.AlertError($"{model.ActionMode} Country failed!");
                }
            }
            var msgCode = model.SaveMessageCache();
            if (!model.IsSuccessStatus()
                || "continue".Equals(model.CommandName))
            {
                if (model.ReturnUrl.IndexOf('?') > 0)
                    model.ReturnUrl += "&msg=" + msgCode;
                else
                    model.ReturnUrl += "?msg=" + msgCode;
                return Redirect(model.ReturnUrl);
            }
            return RedirectToAction("Index", new { msg = msgCode });
        }

        public async Task<PartialViewResult> Detail(int id)
        {
            var model = new ECountry();
            try
            {
                initChanel();
                var entry = await countryServicesClient.GetAsync(new IdRequest { Id = id.ToString() });
                if (entry.Id == 0)
                    return PartialView("Error");

                model = mapper.Map<ECountry>(entry);
                if (model == null)
                    return PartialView("Error");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Detail:: Get Country ID '{id}' failed!");
                return PartialView("Error");
            }
            return PartialView(model);
        }

        public async Task<JsonResult> Search(string keyword = "")
        {
            var result = new Select2Datasource();
            try
            {
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    initChanel();
                    var request = new CountryFilter
                    {
                        Name = keyword,
                        Paging = new PagingType { Start = 0, Length = 30 }
                    };
                    var response = await countryServicesClient.GetListAsync(request);
                    if (response.TotalRecords > 0)
                        result.Results = response.Data.Select(e => new Select2DatasourceItem(e.Id.ToString(), $"{e.Name} - {e.Iso2}")).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Search Country '{keyword}' failed!. ERROR: {ex.Message}");
            }
            return Json(result);
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(int id)
        {
            AlertMessage respMsg = null;
            try
            {
                initChanel();
                var response = await countryServicesClient.DeleteAsync(new IdRequest { Id = id.ToString() });
                respMsg = mapper.Map<AlertMessage>(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Country ID '{id}' failed!. ERROR: {ex.Message}");
                respMsg = new AlertMessage
                {
                    StatusValue = EnumMessageStatus.Danger,
                    Message = "Delete record failed!",
                    StatusCode = "500"
                };
            }
            return Json(respMsg);
        }

        private void initChanel()
        {
            channel = new GrpcChannelHepper().CreateDbDataChanel(AccessToken);
            countryServicesClient = new CountryServices.CountryServicesClient(channel);
            continentServicesClient = new ContinentServices.ContinentServicesClient(channel);
        }
    }
}
