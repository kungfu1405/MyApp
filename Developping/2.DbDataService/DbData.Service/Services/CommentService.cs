using AutoMapper;
using DbData.Bll;
using DbData.Dal;
using DbData.Entities.Models;
using DbData.Protos;
using Grpc.Core;
using Mic.UserDb.Bll;
using Mic.UserDb.Dal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Service.Services
{
    public class CommentService : CommentServices.CommentServicesBase
    {
        private readonly ILogger<CommentService> logger;
        private readonly IMapper mapper;
        public CommentService(IUserDbContext context, IMapper mapper, ILogger<CommentService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
        }
    }
}
