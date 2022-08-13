using System;
using System.Threading.Tasks;
using Mic.UserDb.Bll;
using Microsoft.Extensions.Logging;
using Mic.UserDb.Dal;
using Grpc.Core;
using UserDb.Protos;
using DbData.Protos;
using AutoMapper;
using System.Linq;
using Mic.UserDb.Entities;
using System.Collections.Generic;
using Mic.Core.Entities;

namespace DbData.Service.Services
{
    public class LanguageService : LanguageServices.LanguageServicesBase
    {
        private readonly ILogger<LanguageService> logger;
        private readonly IMapper mapper;
        private readonly LanguageBll languageBll;

        public LanguageService(IUserDbContext context, IMapper mapper, ILogger<LanguageService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            languageBll = new LanguageBll(context);
        }

        public override async Task<LanguageStruct> Get(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
                return new LanguageStruct();

            try
            {
                var result = await languageBll.Get(request.Id);
                if (result != null)
                    return mapper.Map<LanguageStruct>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Language {request.Id} failed! Error: {ex.Message}");
            }
            return new LanguageStruct();
        }

        public override async Task<LanguageStruct> GetDefault(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                var result = await languageBll.GetDefault();
                if (result != null)
                    return mapper.Map<LanguageStruct>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Default Language failed! Error: {ex.Message}");
            }
            return new LanguageStruct();
        }

        public override async Task<ListLanguageResponse> GetAll(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                var result = await languageBll.All();
                if (result != null && result.Any())
                {
                    var response = new ListLanguageResponse();
                    response.TotalRecords = result.Count();
                    response.Languages.Add(mapper.Map<IList<LanguageStruct>>(result));
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get All Language failed! Error: {ex.Message}");
            }
            return new ListLanguageResponse();
        }

        public override async Task<ListLanguageResponse> GetAllActive(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                var result = await languageBll.AllActive();
                if (result != null && result.Any())
                {
                    var response = new ListLanguageResponse();
                    response.TotalRecords = result.Count();
                    response.Languages.Add(mapper.Map<IList<LanguageStruct>>(result));
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get All Active Language failed! Error: {ex.Message}");
            }
            return new ListLanguageResponse();
        }

        public override async Task<ResponseMessage> Add(LanguageStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<ELanguage>(request);
                var result = await languageBll.Add(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add New Language {request.LangCode} Success!",
                    Data = result.LangCode
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add new Language {request.LangCode} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add new Language {request.LangCode} failed! {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Edit(LanguageStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<ELanguage>(request);
                await languageBll.Edit(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Edit Language {entry.LangCode} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit Language {request.LangCode} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Edit Language {request.LangCode} failed! {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Delete(IdRequest request, ServerCallContext context)
        {
            try
            {
                await languageBll.Delete(request.Id);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Delete Language {request.Id} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Language {request.Id} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Delete Language {request.Id} failed! {ex.Message}"
                };
            }
        }
    }
}
