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
using DbData.Entities.Models;
using AutoMapper;
using Mic.UserDb.Entities;

namespace DbData.Service.Services
{
    public class UserAuthenticateService : UserAuthenticateServices.UserAuthenticateServicesBase
    {
        private readonly ILogger<UserAuthenticateService> logger;
        private readonly UserBll userBll;
     

        public UserAuthenticateService(IUserDbContext context, ILogger<UserAuthenticateService> logger)
        {
            this.logger = logger;
            userBll = new UserBll(context);         
        }

        public override async Task<SigninResponse> Signin(SigninRequest request, ServerCallContext context)
        {
            var response = new SigninResponse();
            try
            {
                var user = await userBll.Get(request.UserName, true);
                if (user != null)
                {
                    if (user.VerifyPassword(request.Password))
                    {
                        response.Id = user.Id.ToString();
                        response.UserName = user.UserName;
                        response.FullName = user.FullName;
                        response.Timeout = user.Timeout;
                        response.Roles.Add(user.Roles.Select(e => e.Name).ToList());
                        response.Message = new ResponseMessage
                        {
                            Status = (int)EnumMessageStatus.Success,
                            StatusCode = "200",
                            Message = $"Login success!",
                            Data = request.UserName
                        };

                        // Update activity time
                        user.LastActivity = DateTime.UtcNow;
                        await userBll.Edit(user.Copy());
                    }
                    else
                    {
                        response.Message = new ResponseMessage
                        {
                            Status = (int)EnumMessageStatus.Warning,
                            StatusCode = "401",
                            Message = $"Login failed, invalid Password!"
                        };
                    }
                }
                else
                {
                    response.Message = new ResponseMessage
                    {
                        Status = (int)EnumMessageStatus.Warning,
                        StatusCode = "404",
                        Message = $"User {request.UserName} does not exists!"
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Login Error: Username: {0}. Error: {1}", request.UserName, ex.Message);
                response.Message = new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Login to User {request.UserName} failed!",
                    Data = ex.Message
                };
            }
            return response;
        }
        public override async Task<ResponseMessage> Signup(SignupRequest request, ServerCallContext context)
        {
            var response = new ResponseMessage();
            try
            {
                var user = await userBll.Get(request.UserName, true); 
                if(user == null)
                {
                    user = new EUser();
                    user.Id = new Guid();
                    user.UsergroupId = new Guid("F645D949-ABF3-49D0-926B-96BEB94124BF");

                    user.EmailVerified = true;
                    user.PhoneNumberVerified = true;
                    user.FullName = "";
                    user.LangCode = "vn";
                    user.Timeout = 10;
                    user.Timezone = 8;
                    user.UserName = request.UserName;
                    user.Email = request.Email;
                    user.Password = request.Password;
                    user.MustChangePassword = false;
                    user.IsActive = true;
                    user.Deleted = false;
                    user.GivenName = "User";
                    var result = await userBll.Add(user);

                    return new ResponseMessage
                    {
                        Status = (int)EnumMessageStatus.Success,
                        StatusCode = "200",
                        Message = $"Add User {request.UserName} Success!",
                        Data = result.Id.ToString()
                    };
                }    
                else
                {
                    return new ResponseMessage
                    {
                        Status = (int)EnumMessageStatus.Info,
                        StatusCode = "200",
                        Message = $"Created Account was failed ,{request.Email} or {request.UserName} existed!",
                        //  Data = null
                    };
                }
                 
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Add User {request.UserName} failed! Error: {ex.Message}");
                return new ResponseMessage
                {
                    Status = (int)EnumMessageStatus.Danger,
                    StatusCode = "500",
                    Message = $"Add User {request.UserName} failed! Error: {ex.Message}"
                };
            }
        }
    }
}
