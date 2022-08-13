using AutoMapper;
using DbData.Bll;
using DbData.Dal;
using DbData.Entities;
using DbData.Entities.Models;
using DbData.Protos;
using Grpc.Core;
using Mic.Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbData.Service.Services
{
    public class VehicleService : VehicleServices.VehicleServicesBase
    {
        private readonly ILogger<VehicleService> logger;
        private readonly IMapper mapper;
        private readonly VehicleBll vehicleBll;
        public VehicleService(IDbDataContext context, IMapper mapper, ILogger<VehicleService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            vehicleBll = new VehicleBll(context);
        }
        public override async Task<VehicleStruct> Get(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
                return new VehicleStruct();

            try
            {
                var result = await vehicleBll.Get(new Guid(request.Id));
                if (result != null)
                    return mapper.Map<VehicleStruct>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Vehicle  ID {request.Id} failed! Error: {ex.Message}");
            }
            return new VehicleStruct();
        }

        public override async Task<ListVehicleResponse> GetList(VehicleFilterStruct request, ServerCallContext context)
        {
            try
            {
                var filters = mapper.Map<VehicleFilter>(request);
                var result = await vehicleBll.GetList(filters);
                if (result != null && result.Data.Any())
                {
                    var response = new ListVehicleResponse();
                    response.TotalRecords = result.Data.Count();
                    response.Data.Add(mapper.Map<IList<VehicleStruct>>(result.Data));
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Vehicle  list failed! Error: {ex.Message}");
            }
            return new ListVehicleResponse();
        }

        public override async Task<ResponseMessage> Add(VehicleStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EVehicle>(request);
                var result = await vehicleBll.Add(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add New Vehicle  {request.Name} Success!",
                    Data = result.Id.ToString()
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add Vehicle  {request.Name} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add Vehicle {request.Name} failed! Error: {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Edit(VehicleStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<EVehicle>(request);
                await vehicleBll.Edit(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Edit Vehicle {entry.Name} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit Country {request.Name} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Edit Vehicle  {request.Name} failed! {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Delete(IdRequest request, ServerCallContext context)
        {
            try
            {
                await vehicleBll.Delete(new Guid(request.Id));

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Delete Vehicle ID {request.Id} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Vehicle  {request.Id} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Delete Vehicle  {request.Id} failed! {ex.Message}"
                };
            }
        }
    }

}
