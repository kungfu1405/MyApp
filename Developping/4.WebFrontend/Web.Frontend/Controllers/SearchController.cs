using AutoMapper;
using DbData.Entities;
using DbData.Entities.Enums;
using DbData.Protos;
using Grpc.Net.Client;
using Mic.Core.Entities;
using Mic.Core.Website;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Web.Frontend.Models;

namespace Web.Frontend.Controllers
{
    public class SearchController : BaseController<SearchController>
    {
        private GrpcChannel channel;
        private DestinationServices.DestinationServicesClient destinationClient;
        private ExperienceServices.ExperienceServicesClient experienceClient;
        private AttractionServices.AttractionServicesClient attractionClient;

        public SearchController(IMapper mapper, ILogger<SearchController> logger) : base(logger, mapper)
        {
        }

        public async Task<IActionResult> Index(string q, string type, int page = 1)
        {
            try
            {
                initChanel();
                var model = new SearchModel();
                if (!string.IsNullOrEmpty(q))
                {
                    Regex reg = new Regex("[*'\",_&#^@]");
                    model.keyword = reg.Replace(q, string.Empty);
                }

                if (page > 0)
                    model.page = page;
                var paging = new PagingType
                {
                    Start = model.page > 1 ? (model.page - 1) * 6 : 0,
                    Length = 6
                };

                model.TypeSearch = !string.IsNullOrEmpty(type) ? (EnumTypeSearch)Enum.Parse(typeof(EnumTypeSearch), type, true) : EnumTypeSearch.Story;
                if (model.TypeSearch.Equals(EnumTypeSearch.Story))
                {
                    if (!string.IsNullOrEmpty(q))
                    {
                        
                        var response = await experienceClient.GetListAsync(new ExperienceFilter
                        {
                            LangCode = CurrentLanguage,
                            Paging = paging,
                            Title = model.keyword
                        });

                        model.ListExperience = mapper.Map<IList<EExperience>>(response.Data);
                        model.CountResult = response.TotalRecords;                    
                    }                    
                }
                else if(model.TypeSearch.Equals(EnumTypeSearch.PlaceToEat) 
                            || model.TypeSearch.Equals(EnumTypeSearch.PlaceToStay) 
                            || model.TypeSearch.Equals(EnumTypeSearch.PlaceToVisit))
                {
                    var attractionTypes = 0;

                    if (model.TypeSearch.Equals(EnumTypeSearch.PlaceToEat))
                        attractionTypes = (int)EnumAttractionType.PlaceToEat;
                    else if (model.TypeSearch.Equals(EnumTypeSearch.PlaceToStay))
                        attractionTypes = (int)EnumAttractionType.PlaceToStay;
                    else if (model.TypeSearch.Equals(EnumTypeSearch.PlaceToVisit))
                        attractionTypes = (int)EnumAttractionType.PlaceToVisit;

                    var response = await attractionClient.GetListAsync(new AttractionFilter
                    {
                        Paging = paging,
                        LangCode = CurrentLanguage,
                        AttractionTypes = attractionTypes,
                        Name = model.keyword
                    });

                    model.ListAttraction = mapper.Map<IList<EAttraction>>(response.Data);
                    model.CountResult = response.TotalRecords;
                }
                //string description = Mic.Core.DataTypes.dEnum.GetDescription(EnumTypeSearch.Story);

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Search Error. Message: {ex.Message}");
                return View("Error");
            }
        }

        private void initChanel()
        {
            channel = new GrpcChannelHepper().CreateDbDataChanel(AccessToken);
            destinationClient = new DestinationServices.DestinationServicesClient(channel);
            experienceClient = new ExperienceServices.ExperienceServicesClient(channel);
            attractionClient = new AttractionServices.AttractionServicesClient(channel);
        }

        public async Task<JsonResult> Destination(string keyword)
        {
            try
            {
                initChanel();
                var select2Datasource = new Select2Datasource();
                var response = await destinationClient.GetListAsync(new DestinationFilter
                {
                    LangCode = CurrentLanguage,
                    Name = keyword

                });
                var model = mapper.Map<IList<EDestination>>(response.Data);

                var result = model.ToList<EDestination>();
                foreach (var item in result)
                {
                    Select2DatasourceItem itemSelect = new Select2DatasourceItem();
                    itemSelect.Id = item.Id.ToString();
                    itemSelect.Text = "";
                    itemSelect.Text += (item.Continent != null ? " " + item.Continent + " - " : "");
                    itemSelect.Text += (item.CountryName != null ? " " + item.CountryName + " - " : "");
                    itemSelect.Text += (item.StateName != null ? " " + item.StateName + " - " : "");
                    itemSelect.Text += (item.CityName != null ? " " + item.CityName + " - " : "");
                    itemSelect.Text += (item.DefaultName != null ? " " + item.DefaultName : "");
                    select2Datasource.Results.Add(itemSelect);
                }

                return Json(select2Datasource.Results);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Destination list failed. Message: {ex.Message}");
                object data = "null";
                return Json(data);
            }
            //return Json(model);
        }

        public async Task<JsonResult> Attraction(string keyword, string destinationId)
        {
            try
            {
                initChanel();
                var select2Datasource = new Select2Datasource();
                var response = await attractionClient.GetListAsync(new AttractionFilter
                {
                    LangCode = CurrentLanguage,
                    Name = keyword,
                    DestinationId = (destinationId != null ? destinationId : null)

                });
                var model = mapper.Map<IList<EAttraction>>(response.Data);

                var result = model;
                foreach (var item in result)
                {
                    Select2DatasourceItem itemSelect = new Select2DatasourceItem();
                    itemSelect.Id = item.Id.ToString();
                    itemSelect.Text = "";
                    itemSelect.Text += (item.DefaultName != null ? " " + item.DefaultName + " - " : "");
                    //itemSelect.Text += (item.CountryName != null ? " " + item.CountryName + " - " : "");
                    //itemSelect.Text += (item.StateName != null ? " " + item.StateName + " - " : "");
                    //itemSelect.Text += (item.CityName != null ? " " + item.CityName + " - " : "");
                    //itemSelect.Text += (item.DefaultName != null ? " " + item.DefaultName : "");
                    select2Datasource.Results.Add(itemSelect);
                }

                return Json(select2Datasource.Results);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Destination list failed. Message: {ex.Message}");
                object data = "null";
                return Json(data);
            }
            //return Json(model);
        }

    }
}
