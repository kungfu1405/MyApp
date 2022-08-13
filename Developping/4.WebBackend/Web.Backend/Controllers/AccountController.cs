using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mic.Core.Website;

namespace Web.Backend.Controllers
{
    public class AccountController : BaseController<AccountController>
    {
        public AccountController(ILogger<AccountController> logger) : base(logger)
        {
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
