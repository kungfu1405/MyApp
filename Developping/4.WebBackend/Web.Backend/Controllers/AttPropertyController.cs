using AutoMapper;
using DbData.Entities;
using DbData.Protos;
using Grpc.Net.Client;
using Mic.Core.Entities;
using Mic.Core.Website;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Backend.Models;

namespace Web.Backend.Controllers
{
    public class AttPropertyController : BaseController<AttPropertyController>
    {
        private GrpcChannel channel;
        private CityServices.CityServicesClient cityServicesClient;
        private ContinentServices.ContinentServicesClient continentServicesClient;
        private AttPropertyServices.AttPropertyServicesClient attPropertyServicesClient;
        public AttPropertyController(IMapper mapper, ILogger<AttPropertyController> logger) : base(logger, mapper)
        {

        }
        public IActionResult Index(string msg= "")
        {
            var model = new AttPropertyListViewModel();

            if (!string.IsNullOrWhiteSpace(msg))
                model.LoadMessageCache(msg);
            return View(model);
        }
        public async Task<IActionResult> AddEdit(string id, string msg = "")
        {
            
            var model = new AttPropertyViewModel();
            try
            {
                initChanel();
                if (!string.IsNullOrEmpty(id))
                {                    
                    var entry = await attPropertyServicesClient.GetAsync(new IdRequest { Id = id.ToString() });
                    if (!string.IsNullOrEmpty(entry.Id))
                    {
                        model.AttProperty = mapper.Map<EAttProperty>(entry);
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
        public async Task<IActionResult> AddEdit(AttPropertyViewModel model)        
        {
            //string a = model["AttProperty.Name"];
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
                        response = await attPropertyServicesClient.DeleteAsync(new IdRequest { Id = model.AttProperty.Id.ToString() });
                    }
                    else
                    {
                        var request = mapper.Map<AttPropertyStruct>(model.AttProperty);
                        if (FormActionMode.Edit.Equals(model.ActionMode))
                        {
                            response = await attPropertyServicesClient.EditAsync(request);
                        }
                        else
                        {
                            response = await attPropertyServicesClient.AddAsync(request);
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
                    logger.LogError(ex, $"{model.ActionMode} att Property failed!");
                    model.AlertError($"{model.ActionMode} att Property failed!");
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

        [HttpPost]
        public async Task<JsonResult> GetDatatable(AttPropertyListViewModel model)
        {
            try
            {
                initChanel();
       
                var request = mapper.Map<AttPropertyFilter>(model);
                request.Paging = mapper.Map<PagingType>(model.Pagination);
                var response = await attPropertyServicesClient.GetListAsync(request);

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
                    data = mapper.Map<List<EAttProperty>>(response.Data)
                };
                var a = Json(result);
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
        public async Task<PartialViewResult> Detail(string id)
        {
            var model = new EAttProperty();
            try
            {
                initChanel();
                var entry = await attPropertyServicesClient.GetAsync(new IdRequest { Id = id.ToString() });
                if (string.IsNullOrEmpty(entry.Id))
                    return PartialView("Error");

                model = mapper.Map<EAttProperty>(entry);
                if (model == null)
                    return PartialView("Error");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Detail:: Get att Property ID '{id}' failed!");
                return PartialView("Error");
            }
            return PartialView(model);
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(string id)
        {
            AlertMessage respMsg = null;
            try
            {
                initChanel();
                var response = await attPropertyServicesClient.DeleteAsync(new IdRequest { Id = id.ToString() });
                respMsg = mapper.Map<AlertMessage>(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete att Property ID '{id}' failed!. ERROR: {ex.Message}");
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
            attPropertyServicesClient = new AttPropertyServices.AttPropertyServicesClient(channel);
        }
    }
}
