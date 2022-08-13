using Mic.Core.MemCache;
using Mic.UserDb.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserDb.Protos;
using DbData.Protos;
using AutoMapper;
using Grpc.Net.Client;
using Grpc.Core;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Mic.Core.Website
{    
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await base.OnActionExecutionAsync(context, next);
        }

        public Guid? CurrentUserId
        {
            get
            {
                if (User.Claims.Any())
                {
                    var idClaim = User.Claims.Single(c => c.Type == "sub");
                    if (idClaim != null)
                        return new Guid(idClaim.Value);
                }
                return null;
            }
        }

        public string AccessToken
        {
            get { return HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken).Result; }
        }

        public string CurrentLanguage
        {
            get
            {
                try
                {
                    return HttpContext.Request.Cookies["lang"];
                }
                catch
                {
                    return "vn";
                }
            }
        }

        public int UserTimezone
        {
            get
            {
                var userId = CurrentUserId;
                if (!userId.HasValue || new Guid().Equals(userId))
                    return 0;

                if (MemCacheManager.App.ExistFormat(MemCacheConfigs.UserTimezonePatternKey, userId))
                    return MemCacheManager.App.GetFormat<int>(MemCacheConfigs.UserTimezonePatternKey, userId);
                return 0;
            }
        }
        
        public virtual EUser CurrentUser
        {
            get
            {
                var userId = CurrentUserId;
                if (!userId.HasValue || new Guid().Equals(userId))
                    return null;

                if (MemCacheManager.App.ExistFormat(MemCacheConfigs.UserProfilePatternKey, userId))
                    return MemCacheManager.App.GetFormat<EUser>(MemCacheConfigs.UserProfilePatternKey, userId);
                return null;
            }
        }

        public virtual IList<ELanguage> Languages
        {
            get
            {
                if (MemCacheManager.App.Exist(MemCacheConfigs.LanguageKey))
                {
                    return MemCacheManager.App.Get<List<ELanguage>>(MemCacheConfigs.LanguageKey);
                }
                return new List<ELanguage>();
            }
        }

    }

    public class BaseController<T> : BaseController
    {
        protected readonly ILogger<T> logger;
        protected readonly IMapper mapper;
        public BaseController(ILogger<T> logger, IMapper mapper = null)
        {
            this.logger = logger;
            if (mapper != null)
                this.mapper = mapper;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await base.OnActionExecutionAsync(context, next);
            ViewBag.Language = CurrentLanguage;
            ViewBag.Languages = Languages;
            ViewBag.CurrentUser = CurrentUser;
        }

        public override EUser CurrentUser
        {
            get
            {
                var userId = CurrentUserId;
                if (!userId.HasValue || new Guid().Equals(userId))
                    return null;

                var user = new EUser();
                if (MemCacheManager.App.ExistFormat(MemCacheConfigs.UserProfilePatternKey, userId))
                {
                    user = MemCacheManager.App.GetFormat<EUser>(MemCacheConfigs.UserProfilePatternKey, userId);
                }
                else
                {
                    var dbChannel = new GrpcChannelHepper().CreateDbDataChanel(AccessToken);
                    var userClient = new UserServices.UserServicesClient(dbChannel);

                    var response = userClient.GetAsync(new IdRequest { Id = userId.ToString() }).GetAwaiter().GetResult();

                    if (!string.IsNullOrEmpty(response.Id))
                    {
                        user = mapper.Map<EUser>(response);
                        MemCacheManager.App.SetFormat(user, MemCacheConfigs.UserProfilePatternKey, userId);
                        MemCacheManager.App.SetFormat(user.Timezone, MemCacheConfigs.UserTimezonePatternKey, userId);
                    }
                }
                return user;
            }
        }

        public override IList<ELanguage> Languages
        {
            get
            {
                var result = new List<ELanguage>();
                if (MemCacheManager.App.Exist(MemCacheConfigs.LanguageKey))
                {
                    result = MemCacheManager.App.Get<List<ELanguage>>(MemCacheConfigs.LanguageKey);
                }
                else
                {
                    var dbChannel = new GrpcChannelHepper().CreateDbDataChanel("");
                    var languageClient = new LanguageServices.LanguageServicesClient(dbChannel);

                    var response = languageClient.GetAllActiveAsync(new EmptyRequest()).GetAwaiter().GetResult();
                    if (response.Languages != null && response.Languages.Any())
                    {
                        result = mapper.Map<List<ELanguage>>(response.Languages);
                        MemCacheManager.App.Set(result, MemCacheConfigs.LanguageKey);
                    }
                }
                return result;
            }
        }
    }
}
