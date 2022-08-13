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
    public class DestinationService : DestinationServices.DestinationServicesBase
    {
        private readonly ILogger<DestinationService> logger;
        private readonly IMapper mapper;
        private readonly LanguageBll languageBll;
        private readonly DestinationBll destinationBll;
        public DestinationService(IDbDataContext context, IUserDbContext userContext, IMapper mapper, ILogger<DestinationService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            languageBll = new LanguageBll(userContext);
            destinationBll = new DestinationBll(context);
        }

        public override async Task<DestinationStruct> Get(IdLangRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
            {
                return new DestinationStruct();
            }
            try
            {
                var defaultLanguage = await languageBll.GetDefault();
                var defaultLang = defaultLanguage == null ? "" : defaultLanguage.LangCode;
                var result = await destinationBll.Get(request.Id, request.LangCode, defaultLang);
                if (result != null)
                {
                    return mapper.Map<DestinationStruct>(result);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Destination {request.Id} failed! Error: {ex.Message}");
            }
            return new DestinationStruct();
        }

        public override async Task<ListDestinationResponse> GetList(DestinationFilter request, ServerCallContext context)
        {
            try
            {
                var defaultLanguage = await languageBll.GetDefault();
                var defaultLang = defaultLanguage == null ? "" : defaultLanguage.LangCode;

                var model = mapper.Map<DestinationFilters>(request);
                model.DefaultLang = (request.LangCode != null ? request.LangCode : defaultLang );
                var result = await destinationBll.GetList(model);
                if (result != null)
                {
                    var response = new ListDestinationResponse();
                    response.TotalRecords = result.TotalRecords;
                    response.Data.Add(mapper.Map<IList<DestinationStruct>>(result.Data));
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Destinations failed! Error: {ex.Message}");
            }
            return new ListDestinationResponse();
        }

        public override async Task<DestinationStruct> GetBy(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
            {
                return new DestinationStruct();
            }
            try
            {
                var result = await destinationBll.Get(new Guid(request.Id));
                if (result != null)
                {
                    return mapper.Map<DestinationStruct>(result);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Destination {request.Id} failed! Error: {ex.Message}");
            }
            return new DestinationStruct();
        }
        public override async Task<ResponseMessage> Add(DestinationStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EDestination>(request);
                var result = await destinationBll.Add(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add New Destination {request.DefaultName} Success!", // FK_Destination_City_CityId
                    Data = result.Id.ToString()
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add Destination {request.DefaultName} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add Destination {request.DefaultName} failed! Error: {ex.Message}"
                };
            }
        }
        public override async Task<ResponseMessage> Edit(DestinationStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EDestination>(request);
                await destinationBll.Edit(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Edit Destination {entry.DefaultName} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit Destination {request.DefaultName} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Edit Destination {request.DefaultName} failed! {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Delete(IdRequest request, ServerCallContext context)
        {
            try
            {
                await destinationBll.Delete(new Guid(request.Id));

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Delete Destination ID {request.Id} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Destination {request.Id} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Delete Destination {request.Id} failed! {ex.Message}"
                };
            }
        }
    }
}
