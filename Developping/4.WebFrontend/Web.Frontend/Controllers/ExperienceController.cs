using AutoMapper;
using DbData.Entities;
using DbData.Protos;
using UserDb.Protos;
using Grpc.Net.Client;
using Mic.Core.Entities;
using Mic.Core.Website;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Web.Frontend.Models;
using Mic.UserDb.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Web.Frontend.Controllers
{
    public class ExperienceController : BaseController<ExperienceController>
    {        
        private GrpcChannel channel;
        private ExperienceServices.ExperienceServicesClient experienceClient;
        private UserServices.UserServicesClient userClient;
        private UserProfileServices.UserProfileServicesClient userProfileServicesClient;

        public ExperienceController(IMapper mapper, ILogger<ExperienceController> logger) : base(logger, mapper)
        {
        }

        [Route("Experience/{routeUri}")]
        public async Task<IActionResult> Detail(string routeUri)
        {
            var model = new ExperienceDetailModel { 
                ListExperienceRelated = new List<EExperience>() 
            };
            try
            {
                initChanel();                
                var response = await experienceClient.GetAsync(new IdLangRequest
                {
                    Id = routeUri,
                    LangCode = CurrentLanguage
                });
                model.Experience = mapper.Map<EExperience>(response);

                if (!string.IsNullOrEmpty(response.AuthorId))
                {
                    var userResponse = await userClient.GetAsync(new IdRequest { Id = response.AuthorId });
                    if (userResponse != null)
                        model.User = mapper.Map<EUser>(userResponse);

                    var userProfileResponse = await userProfileServicesClient.GetAsync(new IdRequest { Id = response.AuthorId});
                    if (userProfileResponse != null)
                        model.UserProfile = mapper.Map<EUserProfile>(userProfileResponse);
                }

                //Get experience related by tags, if have not tags => not show   
                if (model.Experience.Tags.Count > 0)
                {
                    var strTag = new List<string>();
                    foreach (var tag in model.Experience.Tags)
                    {
                        strTag.Add(tag.Name);
                    }

                    var paging = new PagingType
                    {
                        Start = 0,
                        Length = 6
                    };

                    var lstExperience = await experienceClient.GetListAsync(new ExperienceFilter
                    {
                        LangCode = CurrentLanguage,
                        Paging = paging,
                        Sort = new SortType { ColumnName = "CreateDate", Direction = "DESC" },
                        Tags = string.Join(",", strTag)

                    });

                    //Remove Current Experience In List Related
                    foreach (var itm in lstExperience.Data)
                    {
                        if (itm.Id.Equals(model.Experience.Id.ToString()))
                            lstExperience.Data.Remove(itm);
                    }
                    model.ListExperienceRelated = mapper.Map<IList<EExperience>>(lstExperience.Data);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Experience detail failed. Message: {ex.Message}");
                model.MsgError = "Get Experience detail failed";
            }

            return View(model);
        }

        [Authorize]
        [Route("Experiences/Edit/{routeUri}")]
        [Route("Experiences/Upload")]
        public async Task<IActionResult> AddEdit(string routeUri)
        {
            var model = new ExperienceModel();
            try
            {
                initChanel();
                if (!string.IsNullOrEmpty(routeUri))
                {
                    var response = await experienceClient.GetAsync(new IdLangRequest
                    {
                        Id = routeUri,
                        LangCode = CurrentLanguage
                    });
                    model.Experience = mapper.Map<EExperience>(response);

                    var user = await userClient.GetAsync(new IdRequest { Id = response.AuthorId });
                    model.Author = mapper.Map<EUser>(user);

                    model.ActionMode = FormActionMode.Edit;
                }
               
                //check author with userId
                //if(CurrentUserId!= null || !CurrentUserId.HasValue || CurrentUserId.Value != model.AuthorId)
                //    return View("Error");

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Experience detail failed. Message: {ex.Message}");
                return View("Error");
            }
        }

        [Route("Experiences/Data")]
        public async Task<IActionResult> GetDataHomeList(int page)
        {
            try
            {
                initChanel();
                var record = 9;
                var paging = new PagingType
                {
                    Start = page*record + 1 + 6, //top 6 on toppick
                    Length = record + 1
                };
                var response = await experienceClient.GetListAsync(new ExperienceFilter
                {
                    LangCode = "vn",
                    Paging = paging,
                    Sort = new SortType { ColumnName = "DefaultName", Direction = "ASC" }
                });

                var result = mapper.Map<IList<EExperience>>(response.Data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Experience list failed. Message: {ex.Message}");
            }

            return Ok();
        }

        //[Route("Experience/{routeUri}")]
        public async Task<IActionResult> Preview(string routeUri)
        {
            try
            {
                initChanel();
                var model = new ExperienceDetailModel
                {
                    ListExperienceRelated = new List<EExperience>()
                };
                var response = await experienceClient.GetAsync(new IdLangRequest
                {
                    Id = routeUri,
                    LangCode = CurrentLanguage
                });
                model.Experience = mapper.Map<EExperience>(response);

                if (!string.IsNullOrEmpty(response.AuthorId))
                {
                    var userResponse = await userClient.GetAsync(new IdRequest { Id = response.AuthorId });
                    if (userResponse != null)
                        model.User = mapper.Map<EUser>(userResponse);

                    var userProfileResponse = await userProfileServicesClient.GetAsync(new IdRequest { Id = response.AuthorId });
                    if (userProfileResponse != null)
                        model.UserProfile = mapper.Map<EUserProfile>(userProfileResponse);
                }

                //Get experience related by tags, if have not tags => not show   
                if(model.Experience.Tags.Count > 0)
                {
                    var strTag = new List<string>();
                    foreach (var tag in model.Experience.Tags)
                    {
                        strTag.Add(tag.Name);
                    }

                    var paging = new PagingType
                    {
                        Start = 0,
                        Length = 6
                    };
                    
                    var lstExperience = await experienceClient.GetListAsync(new ExperienceFilter
                    {
                        LangCode = CurrentLanguage,
                        Paging = paging,
                        Sort = new SortType { ColumnName = "CreateDate", Direction = "DESC" },
                        Tags = string.Join(",", strTag)

                    });

                    //Remove Current Experience In List Related
                    foreach (var itm in lstExperience.Data)
                    {
                        if (itm.Id.Equals(model.Experience.Id.ToString()))
                            lstExperience.Data.Remove(itm);
                    }
                    model.ListExperienceRelated = mapper.Map<IList<EExperience>>(lstExperience.Data);
                }                

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Experience detail failed. Message: {ex.Message}");
                return Error();
            }
        }

        private void initChanel()
        {
            channel = new GrpcChannelHepper().CreateDbDataChanel(AccessToken);
            experienceClient = new ExperienceServices.ExperienceServicesClient(channel);
            userClient = new UserServices.UserServicesClient(channel);
            userProfileServicesClient = new UserProfileServices.UserProfileServicesClient(channel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
