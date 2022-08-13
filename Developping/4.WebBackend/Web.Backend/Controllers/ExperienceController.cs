using AutoMapper;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Backend.Commons;
using DbData.Protos;
using UserDb.Protos;
using Mic.Core.Entities;
using DbData.Entities;
using Web.Backend.Models;
using Mic.UserDb.Entities;
using Mic.Core.Website;

namespace Web.Backend.Controllers
{
    public class ExperienceController : BaseController<ExperienceController>
    {
        private GrpcChannel dbChannel;

        private ExperienceServices.ExperienceServicesClient experienceClient;
        private ExperienceSessionServices.ExperienceSessionServicesClient sessionClient;
        private LanguageServices.LanguageServicesClient languageClient;

        public ExperienceController(IMapper mapper, ILogger<ExperienceController> logger) : base(logger, mapper)
        {
        }

        public IActionResult Index(string msg = "")
        {
            var model = new ExperienceListViewModel();

            if (!string.IsNullOrWhiteSpace(msg))
                model.LoadMessageCache(msg);
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetDatatable(ExperienceListViewModel model)
        {
            try
            {
                initChanel();
                var request = mapper.Map<ExperienceFilter>(model);

                var response = await experienceClient.GetListAsync(request);
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
                    data = mapper.Map<List<EExperience>>(response.Data)
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
                data = new List<EExperience>()
            };
            return Json(emptyResult);
        }

        public async Task<IActionResult> AddEdit(Guid? id = null, string msg = "")
        {
            var model = new ExperienceViewModel();
            try
            {
                initChanel();
                var allLangs = await languageClient.GetAllActiveAsync(new EmptyRequest());
                model.Languages = mapper.Map<List<ELanguage>>(allLangs.Languages);

                if (id.HasValue)
                {
                    var experience = await experienceClient.GetByAsync(new IdRequest { Id = id.ToString() });
                    if (string.IsNullOrEmpty(experience.Id))
                        model.Experience = new EExperience();
                    else
                    {
                        model.Experience = mapper.Map<EExperience>(experience);
                        model.ActionMode = FormActionMode.Edit;
                    }
                }

                if (model.Experience.ExperienceLanguages == null)
                    model.Experience.ExperienceLanguages = new List<EExperienceLanguage>();
                foreach(var lang in model.Languages)
                {
                    if (model.Experience.ExperienceLanguages.Any(e => e.LangCode == lang.LangCode))
                        continue;

                    var explang = new EExperienceLanguage { LangCode = lang.LangCode };
                    if (model.ActionMode == FormActionMode.Edit)
                        explang.ExperienceId = model.Experience.Id;
                    model.Experience.ExperienceLanguages.Add(explang);
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
        public async Task<IActionResult> AddEdit(ExperienceViewModel model)
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
                        response = await experienceClient.DeleteAsync(new IdRequest { Id = model.Experience.Id.ToString() });
                    }
                    else
                    {
                        var request = mapper.Map<ExperienceStruct>(model.Experience);
                        if (FormActionMode.Edit.Equals(model.ActionMode))
                        {
                            response = await experienceClient.EditAsync(request);
                        }
                        else
                        {
                            var expResponse = await experienceClient.AddAsync(request);
                            response = expResponse.Message;
                            model.Experience = mapper.Map<EExperience>(expResponse.Experience);
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
                    logger.LogError(ex, $"Update Experience failed!");
                    model.AlertError($"Edit Experience failed!");
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
            var model = new EExperience();
            try
            {
                initChanel();
                var response = await experienceClient.GetAsync(new IdLangRequest { Id = id }); //table ID
                if (string.IsNullOrEmpty(response.Id))
                    return PartialView("Error");

                model = mapper.Map<EExperience>(response);
                if (model == null)
                    return PartialView("Error");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Detail:: Get Experience ID '{id}' failed!");
                return PartialView("Error");
            }
            return PartialView(model);
        }

        private void initChanel()
        {
            dbChannel = new GrpcChannelHepper().CreateDbDataChanel(AccessToken);
            experienceClient = new ExperienceServices.ExperienceServicesClient(dbChannel);
            sessionClient = new ExperienceSessionServices.ExperienceSessionServicesClient(dbChannel);
            languageClient = new LanguageServices.LanguageServicesClient(dbChannel);
        }
    }
}
