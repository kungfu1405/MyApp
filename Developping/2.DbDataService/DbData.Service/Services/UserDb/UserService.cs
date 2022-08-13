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
    public class UserService : UserServices.UserServicesBase
    {
        private readonly ILogger<UserService> logger;
        private readonly IMapper mapper;
        private readonly UserBll userBll;
        public UserService(IUserDbContext context, IMapper mapper, ILogger<UserService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            userBll = new UserBll(context);
        }

        public override async Task<UserStruct> Get(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
            {
                return new UserStruct();
            }
            try
            {
                var result = await userBll.Get(new Guid(request.Id));
                if (result != null)
                {
                    return mapper.Map<UserStruct>(result);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get UserID {request.Id} failed! Error: {ex.Message}");
            }
            return new UserStruct();
        }
    }
}
