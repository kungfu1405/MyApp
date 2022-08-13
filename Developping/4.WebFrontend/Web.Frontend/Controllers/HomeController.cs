using AutoMapper;
using DbData.Entities;
using DbData.Protos;
using Grpc.Net.Client;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Web.Frontend.Commons;
using Web.Frontend.Models;
using Mic.Core.Website;

namespace Web.Frontend.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        private GrpcChannel channel;
        private ExperienceServices.ExperienceServicesClient experienceClient;

        public HomeController(IMapper mapper, ILogger<HomeController> logger) : base(logger, mapper)
        {
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                initChanel();
                var paging = new PagingType
                {
                    Start = 0,
                    Length = 24
                };
                var response = await experienceClient.GetListAsync(new ExperienceFilter { 
                    LangCode = CurrentLanguage,
                    Paging = paging,
                    Sort = new SortType { ColumnName = "CreateDate", Direction = "DESC" }
                });

                var model = new HomeModel { 
                    ListExperience = mapper.Map<IList<EExperience>>(response.Data)
                }; 

                return View(model);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Home Page Error. Message: {ex.Message}");
                return View("Error");
            }
            
        }

        [Authorize]
        public IActionResult Privacy()
        {
            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //var accessToken = HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken).Result;

            //if (!string.IsNullOrWhiteSpace(accessToken))
            //{
            //    client.SetBearerToken(accessToken);
            //}
            //var result = client.GetAsync($"{UrlList.BackendApi}/test").Result;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Upload()
        {
            return View();
        }

        private void initChanel()
        {
            channel = new GrpcChannelHepper().CreateDbDataChanel(AccessToken);
            experienceClient = new ExperienceServices.ExperienceServicesClient(channel);
        }
    }
}
