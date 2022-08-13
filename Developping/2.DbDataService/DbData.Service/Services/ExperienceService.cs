using AutoMapper;
using DbData.Bll;
using DbData.Dal;
using DbData.Entities;
using DbData.Entities.Models;
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
    public class ExperienceService : ExperienceServices.ExperienceServicesBase
    {
        private readonly ILogger<ExperienceService> logger;
        private readonly IMapper mapper;
        private readonly ExperienceBll experienceBll;
        private readonly LanguageBll languageBll;

        public ExperienceService(IDbDataContext context, IUserDbContext userContext, IMapper mapper, ILogger<ExperienceService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            experienceBll = new ExperienceBll(context);
            languageBll = new LanguageBll(userContext);
        }

        public override async Task<ExperienceStruct> Get(IdLangRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
            {
                return new ExperienceStruct();
            }
            try
            {
                var defaultLanguage = await languageBll.GetDefault();
                var defaultLang = defaultLanguage == null ? "" : defaultLanguage.LangCode;
                var result = await experienceBll.Get(request.Id, request.LangCode, defaultLang);
                if (result != null)
                {
                    return mapper.Map<ExperienceStruct>(result);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Experience {request.Id} failed! Error: {ex.Message}");
            }
            return new ExperienceStruct();
        }


        public override async Task<ExperienceStruct> GetBy(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
            {
                return new ExperienceStruct();
            }
            try
            {
                var defaultLanguage = await languageBll.GetDefault();
                var defaultLang = defaultLanguage == null ? "" : defaultLanguage.LangCode;
                var result = await experienceBll.Get(new Guid(request.Id), defaultLang);
                if (result != null)
                {
                    return mapper.Map<ExperienceStruct>(result);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Experience {request.Id} failed! Error: {ex.Message}");
            }
            return new ExperienceStruct();
        }
        public override async Task<ListExperienceResponse> GetList(ExperienceFilter request, ServerCallContext context)
        {
            try
            {
                var defaultLanguage = await languageBll.GetDefault();
                var defaultLang = defaultLanguage == null ? "" : defaultLanguage.LangCode;

                var model = mapper.Map<ExperienceFilters>(request);
                model.DefaultLang = defaultLang;
                if(request.Sort != null)
                {
                    model.Sort.Field = request.Sort.ColumnName;
                    model.Sort.Sort = request.Sort.Direction;
                }               

                var result = await experienceBll.GetList(model);
                if (result != null)
                {
                    var response = new ListExperienceResponse();
                    response.TotalRecords = result.TotalRecords;
                    response.Data.Add(mapper.Map<IList<ExperienceStruct>>(result.Data));
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Experiences failed! Error: {ex.Message}");
            }
            return new ListExperienceResponse();
        }

        public override async Task<ExperienceResponse> Add(ExperienceStruct request, ServerCallContext context)
        {
            var result = new ExperienceResponse();
            try
            {
                var entry = mapper.Map<EExperience>(request);
                var experience = await experienceBll.Add(entry);

                result.Experience = mapper.Map<ExperienceStruct>(experience);
                result.Message = new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add New Experience Success!",
                    Data = experience.Id.ToString()
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add new Experience {request.RouteUri} failed! Error: {ex.Message}");
                result.Message = new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add new Experience failed! {ex.Message}"
                };
            }
            return result;
        }

        public override async Task<ResponseMessage> Edit(ExperienceStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EExperience>(request);
                await experienceBll.Edit(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Edit Experience Success!"
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit Experience {request.Id} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Edit Experience failed! {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Delete(IdRequest request, ServerCallContext context)
        {
            try
            {
                await experienceBll.Delete(new Guid(request.Id));

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Delete Experience Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Experience {request.Id} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Delete Experience failed! {ex.Message}"
                };
            }
        }
    }
}
