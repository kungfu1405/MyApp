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
    public class ExperienceSessionController : BaseController<ExperienceSessionController>
    {
        private GrpcChannel dbChannel;

        private ExperienceSessionServices.ExperienceSessionServicesClient sessionClient;
        private LanguageServices.LanguageServicesClient languageClient;

        public ExperienceSessionController(IMapper mapper, ILogger<ExperienceSessionController> logger) : base(logger, mapper)
        {
        }

        [HttpPost]
        public async Task<JsonResult> GetDatatable(ExperienceSessionListViewModel model)
        {
            try
            {
                initChanel();
                var response = await sessionClient.GetListAsync(new IdLangRequest
                {
                    Id = model.ExperienceId.ToString(),
                    LangCode = CurrentLanguage
                });
                var result = new
                {
                    data = mapper.Map<List<EExperienceSession>>(response.Data)
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get List failed!. ERROR: {ex.Message}");
            }
            var emptyResult = new
            {
                data = new List<EExperience>()
            };
            return Json(emptyResult);
        }


        public async Task<PartialViewResult> AddEdit(Guid eid, Guid? id = null, string msg = "")
        {
            var model = new ExperienceSessionViewModel();
            try
            {
                initChanel();

                var allLangs = await languageClient.GetAllActiveAsync(new EmptyRequest());
                model.Languages = mapper.Map<List<ELanguage>>(allLangs.Languages);

                if (id.HasValue)
                {
                    var response = await sessionClient.GetAllLanguageAsync(new IdRequest { Id = id.ToString() });

                    if (response.Data == null || !response.Data.Any())
                        model.Sessions = new List<EExperienceSession>();
                    else
                    {
                        model.Sessions = mapper.Map<List<EExperienceSession>>(response.Data);
                        model.ActionMode = FormActionMode.Edit;
                    }
                }

                foreach (var lang in model.Languages)
                {
                    if (model.Sessions.Any(e => e.LangCode == lang.LangCode))
                        continue;

                    var session = new EExperienceSession
                    {
                        ExperienceId = eid,
                        LangCode = lang.LangCode
                    };
                    if (model.ActionMode == FormActionMode.Edit)
                    {
                        session.Id = id.Value;
                    }
                    model.Sessions.Add(session);
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

        private void initChanel()
        {
            dbChannel = new GrpcChannelHepper().CreateDbDataChanel(AccessToken);
            sessionClient = new ExperienceSessionServices.ExperienceSessionServicesClient(dbChannel);
            languageClient = new LanguageServices.LanguageServicesClient(dbChannel);
        }
    }
}
