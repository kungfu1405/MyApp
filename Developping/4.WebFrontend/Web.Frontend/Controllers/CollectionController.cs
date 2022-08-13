using AutoMapper;
using Mic.Core.Website;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Frontend.Controllers
{
    public class CollectionController :  BaseController<CollectionController>
    {
        public CollectionController(IMapper mapper, ILogger<CollectionController> logger) : base(logger, mapper)
        {

        }
        public async Task<IActionResult> Index()        
        {
            return View();
        } 
        public IActionResult MyCollection()
        {
            return View();
        }
        public IActionResult MySubCollection()
        {
            return View();
        }

    }
}
