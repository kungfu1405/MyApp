using AutoMapper;
using Mic.UserDb.Dal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbData.Service.Services
{
    public class WebNavigatorService
    {
        private readonly ILogger<WebNavigatorService> logger;
        private readonly IMapper mapper;
        public WebNavigatorService(IUserDbContext context, IMapper mapper, ILogger<WebNavigatorService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
        }
    }
}
