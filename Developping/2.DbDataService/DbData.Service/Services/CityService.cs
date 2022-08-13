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
    public class CityService : CityServices.CityServicesBase
    {
        private readonly ILogger<CityService> logger;
        private readonly IMapper mapper;
        private readonly CityBll cityBll;
        public CityService(IDbDataContext context, IMapper mapper, ILogger<CityService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            cityBll = new CityBll(context);
        }

        public override async Task<CityStruct> Get(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
                return new CityStruct();

            try
            {
                var result = await cityBll.Get(dNum.ToInt(request.Id));
                if (result != null)
                    return mapper.Map<CityStruct>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get City ID {request.Id} failed! Error: {ex.Message}");
            }
            return new CityStruct();
        }

        public override async Task<ListCityResponse> GetList(CityFilter request, ServerCallContext context)
        {
            try
            {
                var filters = mapper.Map<CityFilters>(request);
                var result = await cityBll.GetList(filters);
                if (result != null && result.Data.Any())
                {
                    var response = new ListCityResponse();
                    response.TotalRecords = result.Data.Count();
                    response.Data.Add(mapper.Map<IList<CityStruct>>(result.Data));
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get City list failed! Error: {ex.Message}");
            }
            return new ListCityResponse();
        }

        public override async Task<ResponseMessage> Add(CityStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<ECity>(request);
                var result = await cityBll.Add(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add New City {request.Name} Success!",
                    Data = result.Id.ToString()
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add City {request.Name} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add City {request.Name} failed! Error: {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Edit(CityStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<ECity>(request);
                await cityBll.Edit(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Edit City {entry.Name} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit City {request.Name} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Edit City {request.Name} failed! {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Delete(IdRequest request, ServerCallContext context)
        {
            try
            {
                await cityBll.Delete(dNum.ToInt(request.Id));

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Delete City ID {request.Id} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete City {request.Id} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Delete City {request.Id} failed! {ex.Message}"
                };
            }
        }
    }
}
