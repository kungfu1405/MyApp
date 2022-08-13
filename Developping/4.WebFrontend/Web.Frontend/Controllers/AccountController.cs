using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mic.Core.Website;
using AutoMapper;
using Mic.Core.MemCache;
using Grpc.Net.Client;
using DbData.Protos;
using UserDb.Protos;
using DbData.Entities;
using Web.Frontend.Models;
using Mic.UserDb.Entities;
using DbData.Entities.Models;

namespace Web.Frontend.Controllers
{
    public class AccountController : BaseController<AccountController>
    {
        private GrpcChannel channel;
        private DestinationServices.DestinationServicesClient destinationClient;
        private UserProfileServices.UserProfileServicesClient userProfileClient;
        private UserServices.UserServicesClient userClient;
        private ExperienceServices.ExperienceServicesClient experienceClient;

        private UserAuthenticateServices.UserAuthenticateServicesClient userAuthenticateServicesClient;
        public AccountController(IMapper mapper, ILogger<AccountController> logger) : base(logger, mapper)
        {
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var model = new UserModel();
            if (string.IsNullOrEmpty(CurrentUser.Id.ToString()))
            {
                UserModel md = new UserModel();
                md.User = new EUser() ;
                md.UserProfile = new EUserProfile();
                return View(Redirect("/Search"));
            }

            try
            {
                initChanel();
                //Get User Information
                
                var user = await userClient.GetAsync(new IdRequest { Id = CurrentUser.Id.ToString() });
                model.User = mapper.Map<EUser>(user);

                //Get User Profile
                var userProfile = await userProfileClient.GetAsync(new IdRequest
                {
                    Id = CurrentUserId.ToString()
                });
                model.UserProfile = mapper.Map<EUserProfile>(userProfile);

                model.Pagination.Page = 1;
                model.Pagination.Perpage = 9;
                var experience = await experienceClient.GetListAsync(new ExperienceFilter
                {
                    AuthorId = user.Id,
                    Paging = mapper.Map<PagingType>(model.Pagination),
                });
                model._ListExperience.listExperience = mapper.Map<IList<EExperience>>(experience.Data);
                model._ListExperience.TotalRecord = mapper.Map<int>(experience.TotalRecords);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get User profile detail failed. Message: {ex.Message}");
                return View("Error");
            }
            //return View();
        }

        [Authorize]
        [Route("Account/RequestPage/{page}")]
        public async Task<JsonResult> RequestPage(int page)
        {
            var model = new UserModel();
          
            try
            {
                initChanel();
                model.Pagination.Page = page;
                model.Pagination.Perpage = 9;
                var experience = await experienceClient.GetListAsync(new ExperienceFilter
                {
                    AuthorId = CurrentUser.Id.ToString(),
                    Paging = mapper.Map<PagingType>(model.Pagination),
                });
                model._ListExperience.listExperience = mapper.Map<IList<EExperience>>(experience.Data);
                model._ListExperience.TotalRecord = mapper.Map<int>(experience.TotalRecords);
                return Json(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get User profile detail failed. Message: {ex.Message}");
                return Json(model);
            }
            //return View();
        }

        [Authorize]
        public IActionResult Login(string returnUrl)
        {
            return string.IsNullOrEmpty(returnUrl)
                ? RedirectToAction("Index", "Home")
                : Redirect(returnUrl);
        }       
        public IActionResult Logout()
        {
            MemCacheManager.App.Remove(string.Format(MemCacheConfigs.UserProfilePatternKey, CurrentUserId));
            return SignOut("Cookies", "oidc");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }

        public IActionResult GetList()
        {
            return View();
        }
          
        public async Task<IActionResult> SignUp(string route)
        {
            SignUpModel model = new SignUpModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            try
            {
                initChanel();
                if(ModelState.IsValid)
                {
                    if(model.password != model.repassword)
                    {
                        model.Message =  "Re enter password is not matched";
                        return View(model);
                    }
                    else { 
                        var request = mapper.Map<SignupRequest>(model);
                        var result = await userAuthenticateServicesClient.SignupAsync(request);
                        if(result.Status != 0)
                        {
                            model.Message = result.Message;
                       //     ModelState.AddModelError("Login fail", "account.err.incorrect_user_pass");
                            return View(model);
                        }   
                        else
                        {
                            return Redirect("/");
                        }    
                    }
                }             
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Account SignUp Failed. Message: {ex.Message}");
                return View("Error");
            }
            return View(model);
        }
        private void initChanel()
        {
            channel = new GrpcChannelHepper().CreateDbDataChanel(AccessToken);
            userProfileClient = new UserProfileServices.UserProfileServicesClient(channel);
            userClient = new UserServices.UserServicesClient(channel);
            experienceClient = new ExperienceServices.ExperienceServicesClient(channel);
            userAuthenticateServicesClient = new UserAuthenticateServices.UserAuthenticateServicesClient(channel);
        }
    }
}
