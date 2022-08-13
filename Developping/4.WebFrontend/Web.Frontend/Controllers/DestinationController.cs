using AutoMapper;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Frontend.Commons;
using DbData.Protos;
using DbData.Entities;
using Web.Frontend.Models;

using Mic.Core.Entities;

using Mic.Core.Website;

namespace Web.Frontend.Controllers
{
    public class DestinationController : BaseController<DestinationController>
    {
        private GrpcChannel channel;
        private DestinationServices.DestinationServicesClient destinationClient;
        private ExperienceServices.ExperienceServicesClient experienceClient;
        private AttractionServices.AttractionServicesClient attractionClient;

        public DestinationController(IMapper mapper, ILogger<DestinationController> logger) : base(logger, mapper)
        {
        }

        [Route("Destinations")]
        public async Task<IActionResult> Index()
        {
            try
            {
                initChanel();
                var response = await destinationClient.GetListAsync(new DestinationFilter
                {
                    LangCode = CurrentLanguage
                });

                var result = new DestinationsModel
                {
                    ListDestination = mapper.Map<IList<EDestination>>(response.Data)
                };

                return View(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Destination list failed. Message: {ex.Message}");
                return View("Error");
            }
        }

        [Route("Destination/{routeUri}/{typeOfAtt:int?}")]
        public async Task<IActionResult> Detail(string? routeUri , int? typeOfAtt)
        {           
                try
                {
                    initChanel();
                    var response = await destinationClient.GetAsync(new IdLangRequest
                    {
                        Id = routeUri,
                        LangCode = CurrentLanguage
                    });
                    var model = new DestinationModel();
                model.Pagination.Page = 1;
                model.Pagination.Perpage = 4;
             
                model.RouteUri = routeUri;
                model.TypeOfAtt = typeOfAtt ?? (int)EnumAttractionType.PlaceToVisit ;
                    model.DestinationDetail = mapper.Map<EDestination>(response);                
                var listAllAttraction = await attractionClient.GetListAsync(new AttractionFilter
                {
                    LangCode = CurrentLanguage,                   
                    DestinationId = model.DestinationDetail.Id.ToString(),
                    Paging = mapper.Map<PagingType>(model.Pagination),
                    //Paging = paging,                    
                    AttractionTypes = (typeOfAtt ?? (int)EnumAttractionType.PlaceToVisit)
                });

                model._ListAllAttraction.listAttraction = mapper.Map<IList<EAttraction>>(listAllAttraction.Data);
                model._ListAllAttraction.TotalRecord = mapper.Map<int>(listAllAttraction.TotalRecords);

             
                model.Pagination.Page = 1;
                model.Pagination.Perpage = 9;
                var listExperience = await experienceClient.GetListAsync(new ExperienceFilter
                {
                    LangCode = CurrentLanguage,
                    DestinationId = model.DestinationDetail.Id.ToString(),
                    Paging = mapper.Map<PagingType>(model.Pagination)
                });
                model._ListExperience.listExperience = mapper.Map<IList<EExperience>>(listExperience.Data);
                model._ListExperience.TotalRecord = mapper.Map<int>(listExperience.TotalRecords);
                return View(model);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Get Destination detail failed. Message: {ex.Message}");
                    return View("Error");
                }
         }
        [HttpPost]
        //[Route("Destination/RequestByPage/{routeUri}/{page}/{typeOfAtt:int?}")]

        [Route("Destination/RequestByPage")]
        public async Task<JsonResult> RequestByPage(string routeUri, int typeOfAtt=0, int page=0)
        {
            var result_code = 0;
            var result_message = "";
            try
            {
                initChanel();
                var response = await destinationClient.GetAsync(new IdLangRequest
                {
                    Id = routeUri,
                    LangCode = CurrentLanguage
                });
                var model = new DestinationModel();
                model.RouteUri = routeUri;
                model.TypeOfAtt = typeOfAtt ;
                model.DestinationDetail = mapper.Map<EDestination>(response);
              
                model.Pagination.Page = page;
                model.Pagination.Perpage = 4;

                model.RouteUri = routeUri;
                model.TypeOfAtt = typeOfAtt;
                model.DestinationDetail = mapper.Map<EDestination>(response);


                var listAllAttraction = await attractionClient.GetListAsync(new AttractionFilter
                {
                    LangCode = CurrentLanguage,
                    DestinationId = model.DestinationDetail.Id.ToString(),
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
                    DestinationId = model.DestinationDetail.Id.ToString(),
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

        [HttpPost]
        [Route("Destination/GetData")]

        public JsonResult GetData(string routeUri, int typeOfAtt = 0, int page = 0)
        {
            var josn = "kdjfhg";
            return Json(josn);
        }
        private void initChanel()
        {
            channel = new GrpcChannelHepper().CreateDbDataChanel(AccessToken);
            destinationClient = new DestinationServices.DestinationServicesClient(channel);
            experienceClient = new ExperienceServices.ExperienceServicesClient(channel);
            attractionClient = new AttractionServices.AttractionServicesClient(channel);
        }
    }
 
}
