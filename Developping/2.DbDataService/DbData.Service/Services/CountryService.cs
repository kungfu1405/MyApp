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
    public class CountryService : CountryServices.CountryServicesBase
    {
        private readonly ILogger<CountryService> logger;
        private readonly IMapper mapper;
        private readonly CountryBll countryBll;
        public CountryService(IDbDataContext context, IMapper mapper, ILogger<CountryService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            countryBll = new CountryBll(context);
        }
        public override async Task<CountryStruct> Get(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
                return new CountryStruct();

            try
            {
                var result = await countryBll.Get(dNum.ToInt(request.Id));
                if (result != null)
                    return mapper.Map<CountryStruct>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Country ID {request.Id} failed! Error: {ex.Message}");
            }
            return new CountryStruct();
        }

        public override async Task<ListCountryResponse> GetList(CountryFilter request, ServerCallContext context)
        {
            try
            {
                var filters = mapper.Map<CountryFilters>(request);
                var result = await countryBll.GetList(filters);
                if (result != null && result.Data.Any())
                {
                    var response = new ListCountryResponse();
                    response.TotalRecords = result.Data.Count();
                    response.Data.Add(mapper.Map<IList<CountryStruct>>(result.Data));
                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Country list failed! Error: {ex.Message}");
            }
            return new ListCountryResponse();
        }

        public override async Task<ResponseMessage> Add(CountryStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<ECountry>(request);
                var result = await countryBll.Add(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add New Country {request.Name} Success!",
                    Data = result.Id.ToString()
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add Country {request.Name} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add Country {request.Name} failed! Error: {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Edit(CountryStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<ECountry>(request);
                await countryBll.Edit(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Edit Country {entry.Name} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit Country {request.Name} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Edit Country {request.Name} failed! {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Delete(IdRequest request, ServerCallContext context)
        {
            try
            {
                await countryBll.Delete(dNum.ToInt(request.Id));

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Delete Country ID {request.Id} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Country {request.Id} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Delete Country {request.Id} failed! {ex.Message}"
                };
            }
        }
    }
}
