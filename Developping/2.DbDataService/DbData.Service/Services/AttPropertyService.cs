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
using System.Linq;
using System.Threading.Tasks;


namespace DbData.Service.Services
{
    public class AttPropertyService :  AttPropertyServices.AttPropertyServicesBase  
    {
        private readonly ILogger<AttPropertyService> logger;
        private readonly IMapper mapper;
        private readonly LanguageBll languageBll;
        private readonly AttPropertyBll attPropertyBll;
        public AttPropertyService(IDbDataContext context, IUserDbContext userContext, IMapper mapper, ILogger<AttPropertyService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            languageBll = new LanguageBll(userContext);
            attPropertyBll = new AttPropertyBll(context);
        }
        public override async Task<AttPropertyStruct> Get(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
                return new AttPropertyStruct();

            try
            {
              //  var result = await cityBll.Get(dNum.ToInt(request.Id));
                var result = await attPropertyBll.Get(new Guid(request.Id));
                if (result != null)
                {
                    AttPropertyStruct model = mapper.Map<AttPropertyStruct>(result);

                    return mapper.Map<AttPropertyStruct>(result);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get property ID {request.Id} failed! Error: {ex.Message}");
            }
            return new AttPropertyStruct();
        }

        public override async Task<ResponseMessage> Add(AttPropertyStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EAttProperty>(request);
                var result = await attPropertyBll.Add(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add AttProperty {request.Name} Success!",
                    Data = result.Id.ToString()
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add AttProperty {request.Name} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add Property {request.Name} failed! Error: {ex.Message}"
                };

            }
        }
        public override async Task<ResponseMessage> Edit(AttPropertyStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EAttProperty>(request);
                await attPropertyBll.Edit(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Edit City {entry.Name} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit attribute property {request.Name} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Edit attribute property {request.Name} failed! {ex.Message}"
                };
            }
        }
        public override async Task<ResponseMessage> Delete(IdRequest request, ServerCallContext context)
        {
            try
            {
                await attPropertyBll.Delete(new Guid(request.Id));

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Delete attribute property ID {request.Id} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete attribute property {request.Id} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Delete attribute property {request.Id} failed! {ex.Message}"
                };
            }
        }
        public override async Task<ListAttPropertyResponse> GetList(AttPropertyFilter request, ServerCallContext context)
        {
            try
            {
                var filters = mapper.Map<AttPropertyFillter>(request);
                var result = await attPropertyBll.GetList(filters);
                if (result != null && result.Data.Any())
                {
                    var response = new ListAttPropertyResponse();
                    response.TotalRecords = result.Data.Count();
                    response.Data.Add(mapper.Map<IList<AttPropertyStruct>>(result.Data));
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Property list failed! Error: {ex.Message}");
            }
            return new ListAttPropertyResponse();
        }

    }

}
