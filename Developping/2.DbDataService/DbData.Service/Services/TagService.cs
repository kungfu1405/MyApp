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
    public class TagService : TagServices.TagServicesBase
    {
        private readonly ILogger<TagService> logger;
        private readonly IMapper mapper;
        private readonly TagBll tagBll;
        public TagService(IDbDataContext context, IMapper mapper, ILogger<TagService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            tagBll = new TagBll(context);
        }

        public override async Task<TagStruct> Get(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
                return new TagStruct();

            try
            {
                var result = await tagBll.Get(Guid.Parse(request.Id));
                if (result != null)
                    return mapper.Map<TagStruct>(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Tag ID {request.Id} failed! Error: {ex.Message}");
            }

            return new TagStruct();
        }

        public override async Task<ResponseMessage> Add(TagStruct request, ServerCallContext context)
        {
            try
            {
                var entry = mapper.Map<ETag>(request);
                var result = await tagBll.Add(entry);

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add New Tag {request.Name} Success!",
                    Data = result.Id.ToString()
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add Tag {request.Name} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add Tag {request.Name} failed! Error: {ex.Message}"
                };
            }
        }

        public override async Task<ResponseMessage> Delete(IdRequest request, ServerCallContext context)
        {
            try
            {
                await tagBll.Delete(Guid.Parse(request.Id));

                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Delete Tag ID {request.Id} Success!",
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Tag {request.Id} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Delete Tag {request.Id} failed! {ex.Message}"
                };
            }
        }
    }
}
