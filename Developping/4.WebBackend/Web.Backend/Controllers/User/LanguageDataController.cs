using AutoMapper;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DbData.Protos;
using UserDb.Protos;
using Mic.Core.Entities;
using Mic.UserDb.Entities;
using Mic.Core.Website;
using Web.Backend.Models.User;

namespace Web.Backend.Controllers.User
{
    public class LanguageDataController : BaseController<LanguageDataController>
    {
        private GrpcChannel dbChannel;
        private LanguageDataServices.LanguageDataServicesClient languageDataServicesClient;

        public LanguageDataController(IMapper mapper, ILogger<LanguageDataController> logger) : base(logger, mapper)
        {
        }

        public IActionResult Index(string id)
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetDatatable(LanguageDataListViewModel model)
        {
            try
            {
                initChanel();
                var request = mapper.Map<LanguageDataFilter>(model);
                //request.Paging = mapper.Map<PagingType>(model.Pagination);

                var response = await languageDataServicesClient.GetListAsync(new LanguageDataFilter());
                var result = new
                {
                    data = mapper.Map<List<ELanguageData>>(response.Data)
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get LanguageData List failed!. ERROR: {ex.Message}");
            }
            var emptyResult = new
            {
                data = new List<ELanguageData>()
            };
            return Json(emptyResult);
        }

        public async Task<PartialViewResult> AddEdit(string id = "", string msg = "")
        {
            var model = new LanguageDataViewModel();
            try
            {
                initChanel();

                //if (!string.IsNullOrEmpty(id))
                //{
                //    var entry = await languageDataServicesClient.GetAsync(new IdRequest { Id = id });
                //    if (!string.IsNullOrEmpty(id))
                //    {
                //        model.LanguageData = mapper.Map<ELanguageData>(entry);
                //        model.ActionMode = FormActionMode.Edit;
                //    }
                //}
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"AddEdit:: Get RecordID '{id}' failed!");
                model.AlertError("Data not found!");
            }

            if (!string.IsNullOrWhiteSpace(msg))
                model.LoadMessageCache(msg);
            return PartialView(model);
        }

        [HttpPost]
        public async Task<JsonResult> AddEdit(LanguageDataViewModel model)
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
                        response = await languageDataServicesClient.DeleteAsync(new IdRequest { Id = model.LanguageData.LangCode });
                    }
                    else
                    {
                        var request = mapper.Map<LanguageDataStruct>(model.LanguageData);
                        if (FormActionMode.Edit.Equals(model.ActionMode))
                        {
                            response = await languageDataServicesClient.EditAsync(request);
                        }
                        else
                        {
                            response = await languageDataServicesClient.AddAsync(request);
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
                    logger.LogError(ex, $"{model.ActionMode} LanguageData failed!");
                    model.AlertError($"{model.ActionMode} LanguageData failed!");
                }
            }
            return Json(model.AlertMessages);
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(string id)
        {
            AlertMessage respMsg = null;
            try
            {
                initChanel();
                var response = await languageDataServicesClient.DeleteAsync(new IdRequest { Id = id });
                respMsg = mapper.Map<AlertMessage>(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete LanguageData '{id}' failed!. ERROR: {ex.Message}");
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
            dbChannel = new GrpcChannelHepper().CreateDbDataChanel(AccessToken);
            languageDataServicesClient = new LanguageDataServices.LanguageDataServicesClient(dbChannel);
        }
    }
}
