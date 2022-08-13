using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Mic.UserDb.Bll;
using UserDb.Protos;
using Microsoft.Extensions.Logging;
using Mic.UserDb.Dal;
using DbData.Protos;
using Mic.Core.Entities;
using AutoMapper;

namespace DbData.Service.Services
{
    public class RoleService : RoleServices.RoleServicesBase
    {
        private readonly ILogger<RoleService> logger;
        private readonly IMapper mapper;
        public RoleService(IUserDbContext context, IMapper mapper, ILogger<RoleService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
        }

        public override async Task<ResponseMessage> Add(RoleStruct request, ServerCallContext context)
        {
            
            return new ResponseMessage();
        }
    }
}
