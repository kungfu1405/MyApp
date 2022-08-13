using AutoMapper;
using DbData.Bll;
using DbData.Dal;
using DbData.Entities;
using DbData.Entities.Models;
using DbData.Protos;
using Grpc.Core;
using Mic.Core.DataTypes;
using Mic.Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbData.Service.Services
{
    public class StateService : StateServices.StateServicesBase
    {
        private readonly ILogger<StateService> logger;
        private readonly IMapper mapper;
        private readonly StateBll stateBll;
        public StateService(IDbDataContext context, IMapper mapper, ILogger<StateService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            stateBll = new StateBll(context);
        }
        public override async Task<StateStruct> Get(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
                return new StateStruct();

            try
            {
                var result = await stateBll.Get(dNum.ToInt(request.Id));
                if (result != null)
                    return mapper.Map<StateStruct>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get State ID {request.Id} failed! Error: {ex.Message}");
            }
            return new StateStruct();
        }

        public override async Task<ListStateResponse> GetList(StateFilter request, ServerCallContext context)
        {
            try
            {
                var filters = mapper.Map<StateFilters>(request);
                var result = await stateBll.GetList(filters);
                if (result != null && result.Data.Any())
                {
                    var response = new ListStateResponse();
                    response.TotalRecords = result.Data.Count();
                    response.Data.Add(mapper.Map<IList<StateStruct>>(result.Data));
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get State list failed! Error: {ex.Message}");
            }
            return new ListStateResponse();
        }

        public override async Task<ResponseMessage> Add(StateStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EState>(request);
                var result = await stateBll.Add(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add New State {request.Name} Success!",
                    Data = result.Id.ToString()
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add State {request.Name} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add State {request.Name} failed! Error: {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Edit(StateStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EState>(request);
                await stateBll.Edit(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Edit State {entry.Name} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit State {request.Name} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Edit State {request.Name} failed! {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Delete(IdRequest request, ServerCallContext context)
        {
            try
            {
                await stateBll.Delete(dNum.ToInt(request.Id));

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Delete State ID {request.Id} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete State {request.Id} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Delete State {request.Id} failed! {ex.Message}"
                };
            }
        }
    }
}
