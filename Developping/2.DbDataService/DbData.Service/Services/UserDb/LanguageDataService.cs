using System;
using System.Threading.Tasks;
using Mic.UserDb.Bll;
using Microsoft.Extensions.Logging;
using Mic.UserDb.Dal;
using Grpc.Core;
using UserDb.Protos;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using DbData.Protos;
using Mic.Core.Entities;
using Mic.UserDb.Entities;

namespace DbData.Service.Services
{
    public class LanguageDataService : LanguageDataServices.LanguageDataServicesBase
    {
        private readonly ILogger<LanguageDataService> logger;
        private readonly IMapper mapper;
        private readonly LanguageDataBll languageDataBll;

        public LanguageDataService(IUserDbContext context, IMapper mapper, ILogger<LanguageDataService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            languageDataBll = new LanguageDataBll(context);
        }

        public override async Task<LanguageDataStruct> Get(LanguageDataFilter request, ServerCallContext context)
        {
            try
            {
                var result = await languageDataBll.Get(request.LangKey, request.LangCode);
                if (result != null)
                    return mapper.Map<LanguageDataStruct>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Language Key {request.LangCode}-{request.LangKey} failed! Error: {ex.Message}");
            }
            return new LanguageDataStruct();
        }

        public override async Task<ListLanguageDataResponse> GetList(LanguageDataFilter request, ServerCallContext context)
        {
            try
            {
                var result = await languageDataBll.GetList(request.LangKey, request.LangCode, request.IsGroupOnly);
                if (result != null && result.Any())
                {
                    var response = new ListLanguageDataResponse();
                    response.TotalRecords = result.Count();
                    response.Data.Add(mapper.Map<IList<LanguageDataStruct>>(result));
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Language Keys {request.LangCode}-{request.LangKey} failed! Error: {ex.Message}");
            }
            return new ListLanguageDataResponse();
        }

        public override async Task<ResponseMessage> Add(LanguageDataStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<ELanguageData>(request);
                var result = await languageDataBll.Add(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add New Language Key {entry.LangKey} Success!",
                    Data = result.LangKey
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add new Language Key {request.LangKey} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add new Language Key {request.LangKey} failed! {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Edit(LanguageDataStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<ELanguageData>(request);
                await languageDataBll.Edit(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Edit Language Key {entry.LangKey} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit Language Key {request.LangKey} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Edit Language Key {request.LangKey} failed! {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Delete(IdRequest request, ServerCallContext context)
        {
            try
            {
                await languageDataBll.Delete(request.Id);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Delete Language Key {request.Id} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Language Key {request.Id} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Delete Language Key {request.Id} failed! {ex.Message}"
                };
            }
        }
    }
}
