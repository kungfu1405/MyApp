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
using Mic.Core.Entities;
using Mic.UserDb.Entities;
using DbData.Protos;

namespace DbData.Service.Services
{
    public class LanguageDataLocalService : LanguageDataLocalServices.LanguageDataLocalServicesBase
    {
        private readonly ILogger<LanguageDataLocalService> logger;
        private readonly IMapper mapper;
        private readonly LanguageDataLocalBll languageDataLocalBll;

        public LanguageDataLocalService(IUserDbContext context, IMapper mapper, ILogger<LanguageDataLocalService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            languageDataLocalBll = new LanguageDataLocalBll(context);
        }

        public override async Task<LanguageDataStruct> Get(LanguageDataFilter request, ServerCallContext context)
        {
            try
            {
                var result = await languageDataLocalBll.Get(request.LangKey, request.LangCode);
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
                var result = await languageDataLocalBll.GetList(request.LangKey, request.LangCode);
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
                var entry = mapper.Map<ELanguageDataLocal>(request);
                await languageDataLocalBll.Add(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add Language Key {entry.LangKey} Success!"
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add Language Key {request.LangKey} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add Language Key {request.LangKey} failed! {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Delete(IdRequest request, ServerCallContext context)
        {
            try
            {
                await languageDataLocalBll.Delete(request.Id);

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
