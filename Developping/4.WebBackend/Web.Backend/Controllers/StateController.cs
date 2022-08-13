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
    public class StateController : BaseController<StateController>
    {
        private GrpcChannel channel;
        private StateServices.StateServicesClient stateServicesClient;
        private CountryServices.CountryServicesClient countryServicesClient;

        public StateController(IMapper mapper, ILogger<StateController> logger) : base(logger, mapper)
        {
        }

        public async Task<IActionResult> Index(string msg = "")
        {
            var model = new StateListViewModel();

            try
            {
                initChanel();
                var request = new CountryFilter
                {
                    Paging = new PagingType { Start = 0, Length = 500 }
                };
                var response = await countryServicesClient.GetListAsync(request);
                if (response.TotalRecords > 0)
                    model.Countries = mapper.Map<List<ECountry>>(response.Data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get State List failed!. ERROR: {ex.Message}");
            }

            if (!string.IsNullOrWhiteSpace(msg))
                model.LoadMessageCache(msg);
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetDatatable(StateListViewModel model)
        {
            try
            {
                initChanel();
                var request = mapper.Map<StateFilter>(model);
                request.Paging = mapper.Map<PagingType>(model.Pagination);

                var response = await stateServicesClient.GetListAsync(request);
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
                    data = mapper.Map<List<EState>>(response.Data)
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get State List failed!. ERROR: {ex.Message}");
            }
            var emptyResult = new
            {
                meta = new KTPagination(),
                data = new List<EState>()
            };
            return Json(emptyResult);
        }

        public async Task<IActionResult> AddEdit(int? id = 0, string msg = "")
        {
            var model = new StateViewModel();
            try
            {
                initChanel();
                var request = new CountryFilter
                {
                    Paging = new PagingType { Start = 0, Length = 500 }
                };
                var response = await countryServicesClient.GetListAsync(request);
                if (response.TotalRecords > 0)
                    model.Countries = mapper.Map<List<ECountry>>(response.Data);

                if (id.HasValue && id > 0)
                {
                    var entry = await stateServicesClient.GetAsync(new IdRequest { Id = id.ToString() });
                    if (entry.Id > 0)
                    {
                        model.State = mapper.Map<EState>(entry);
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
        public async Task<IActionResult> AddEdit(StateViewModel model)
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
                        response = await stateServicesClient.DeleteAsync(new IdRequest { Id = model.State.Id.ToString() });
                    }
                    else
                    {
                        var request = mapper.Map<StateStruct>(model.State);
                        if (FormActionMode.Edit.Equals(model.ActionMode))
                        {
                            response = await stateServicesClient.EditAsync(request);
                        }
                        else
                        {
                            response = await stateServicesClient.AddAsync(request);
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
                    logger.LogError(ex, $"{model.ActionMode} State failed!");
                    model.AlertError($"{model.ActionMode} State failed!");
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
            var model = new EState();
            try
            {
                initChanel();
                var entry = await stateServicesClient.GetAsync(new IdRequest { Id = id.ToString() });
                if (entry.Id == 0)
                    return PartialView("Error");

                model = mapper.Map<EState>(entry);
                if (model == null)
                    return PartialView("Error");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Detail:: Get State ID '{id}' failed!");
                return PartialView("Error");
            }
            return PartialView(model);
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(int id)
        {
            AlertMessage respMsg = null;
            try
            {
                initChanel();
                var response = await stateServicesClient.DeleteAsync(new IdRequest { Id = id.ToString() });
                respMsg = mapper.Map<AlertMessage>(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete State ID '{id}' failed!. ERROR: {ex.Message}");
                respMsg = new AlertMessage
                {
                    StatusValue = EnumMessageStatus.Danger,
                    Message = "Delete record failed!",
                    StatusCode = "500"
                };
            }
            return Json(respMsg);
        }

        public async Task<JsonResult> Search(string keyword = "", int countryId = 0)
        {
            var result = new Select2Datasource();
            try
            {
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    initChanel();
                    var request = new StateFilter
                    {
                        Name = keyword,
                        CountryId = countryId,
                        Paging = new PagingType { Start = 0, Length = 30 }
                    };
                    var response = await stateServicesClient.GetListAsync(request);
                    if (response.TotalRecords > 0)
                        result.Results = response.Data
                            .Select(e => new Select2DatasourceItem(e.Id.ToString(), e.Name + (string.IsNullOrEmpty(e.Iso2) ? "" : " - " + e.Iso2)))
                            .ToList();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Search Country '{keyword}' failed!. ERROR: {ex.Message}");
            }
            return Json(result);
        }
        private void initChanel()
        {
            channel = new GrpcChannelHepper().CreateDbDataChanel(AccessToken);
            stateServicesClient = new StateServices.StateServicesClient(channel);
            countryServicesClient = new CountryServices.CountryServicesClient(channel);
        }
    }
}
