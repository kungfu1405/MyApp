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
    public class LanguageController : BaseController<LanguageController>
    {
        private GrpcChannel dbChannel;
        private LanguageServices.LanguageServicesClient languageServicesClient;

        public LanguageController(IMapper mapper, ILogger<LanguageController> logger) : base(logger, mapper)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetDatatable()
        {
            try
            {
                initChanel();

                var response = await languageServicesClient.GetAllAsync(new EmptyRequest());
                var result = new
                {
                    data = mapper.Map<List<ELanguage>>(response.Languages)
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Language List failed!. ERROR: {ex.Message}");
            }
            var emptyResult = new
            {
                data = new List<ELanguage>()
            };
            return Json(emptyResult);
        }

        public async Task<PartialViewResult> AddEdit(string id = "", string msg = "")
        {
            var model = new LanguageViewModel();
            try
            {
                initChanel();

                if (!string.IsNullOrEmpty(id))
                {
                    var entry = await languageServicesClient.GetAsync(new IdRequest { Id = id });
                    if (!string.IsNullOrEmpty(id))
                    {
                        model.Language = mapper.Map<ELanguage>(entry);
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
            return PartialView(model);
        }

        [HttpPost]
        public async Task<JsonResult> AddEdit(LanguageViewModel model)
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
                        response = await languageServicesClient.DeleteAsync(new IdRequest { Id = model.Language.LangCode });
                    }
                    else
                    {
                        var request = mapper.Map<LanguageStruct>(model.Language);
                        if (FormActionMode.Edit.Equals(model.ActionMode))
                        {
                            response = await languageServicesClient.EditAsync(request);
                        }
                        else
                        {
                            response = await languageServicesClient.AddAsync(request);
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
                    logger.LogError(ex, $"{model.ActionMode} Language failed!");
                    model.AlertError($"{model.ActionMode} Language failed!");
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
                var response = await languageServicesClient.DeleteAsync(new IdRequest { Id = id });
                respMsg = mapper.Map<AlertMessage>(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Language '{id}' failed!. ERROR: {ex.Message}");
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
            languageServicesClient = new LanguageServices.LanguageServicesClient(dbChannel);
        }
    }
}
