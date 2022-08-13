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
using System.Linq;
using System.Threading.Tasks;

namespace DbData.Service.Services
{
    public class AttractionLanguageService  : AttractionLanguageServices.AttractionLanguageServicesBase
    {
        private readonly ILogger<AttractionLanguageService> logger;
        private readonly IMapper mapper;
        private readonly LanguageBll languageBll;
        private readonly AttractionLanguageBll attractionLanguageBll;        
        public AttractionLanguageService(IDbDataContext context, IUserDbContext userContext, IMapper mapper, ILogger<AttractionLanguageService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            languageBll = new LanguageBll(userContext);
            attractionLanguageBll = new AttractionLanguageBll(context);
        }
        public override async Task<ResponseMessage> Add(AttractionLanguageStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EAttractionLanguage>(request);
                var result = await attractionLanguageBll.Add(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add New Attraction language {request.Name} Success!",
                    Data = result.Id.ToString()
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add Attraction language {request.Name} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add Attraction language {request.Name} failed! Error: {ex.InnerException}"
                };
            }
        }
        public override async Task<AttractionLanguageStruct> Get(IdLangRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
                return new AttractionLanguageStruct();

            try
            {
                var result = await attractionLanguageBll.Get(request.Id , request.LangCode);
                if (result != null)
                    return mapper.Map<AttractionLanguageStruct>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Attraction language ID {request.Id} failed! Error: {ex.Message}");
            }
            return new AttractionLanguageStruct();
        }
        public override async Task<AttractionLanguageStruct> GetById(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
                return new AttractionLanguageStruct();

            try
            {
                var result = await attractionLanguageBll.GetById(request.Id);
                if (result != null)
                    return mapper.Map<AttractionLanguageStruct>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Attraction language ID {request.Id} failed! Error: {ex.Message}");
            }
            return new AttractionLanguageStruct();
        }

        public override async Task<AttractionLanguageResponse> GetListById(IdRequest request, ServerCallContext context)
        {
            try
            {

                var result = await attractionLanguageBll.GetList(new Guid(request.Id.ToString()));
                if (result != null && result.Any())
                {
                    var response = new AttractionLanguageResponse();
                    response.Data.Add(mapper.Map<IList<AttractionLanguageStruct>>(result));
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Attraction language list failed! Error: {ex.Message}");
            }
            return new AttractionLanguageResponse();
        }
        public override async Task<ResponseMessage> Edit(AttractionLanguageStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EAttractionLanguage>(request);
                await attractionLanguageBll.Edit(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Edit Attraction language {entry.Name} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit Attraction language {request.Name} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Edit Attraction language {request.Name} failed! {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Delete(IdRequest request, ServerCallContext context)
        {
            try
            {
                await attractionLanguageBll.Delete(new Guid(request.Id));

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Delete Attraction language ID {request.Id} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Attraction language {request.Id} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Delete Attraction language {request.Id} failed! {ex.Message}"
                };
            }
        }
    }
}
