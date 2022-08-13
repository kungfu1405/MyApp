using AutoMapper;
using Mic.UserDb.Dal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbData.Service.Services
{
    public class UsergroupService
    {
        private readonly ILogger<UsergroupService> logger;
        private readonly IMapper mapper;
        public UsergroupService(IUserDbContext context, IMapper mapper, ILogger<UsergroupService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
        }
    }
}
