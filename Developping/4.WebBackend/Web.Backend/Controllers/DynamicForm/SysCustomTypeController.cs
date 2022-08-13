using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mic.Core.Website;

namespace Web.Backend.Controllers.DynamicForm
{
    public class SysCustomTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
