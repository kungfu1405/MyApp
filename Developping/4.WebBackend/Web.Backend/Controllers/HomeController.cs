using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.Backend.Commons;
using Web.Backend.Models;
using DynamicData.Protos;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using IdentityModel.Client;
using IdentityModel;
using AutoMapper;
using Mic.Core.Website;

namespace Web.Backend.Controllers
{
    public class HomeController : BaseController<HomeController>
    {

        public HomeController(IMapper mapper, ILogger<HomeController> logger) : base(logger, mapper)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Test()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var accessToken = HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken).Result;

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                client.SetBearerToken(accessToken);
            }
            //var result = client.GetAsync($"{UrlList.BackendApi}/test").Result;
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var accessToken = HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken).Result;

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                client.SetBearerToken(accessToken);
            }
            //var result = client.GetAsync($"{UrlList.BackendApi}/test").Result;
            var userClaims = User.Claims;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
