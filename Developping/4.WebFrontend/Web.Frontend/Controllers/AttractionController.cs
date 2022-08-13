using AutoMapper;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Frontend.Commons;
using DbData.Protos;
using DbData.Entities;
using Web.Frontend.Models;
using Mic.Core.Website;
using Mic.Core.Entities;

namespace Web.Frontend.Controllers
{
    public class AttractionController : BaseController<AttractionController>
    {
        private GrpcChannel channel;
        private AttractionServices.AttractionServicesClient attractionClient;
        private DestinationServices.DestinationServicesClient destinationClient;
        private ExperienceServices.ExperienceServicesClient experienceClient;

        public AttractionController(IMapper mapper, ILogger<AttractionController> logger) : base(logger, mapper)
        {
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                initChanel();
                var response = await attractionClient.GetListAsync(new AttractionFilter
                {
                    LangCode = CurrentLanguage
                });
                var model = mapper.Map<IList<EAttraction>>(response.Data);
                return View();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Attraction list failed. Message: {ex.Message}");
                return View("Error");
            }
        }

        [Route("Attraction/{routeUri}/{typeOfAtt:int?}")]
        public async Task<IActionResult> Detail(string routeUri, int? typeOfAtt)
        {
            try
            {
                initChanel();
                var response = await attractionClient.GetAsync(new IdLangRequest
                {
                    Id = routeUri,
                    LangCode = CurrentLanguage
                });

                var model = new AttractionModel();
                model.RouteUri = routeUri;
                model.TypeOfAtt = typeOfAtt ?? (int)EnumAttractionType.PlaceToVisit;
                model.AttractionDetail = mapper.Map<EAttraction>(response);
                model.Pagination.Page = 1;
                model.Pagination.Perpage = 9;
                var listExperience = await experienceClient.GetListAsync(new ExperienceFilter
                {
                    LangCode = CurrentLanguage,                    
                    DestinationId = model.AttractionDetail.DestinationId.ToString(),
                    Paging = mapper.Map<PagingType>(model.Pagination)
                });
                model._ListExperience.listExperience = mapper.Map<IList<EExperience>>(listExperience.Data);
                model._ListExperience.TotalRecord = mapper.Map<int>(listExperience.TotalRecords);

                model.Pagination.Page = 1;
                model.Pagination.Perpage = 4;
                var listAllAttraction = await attractionClient.GetListAsync(new AttractionFilter
                {
                    LangCode = CurrentLanguage,
                    DestinationId = model.AttractionDetail.DestinationId.ToString(),
                    Paging = mapper.Map<PagingType>(model.Pagination),
                    AttractionTypes = (typeOfAtt ?? (int)EnumAttractionType.PlaceToVisit)

                });
           
              
                model._ListAllAttraction.listAttraction = mapper.Map<IList<EAttraction>>(listAllAttraction.Data);
                model._ListAllAttraction.TotalRecord = mapper.Map<int>(listAllAttraction.TotalRecords);

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Attraction detail failed. Message: {ex.Message}");
                return View("Error");
            }
            //return View();
        }
        
        [HttpPost]
        //[Route("Attraction/RequestByPage/{routeUri}/{page}/{typeOfAtt:int?}")]
        [Route("Attraction/RequestByPage")]
        public async Task<JsonResult> RequestByPage(string routeUri, int typeOfAtt = 0, int page = 0)
        {
            var result_code = 0;
            var result_message = "";
            try
            {
                initChanel();
                var response = await attractionClient.GetAsync(new IdLangRequest
                {
                    Id = routeUri,
                    LangCode = CurrentLanguage
                });
                var model = new AttractionModel();
                model.RouteUri = routeUri;
                model.TypeOfAtt = typeOfAtt;
                model.AttractionDetail = mapper.Map<EAttraction>(response);

                model.Pagination.Page = page;
                model.Pagination.Perpage = 4;

                model.RouteUri = routeUri;
                model.TypeOfAtt = typeOfAtt;
                model.AttractionDetail = mapper.Map<EAttraction>(response);


                var listAllAttraction = await attractionClient.GetListAsync(new AttractionFilter
                {
                    LangCode = CurrentLanguage,
                    DestinationId = model.AttractionDetail.DestinationId.ToString(),
                    Paging = mapper.Map<PagingType>(model.Pagination),
                    AttractionTypes = (typeOfAtt)

                });
                model._ListAllAttraction.listAttraction = mapper.Map<IList<EAttraction>>(listAllAttraction.Data);
                model._ListExperience.TotalRecord = mapper.Map<int>(listAllAttraction.TotalRecords);

                model.Pagination.Page = page;
                model.Pagination.Perpage = 9;
                var listExperience = await experienceClient.GetListAsync(new ExperienceFilter
                {
                    LangCode = CurrentLanguage,
                    DestinationId = model.AttractionDetail.DestinationId.ToString(),
                    Paging = mapper.Map<PagingType>(model.Pagination),
                });
                model._ListExperience.listExperience = mapper.Map<IList<EExperience>>(listExperience.Data);
                model._ListExperience.TotalRecord = mapper.Map<int>(listExperience.TotalRecords);

                return Json(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Destination detail failed. Message: {ex.Message}");
                //return View("Error");
                var model = new DestinationModel();
                return Json(model);

            }
        }

        private void initChanel()
        {
            channel = new GrpcChannelHepper().CreateDbDataChanel(AccessToken);
            attractionClient = new AttractionServices.AttractionServicesClient(channel);
            destinationClient = new DestinationServices.DestinationServicesClient(channel);
            experienceClient = new ExperienceServices.ExperienceServicesClient(channel);
        }
    }
}
