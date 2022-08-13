using AutoMapper;
using DbData.Entities;
using DbData.Entities.Enums;
using DbData.Protos;
using Grpc.Net.Client;
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
    public class PillarController : BaseController<PillarController>
    {
        private GrpcChannel channel;
        private ExperienceServices.ExperienceServicesClient experienceClient;
        private AttractionServices.AttractionServicesClient attractionClient;
        public PillarController(IMapper mapper, ILogger<PillarController> logger) : base(logger, mapper) { }

        private void initChanel()
        {
            channel = new GrpcChannelHepper().CreateDbDataChanel(AccessToken);
            experienceClient = new ExperienceServices.ExperienceServicesClient(channel);
            attractionClient = new AttractionServices.AttractionServicesClient(channel);
        }

        public async Task<IActionResult> Index()
        {
            initChanel();
            var paging = new PagingType
            {
                Start = 0,
                Length = 9
            };
            var response = await experienceClient.GetListAsync(new ExperienceFilter
            {
                LangCode = CurrentLanguage,
                Paging = paging
            });

            var model = new PillarModel
            {
                ListExperience = mapper.Map<IList<EExperience>>(response.Data)
            };

            return View(model);
        }

        [Route("Pillar/{routeUri}")]
        public async Task<IActionResult> Detail(string routeUri)
        {
            var model = new PillarModel();
            model.Pillar = !string.IsNullOrEmpty(routeUri) ? (EnumPillar)Enum.Parse(typeof(EnumPillar), routeUri, true) : EnumPillar.AdventureAndExploration;
            initChanel();
            var paging = new PagingType
            {
                Start = 0,
                Length = 9
            };
            var response = await experienceClient.GetListAsync(new ExperienceFilter
            {
                LangCode = CurrentLanguage,
                Paging = paging
            });
            model.ListExperience = mapper.Map<IList<EExperience>>(response.Data);

            return View(model);
        }

        [Route("Pillars/{hashtag}")]
        public async Task<IActionResult> Hashtag(string hashtag, string q, string type, int page = 1)
        {
            var model = new HashtagModel();
            initChanel();

            if (page > 0)
                model.Page = page;
            var paging = new PagingType
            {
                Start = model.Page > 1 ? (model.Page - 1) * model.Items : 0,
                Length = model.Items
            };

            model.Hashtag = hashtag.Replace(" ", "").Trim();
            if (!string.IsNullOrEmpty(q))
            {
                Regex reg = new Regex("[*'\",_&#^@]");
                model.Keyword = reg.Replace(q, string.Empty);
            }

            model.TypeSearch = !string.IsNullOrEmpty(type) ? (EnumTypeSearch)Enum.Parse(typeof(EnumTypeSearch), type, true) : EnumTypeSearch.Story;
            if (model.TypeSearch.Equals(EnumTypeSearch.Story))
            {
                if (!string.IsNullOrEmpty(model.Hashtag))
                {

                    var response = await experienceClient.GetListAsync(new ExperienceFilter
                    {
                        LangCode = CurrentLanguage,
                        Paging = paging,
                        Title = model.Keyword,
                        Tags = model.Hashtag
                    });

                    model.ListExperience = mapper.Map<IList<EExperience>>(response.Data);
                    model.TotalRecords = response.TotalRecords;
                }
            }
            else if (model.TypeSearch.Equals(EnumTypeSearch.PlaceToEat)
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
                    Name = model.Keyword,
                    Tags = model.Hashtag
                });

                model.ListAttraction = mapper.Map<IList<EAttraction>>(response.Data);
                model.TotalRecords = response.TotalRecords;
            }

            return View(model);
        }
    }
}
