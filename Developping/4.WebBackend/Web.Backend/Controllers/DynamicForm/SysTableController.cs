using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using DynamicData.Protos;
using Web.Backend.Commons;
using DynamicData.Entities;
using Web.Backend.Models.DynamicForm;
using AutoMapper;
using System.Collections.Generic;
using Mic.Core.Entities;
using Mic.Core.DataTypes;
using Mic.Core.Website;

namespace Web.Backend.Controllers.DynamicForm
{
    public class SysTableController : BaseController<SysTableController>
    {
        private GrpcChannel channel;
        private SysTable.SysTableClient tableClient;

        public SysTableController(IMapper mapper, ILogger<SysTableController> logger) : base(logger, mapper)
        {
        }

        public IActionResult Index(string msg = "")
        {
            var model = new SysTableListViewModel();

            if (!string.IsNullOrWhiteSpace(msg))
                model.LoadMessageCache(msg);
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetDatatable(SysTableListViewModel model)
        {
            try
            {
                initChanel();
                var request = mapper.Map<ListTableFilter>(model);
                request.Paging = mapper.Map<PagingType>(model.Pagination);

                var response = await tableClient.GetListAsync(request);
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
                    data = mapper.Map<List<ESysTable>>(response.Tables)
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Table List failed!. ERROR: {ex.Message}");
            }
            var emptyResult = new
            {
                meta = new KTPagination(),
                data = new List<ESysTable>()
            };
            return Json(emptyResult);
        }

        public async Task<IActionResult> Edit(string id, string msg = "")
        {
            var model = new SysTableViewModel();
            try
            {
                initChanel();
                var tableResponse = await tableClient.GetSingleAsync(new IdRequest { Id = id }); //table ID
                if (string.IsNullOrEmpty(tableResponse.Id))
                    return RedirectToAction("Error", "Home");

                model.SysTable = mapper.Map<ESysTable>(tableResponse);
                if (model.SysTable == null)
                    return RedirectToAction("Error", "Home");
                model.ActionMode = FormActionMode.Edit;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit:: Get Table '{id}' failed!");
                model.AlertError("Data not found!");
                return RedirectToAction("Index", new { msg = model.SaveMessageCache() });
            }

            if (!string.IsNullOrWhiteSpace(msg))
                model.LoadMessageCache(msg);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SysTableViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                try
                {
                    if (model.TablePermissions != null)
                    {
                        model.SysTable.Permissions = model.TablePermissions.Sum(p => dNum.ToInt(p));
                    }
                    else
                    {
                        model.SysTable.Permissions = 0;
                    }

                    initChanel();
                    var request = mapper.Map<SysTableStruct>(model.SysTable);
                    var response = await tableClient.EditAsync(request);

                    if (response != null)
                    {
                        var alertmsg = mapper.Map<AlertMessage>(response);
                        model.AlertMessages.Add(alertmsg);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Update data to Table '{model.SysTable.Name}' failed!");
                    model.AlertError($"Edit Table '{model.SysTable.Name}' failed!");
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
            var model = new ESysTable();
            try
            {
                initChanel();
                var tableResponse = await tableClient.GetSingleAsync(new IdRequest { Id = id }); //table ID
                if (string.IsNullOrEmpty(tableResponse.Id))
                    return PartialView("Error");

                model = mapper.Map<ESysTable>(tableResponse);
                if (model == null)
                    return PartialView("Error");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Detail:: Get Table ID '{id}' failed!");
                return PartialView("Error");
            }
            return PartialView(model);
        }

        [HttpPut]
        public async Task<JsonResult> RescanDB()
        {
            try
            {
                initChanel();
                var response = await tableClient.ReScanAsync(new EmptyRequest());
                var result = mapper.Map<AlertMessage>(response);
                return Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Table List failed!. ERROR: {ex.Message}");
            }
            var emptyResult = new AlertMessage
            {
                Status = (int)EnumMessageStatus.Danger,
                StatusValue = EnumMessageStatus.Danger,
                Message = "Rescan DB failed!"
            };
            return Json(emptyResult);
        }

        private void initChanel()
        {
            channel = new GrpcChannelHepper().CreateDDataChanel(AccessToken);
            tableClient = new SysTable.SysTableClient(channel);
        }
    }
}
