using Microsoft.AspNetCore.Mvc;
using Space.Bussiness;
using Space.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_V1.Controllers
{
    public class CityController : Controller
    {
        private readonly CityBll _cityBll;

        public CityController(IDbDataContext context)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
