using System;
using System.Threading.Tasks;
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
using Mic.WebpageDb.Bll;

namespace DbData.Service.Services
{
    public class WebPageService : WebPageServices.WebPageServicesBase
    {
        private readonly ILogger<WebPageService> logger;
        private readonly IMapper mapper;
        private readonly WebPageBll webPageBll;

        public WebPageService(IUserDbContext context, IMapper mapper, ILogger<WebPageService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            webPageBll = new WebPageBll(context);
        }

        public override async Task<WebPageStruct> Get(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
                return new WebPageStruct();

            try
            {
                var result = await webPageBll.Get(Guid.Parse(request.Id));
                if (result != null)
                    return mapper.Map<WebPageStruct>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Webpage {request.Id} failed! Error: {ex.Message}");
            }
            return new WebPageStruct();
        }

        public override async Task<ListWebPageResponse> GetList(WebPageFilter request, ServerCallContext context)
        {
            try
            {
                var result = await webPageBll.GetListBy(request.Name, 
                                string.IsNullOrEmpty(request.WebControllerId) ? null : Guid.Parse(request.WebControllerId));
                if (result != null && result.Any())
                {
                    var response = new ListWebPageResponse();
                    response.TotalRecords = result.Count();
                    response.WebPages.Add(mapper.Map<IList<WebPageStruct>>(result));
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get GetList Keys {request.Name}-{request.WebControllerId} failed! Error: {ex.Message}");
            }
            return new ListWebPageResponse();
        }

        public override async Task<ResponseMessage> Add(WebPageStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EWebPage>(request);
                var result = await webPageBll.Add(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add New Webapge {request.Action} - Controller {request.WebControllerId} Success!",
                    Data = result.Id.ToString()
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add new Language {request.Action} - Controller {request.WebControllerId} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add new Language {request.Action} - Controller {request.WebControllerId} failed! {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Edit(WebPageStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EWebPage>(request);
                await webPageBll.Edit(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Edit Language {entry.Id} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit Language {request.Id} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Edit Language {request.Id} failed! {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Delete(IdRequest request, ServerCallContext context)
        {
            try
            {
                await webPageBll.Delete(Guid.Parse(request.Id));

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
