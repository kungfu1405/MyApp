using AutoMapper;
using DbData.Bll;
using DbData.Dal;
using DbData.Entities;
using DbData.Entities.Models;
using DbData.Protos;
using Grpc.Core;
using Mic.Core.DataTypes;
using Mic.Core.Entities;
using Mic.UserDb.Bll;
using Mic.UserDb.Dal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Service.Services
{
    public class AttractionService : AttractionServices.AttractionServicesBase
    {
        private readonly ILogger<AttractionService> logger;
        private readonly IMapper mapper;
        private readonly LanguageBll languageBll;
        private readonly AttractionBll attractionBll;
        public AttractionService(IDbDataContext context, IUserDbContext userContext, IMapper mapper, ILogger<AttractionService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            languageBll = new LanguageBll(userContext);
            attractionBll = new AttractionBll(context);
        }

        public override async Task<AttractionStruct> Get(IdLangRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
            {
                return new AttractionStruct();
            }
            try
            {
                var defaultLanguage = await languageBll.GetDefault();
                //var defaultLang = defaultLanguage == null ? "" : defaultLanguage.LangCode;
                var defaultLang = request.LangCode != null ? request.LangCode : defaultLanguage.LangCode;
                var result = await attractionBll.Get(request.Id, request.LangCode, defaultLang);
                if (result != null)
                {
                    return mapper.Map<AttractionStruct>(result);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Attraction {request.Id} failed! Error: {ex.Message}");
            }
            return new AttractionStruct();
        }

        public override async Task<AttractionStruct> GetBy(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
            {
                return new AttractionStruct();
            }
            try
            {
                var result = await attractionBll.GetBy(new Guid(request.Id));
                if (result != null)
                {
                    return mapper.Map<AttractionStruct>(result);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Attraction {request.Id} failed! Error: {ex.Message}");
            }
            return new AttractionStruct();
        }

        public override async Task<ListAttractionResponse> GetList(AttractionFilter request, ServerCallContext context)
        {
            try
            {
                var defaultLanguage = await languageBll.GetDefault();
                var defaultLang = defaultLanguage == null ? "" : defaultLanguage.LangCode;

                var model = mapper.Map<AttractionFilters>(request);
                model.DefaultLang = defaultLang;
                var result = await attractionBll.GetList(model);
                if (result != null)
                {
                    var response = new ListAttractionResponse();
                    response.TotalRecords = result.TotalRecords;
                    response.Data.Add(mapper.Map<IList<AttractionStruct>>(result.Data));
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Attractions failed! Error: {ex.Message}");
            }
            return new ListAttractionResponse();
        }
        public override async Task<ResponseMessage> Add(AttractionStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EAttraction>(request);
                var result = await attractionBll.Add(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add New Attraction {request.DefaultName} Success!",
                    Data = result.Id.ToString()
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add Attraction {request.DefaultName} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add Attraction {request.DefaultName} failed! Error: {ex.InnerException}"
                };
            }
        }

        public override async Task<ResponseMessage> Edit(AttractionStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EAttraction>(request);
                await attractionBll.Edit(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Edit Attraction {entry.DefaultName} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit Attraction {request.DefaultName} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Edit Attraction {request.DefaultName} failed! {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Delete(IdRequest request, ServerCallContext context)
        {
            try
            {
                await attractionBll.Delete(new Guid(request.Id));

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Delete Attraction ID {request.Id} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Attraction {request.Id} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Delete Attraction {request.Id} failed! {ex.Message}"
                };
            }
        }
    }
}
