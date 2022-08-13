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
    public class CityController : BaseController<CityController>
    {
        private GrpcChannel channel;
        private CityServices.CityServicesClient cityServicesClient;
        private ContinentServices.ContinentServicesClient continentServicesClient;

        public CityController(IMapper mapper, ILogger<CityController> logger) : base(logger, mapper)
        {
        }

        public IActionResult Index(string msg = "")
        {
            var model = new CityListViewModel();

            if (!string.IsNullOrWhiteSpace(msg))
                model.LoadMessageCache(msg);
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetDatatable(CityListViewModel model)
        {
            try
            {
                initChanel();
                var request = mapper.Map<CityFilter>(model);
                request.Paging = mapper.Map<PagingType>(model.Pagination);

                var response = await cityServicesClient.GetListAsync(request);
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
                    data = mapper.Map<List<ECity>>(response.Data)
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get City List failed!. ERROR: {ex.Message}");
            }
            var emptyResult = new
            {
                meta = new KTPagination(),
                data = new List<ECity>()
            };
            return Json(emptyResult);
        }

        public async Task<IActionResult> AddEdit(int? id = 0, string msg = "")
        {
            var model = new CityViewModel();
            try
            {
                initChanel();
                if (id.HasValue && id > 0)
                {
                    var entry = await cityServicesClient.GetAsync(new IdRequest { Id = id.ToString() });
                    if (entry.Id > 0)
                    {
                        model.City = mapper.Map<ECity>(entry);
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
        public async Task<IActionResult> AddEdit(CityViewModel model)
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
                        response = await cityServicesClient.DeleteAsync(new IdRequest { Id = model.City.Id.ToString() });
                    }
                    else
                    {
                        var request = mapper.Map<CityStruct>(model.City);
                        if (FormActionMode.Edit.Equals(model.ActionMode))
                        {
                            response = await cityServicesClient.EditAsync(request);
                        }
                        else
                        {
                            //response = await cityServicesClient.AddAsync(request);
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
                    logger.LogError(ex, $"{model.ActionMode} City failed!");
                    model.AlertError($"{model.ActionMode} City failed!");
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
            var model = new ECity();
            try
            {
                initChanel();
                var entry = await cityServicesClient.GetAsync(new IdRequest { Id = id.ToString() });
                if (entry.Id == 0)
                    return PartialView("Error");

                model = mapper.Map<ECity>(entry);
                if (model == null)
                    return PartialView("Error");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Detail:: Get City ID '{id}' failed!");
                return PartialView("Error");
            }
            return PartialView(model);
        }
        public async Task<JsonResult> Search(string keyword = "" , int stateId=0)
        {
            var result = new Select2Datasource();
            try
            {
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    initChanel();
                    var request = new CityFilter
                    {
                        Name = keyword,
                        StateId = stateId,
                        Paging = new PagingType { Start = 0, Length = 30 }
                    };
                    var response = await cityServicesClient.GetListAsync(request);
                    if (response.TotalRecords > 0)
                        result.Results = response.Data.Select(e => new Select2DatasourceItem(e.Id.ToString(), $"{e.Name} ")).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Search City '{keyword}' failed!. ERROR: {ex.Message}");
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
                var response = await cityServicesClient.DeleteAsync(new IdRequest { Id = id.ToString() });
                respMsg = mapper.Map<AlertMessage>(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete City ID '{id}' failed!. ERROR: {ex.Message}");
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
            cityServicesClient = new CityServices.CityServicesClient(channel);
            continentServicesClient = new ContinentServices.ContinentServicesClient(channel);
        }
    }
}
