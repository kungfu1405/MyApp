using AutoMapper;
using DbData.Bll;
using DbData.Dal;
using DbData.Protos;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Service.Services
{
    public class ContinentService : ContinentServices.ContinentServicesBase
    {
        private readonly ILogger<ContinentService> logger;
        private readonly IMapper mapper;
        private readonly ContinentBll continentBll;
        public ContinentService(IDbDataContext context, IMapper mapper, ILogger<ContinentService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            continentBll = new ContinentBll(context);
        }
        public override async Task<ListContinentResponse> GetAll(EmptyRequest request, ServerCallContext context)
        {
            var response = new ListContinentResponse();
            try
            {
                var result = await continentBll.All();
                if (result != null)
                {
                    response.Data.Add(mapper.Map<IList<ContinentStruct>>(result));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Continents failed! Error: {ex.Message}");
            }
            return response;
        }
    }
}
