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
    public class DestinationLanguageService :DestinationLanguageServices.DestinationLanguageServicesBase
    {
        private readonly ILogger<DestinationLanguageService> logger;
        private readonly IMapper mapper;
        private readonly LanguageBll languageBll;        
        private readonly DestinationLanguageBll destinationLanguageBll;
        public DestinationLanguageService(IDbDataContext context, IUserDbContext userContext, IMapper mapper, ILogger<DestinationLanguageService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            languageBll = new LanguageBll(userContext);
            destinationLanguageBll = new DestinationLanguageBll(context);
        }
        public override async Task<ResponseMessage> Add(DestinationLanguageStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EDestinationLanguage>(request);
                var result = await destinationLanguageBll.Add(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add New Destination language {request.Name} Success!",
                    Data = result.Id.ToString()
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add Destination language {request.Name} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add Destination language {request.Name} failed! Error: {ex.InnerException}"
                };
            }
        }
        public override async Task<DestinationLanguageStruct> Get(IdLangRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
                return new DestinationLanguageStruct();

            try
            {
                var result = await destinationLanguageBll.Get(request.Id, request.LangCode);
                if (result != null)
                    return mapper.Map<DestinationLanguageStruct>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Destination language ID {request.Id} failed! Error: {ex.Message}");
            }
            return new DestinationLanguageStruct();
        }
        public override async Task<DestinationLanguageStruct> GetById(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
                return new DestinationLanguageStruct();

            try
            {
                var result = await destinationLanguageBll.GetById(request.Id);
                if (result != null)
                    return mapper.Map<DestinationLanguageStruct>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Destination language ID {request.Id} failed! Error: {ex.Message}");
            }
            return new DestinationLanguageStruct();
        }

        public override async Task<DestinationLanguageResponse> GetListById(IdRequest request, ServerCallContext context)
        {
            try
            {

                var result = await destinationLanguageBll.GetList(new Guid(request.Id.ToString()));
                if (result != null && result.Any())
                {
                    var response = new DestinationLanguageResponse();
                    response.Data.Add(mapper.Map<IList<DestinationLanguageStruct>>(result));
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Destination language list failed! Error: {ex.Message}");
            }
            return new DestinationLanguageResponse();
        }
        public override async Task<ResponseMessage> Edit(DestinationLanguageStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EDestinationLanguage>(request);
                await destinationLanguageBll.Edit(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Edit Destination language {entry.Name} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit Destination language {request.Name} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Edit Destination language {request.Name} failed! {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Delete(IdRequest request, ServerCallContext context)
        {
            try
            {
                await destinationLanguageBll.Delete(new Guid(request.Id));

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Delete Destination language ID {request.Id} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Destination language {request.Id} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Delete Destination language {request.Id} failed! {ex.Message}"
                };
            }
        }
    }
}
