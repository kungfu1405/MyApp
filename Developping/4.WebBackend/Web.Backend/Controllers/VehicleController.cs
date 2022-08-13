using AutoMapper;
using DbData.Entities;
using DbData.Protos;
using Grpc.Net.Client;
using Mic.Core.Entities;
using Mic.Core.Website;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Backend.Models;

namespace Web.Backend.Controllers
{
    public class VehicleController : BaseController<VehicleController>
    {
        private GrpcChannel channel;
        //private CityServices.CityServicesClient cityServicesClient;
        private VehicleServices.VehicleServicesClient vehicleServicesClient;

        public VehicleController(IMapper mapper, ILogger<VehicleController> logger) : base(logger, mapper)
        {
        }

        public IActionResult Index(string msg = "")
        {
            var model = new VehicleListViewModel();

            if (!string.IsNullOrWhiteSpace(msg))
                model.LoadMessageCache(msg);
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetDatatable(VehicleListViewModel model)
        {
            try
            {
                initChanel();
                var request = mapper.Map<VehicleFilterStruct>(model);
                request.Paging = mapper.Map<PagingType>(model.Pagination);

                var response = await vehicleServicesClient.GetListAsync(request);
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
                    data = mapper.Map<List<EVehicle>>(response.Data)
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Vehicle List failed!. ERROR: {ex.Message}");
            }
            var emptyResult = new
            {
                meta = new KTPagination(),
                data = new List<EVehicle>()
            };
            return Json(emptyResult);
        }

        public async Task<IActionResult> AddEdit(string id , string msg = "")
        {
            var model = new VehicleViewModel();
            try
            {
                initChanel();
                if (!string.IsNullOrEmpty(id))
                {
                    var entry = await vehicleServicesClient.GetAsync(new IdRequest { Id = id.ToString() });
                    if (!string.IsNullOrEmpty(entry.Id.ToString()))
                    {
                        model.Vehicle= mapper.Map<EVehicle>(entry);
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
        public async Task<IActionResult> AddEdit(VehicleViewModel model)
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
                        response = await vehicleServicesClient.DeleteAsync(new IdRequest { Id = model.Vehicle.Id.ToString() });
                    }
                    else
                    {
                        var request = mapper.Map<VehicleStruct>(model.Vehicle);
                        if (FormActionMode.Edit.Equals(model.ActionMode))
                        {
                            response = await vehicleServicesClient.EditAsync(request);
                        }
                        else
                        {
                            response = await vehicleServicesClient.AddAsync(request);
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
                    logger.LogError(ex, $"{model.ActionMode} Vehicle failed!");
                    model.AlertError($"{model.ActionMode} Vehicle failed!");
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

        public async Task<PartialViewResult> Detail(string id)
        {
            var model = new EVehicle();
            try
            {
                initChanel();
                var entry = await vehicleServicesClient.GetAsync(new IdRequest { Id = id.ToString() });
                if (string.IsNullOrEmpty(entry.Id.ToString()))
                    return PartialView("Error");

                model = mapper.Map<EVehicle>(entry);
                if (model == null)
                    return PartialView("Error");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Detail:: Get Vehicle ID '{id}' failed!");
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
                var response = await vehicleServicesClient.DeleteAsync(new IdRequest { Id = id.ToString() });
                respMsg = mapper.Map<AlertMessage>(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Vehicle ID '{id}' failed!. ERROR: {ex.Message}");
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
            vehicleServicesClient = new VehicleServices.VehicleServicesClient(channel);
        }
    }
}
