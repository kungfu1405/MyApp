using AutoMapper;
using DbData.Bll;
using DbData.Dal;
using DbData.Entities;
using DbData.Protos;
using Grpc.Core;
using Mic.Core.Entities;
using Mic.UserDb.Bll;
using Mic.UserDb.Dal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Service.Services
{
    public class ExperienceSessionService : ExperienceSessionServices.ExperienceSessionServicesBase
    {
        private readonly ILogger<ExperienceSessionService> logger;
        private readonly IMapper mapper;
        private readonly ExperienceSessionBll experienceSessionBll;
        private readonly LanguageBll languageBll;
        public ExperienceSessionService(IDbDataContext context, IUserDbContext userContext, IMapper mapper, ILogger<ExperienceSessionService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            experienceSessionBll = new ExperienceSessionBll(context);
            languageBll = new LanguageBll(userContext);
        }

        public override async Task<ExperienceSessionStruct> Get(IdLangRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
            {
                return new ExperienceSessionStruct();
            }
            try
            {
                var defaultLanguage = await languageBll.GetDefault();
                var defaultLang = defaultLanguage == null ? "" : defaultLanguage.LangCode;

                var session = await experienceSessionBll.Get(new Guid(request.Id), request.LangCode, defaultLang);
                if (session != null)
                {
                    var result = mapper.Map<ExperienceSessionStruct>(session);
                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Experience Section {request.Id} failed! Error: {ex.Message}");
            }
            return new ExperienceSessionStruct();
        }

        public override async Task<ListExperienceSessionResponse> GetAllLanguage(IdRequest request, ServerCallContext context)
        {
            try
            {
                var result = await experienceSessionBll.Get(new Guid(request.Id));
                if (result != null)
                {
                    var response = new ListExperienceSessionResponse();
                    response.TotalRecords = result.Count;
                    response.Data.Add(mapper.Map<List<ExperienceSessionStruct>>(result));
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Experience Section failed! Error: {ex.Message}");
            }
            return new ListExperienceSessionResponse();
        }

        public override async Task<ListExperienceSessionResponse> GetList(IdLangRequest request, ServerCallContext context)
        {
            try
            {
                var defaultLanguage = await languageBll.GetDefault();
                var defaultLang = defaultLanguage == null ? "" : defaultLanguage.LangCode;

                var result = await experienceSessionBll.GetList(new Guid(request.Id), request.LangCode, defaultLang);
                if (result != null)
                {
                    var response = new ListExperienceSessionResponse();
                    response.TotalRecords = result.Count;
                    response.Data.Add(mapper.Map<List<ExperienceSessionStruct>>(result));
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Experience Section failed! Error: {ex.Message}");
            }
            return new ListExperienceSessionResponse();
        }

        public override async Task<ExperienceSessionResponse> Add(ExperienceSessionStruct request, ServerCallContext context)
        {
            var result = new ExperienceSessionResponse();
            try
            {
                if (string.IsNullOrWhiteSpace(request.LangCode))
                {
                    var defaultLanguage = await languageBll.GetDefault();
                    if(defaultLanguage == null)
                    {
                        logger.LogError("Default Language doesn't config yet!");
                        result.Message = new ResponseMessage
                        {
                            Status = (int)EnumMessageStatus.Danger,
                            StatusCode = "501",
                            Message = "Default Language does't config yet!"
                        };
                        return result;
                    }
                    request.LangCode = defaultLanguage.LangCode;
                }
                var entry = mapper.Map<EExperienceSession>(request);

                var session = await experienceSessionBll.Add(entry);
                result.Session = mapper.Map<ExperienceSessionStruct>(session);
                result.Message = new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add New Section {request.Title} Success!"
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add new Section {request.Title} failed! Error: {ex.Message}");
                result.Message = new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add new Section {request.Title} failed! {ex.Message}"
                };
            }
            return result;
        }

        public override async Task<ResponseMessage> Edit(ExperienceSessionStruct request, ServerCallContext context)
        {
            ResponseMessage result;
            try
            {
                var entry = mapper.Map<EExperienceSession>(request);
                await experienceSessionBll.Edit(entry);

                result = new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Edit Section {request.Title} Success!"
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit Section {request.Title} failed! Error: {ex.Message}");
                result = new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Edit Section {request.Title} failed! {ex.Message}"
                };
            }
            return result;
        }

        public override async Task<ResponseMessage> Delete(IdRequest request, ServerCallContext context)
        {
            try
            {
                await experienceSessionBll.Delete(new Guid(request.Id));

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Delete Section {request.Id} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Section {request.Id} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Delete Section {request.Id} failed! {ex.Message}"
                };
            }
        }

    }
}
