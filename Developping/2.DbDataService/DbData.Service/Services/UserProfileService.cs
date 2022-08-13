using AutoMapper;
using DbData.Bll;
using DbData.Dal;
using DbData.Entities;
using DbData.Entities.Models;
using DbData.Protos;
using Grpc.Core;
using Mic.Core.Entities;
using Mic.UserDb.Bll;
using Mic.UserDb.Dal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Service.Services
{
    public class UserProfileService : UserProfileServices.UserProfileServicesBase
    {
        private readonly ILogger<UserProfileService> logger;
        private readonly IMapper mapper;
        private readonly ExperienceBll experienceBll;
        private readonly LanguageBll languageBll;
        private readonly UserProfileBll userProfileBll;
        private readonly UserFollowBll userFollowBll;

        public UserProfileService(IDbDataContext context, IUserDbContext userContext, IMapper mapper, ILogger<UserProfileService> logger)
        {
            this.logger = logger;
            this.mapper = mapper;
            experienceBll = new ExperienceBll(context);
            languageBll = new LanguageBll(userContext);
            userProfileBll = new UserProfileBll(context);
            userFollowBll = new UserFollowBll(context);
        }

        public override async Task<UserProfileStruct> Get(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
            {
                return new UserProfileStruct();
            }
            try
            {
                var userProfile = await userProfileBll.Get(new Guid(request.Id));
                if (userProfile != null)
                {
                    var result = mapper.Map<UserProfileStruct>(userProfile);
                    return result;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get User Profile {request.Id} Failed! Error: {ex.Message}");
            }
            return new UserProfileStruct();
        }

        public override async Task<ResponseMessage> Add(UserProfileStruct request, ServerCallContext context)
        {
            var result = new ResponseMessage();
            try
            {
                var entry = mapper.Map<EUserProfile>(request);

                await userProfileBll.Add(entry);
                result = new ResponseMessage
                { 
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Add New User Profile Success!"
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add User Profile failed! Error: {ex.Message}");
                result = new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add User Profile failed! {ex.Message}"
                };
            }

            return result;
        }

        public override async Task<ResponseMessage> Edit(UserProfileStruct request, ServerCallContext context)
        {
            ResponseMessage result;
            try
            {
                var entry = mapper.Map<EUserProfile>(request);
                await userProfileBll.Edit(entry);

                result = new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Edit User Profile Success!"
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit User Profile Failed! Error: {ex.Message}");
                result = new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Edit User Profile Failed! {ex.Message}"
                };
            }
            return result;
        }

        public override async Task<ListUserFollowResponse> GetFollowers(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
            {
                return new ListUserFollowResponse();
            }
            try
            {
                var result = await userFollowBll.GetFollowers(new Guid(request.Id));

                if (result != null && result.Data.Count > 0)
                {
                    var response = new ListUserFollowResponse();
                    response.TotalRecords = result.Data.Count;
                    response.Data.Add(mapper.Map<IList<UserFollowStruct>>(result.Data));

                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Followers Of User {request.Id} Failed! Error: {ex.Message}");
            }
            return new ListUserFollowResponse();
        }

        public override async Task<ListUserFollowResponse> GetFollowings(IdRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrEmpty(request.Id))
            {
                return new ListUserFollowResponse();
            }
            try
            {
                var result = await userFollowBll.GetFollowings(new Guid(request.Id));

                if (result != null && result.Data.Count > 0)
                {
                    var response = new ListUserFollowResponse();
                    response.TotalRecords = result.Data.Count;
                    response.Data.Add(mapper.Map<IList<UserFollowStruct>>(result.Data));

                    return response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Followings Of User {request.Id} Failed! Error: {ex.Message}");
            }
            return new ListUserFollowResponse();
        }

        public override async Task<ResponseMessage> AddFollow(UserFollowStruct request, ServerCallContext context)
        {
            var result = new ResponseMessage();

            try
            {
                if (request == null || string.IsNullOrEmpty(request.UserId) || string.IsNullOrEmpty(request.UserFollowingId))
                {
                    return new ResponseMessage
                    {
                        Status = (int)EnumMessageStatus.Danger,
                        StatusCode = "500",
                        Message = $"Invalid Data"
                    };
                }

                if (await userFollowBll.Exists(new Guid(request.UserId), new Guid(request.UserFollowingId)))
                {
                    return new ResponseMessage
                    {
                        Status = (int)EnumMessageStatus.Success,
                        StatusCode = "200",
                        Message = $"Success!"
                    };
                }

                await userFollowBll.Add(new Guid(request.UserId), new Guid(request.UserFollowingId));

                result = new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Success!"
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Add Follow Error: { ex.Message}");
                result = new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Invalid Data"
                };
            }

            return result;
        }

        public override async Task<ResponseMessage> RemoveFollow(UserFollowStruct request, ServerCallContext context)
        {
            var result = new ResponseMessage();

            try
            {
                if (request == null || string.IsNullOrEmpty(request.UserId) || string.IsNullOrEmpty(request.UserFollowingId))
                {
                    return new ResponseMessage
                    {
                        Status = (int)EnumMessageStatus.Danger,
                        StatusCode = "500",
                        Message = $"Invalid Data"
                    };
                }

                if (!await userFollowBll.Exists(new Guid(request.UserId), new Guid(request.UserFollowingId)))
                {
                    return new ResponseMessage
                    {
                        Status = (int)EnumMessageStatus.Success,
                        StatusCode = "200",
                        Message = $"Not Exist!"
                    };
                }

                await userFollowBll.Delete(new Guid(request.UserId), new Guid(request.UserFollowingId));

                result = new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Success,
                    StatusCode = "200",
                    Message = $"Success!"
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Remove Follow Error: { ex.Message}");
                result = new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Invalid Data"
                };
            }

            return result;
        }
    }
}
