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
using Microsoft.AspNetCore.Authorization;
using Mic.Core.Entities;
using Mic.Core.DataTypes;
using Mic.Core.Website;
using System.Linq;
using System.Text.RegularExpressions;

namespace Web.Frontend.Controllers
{
    [Authorize]
    public class SessionController : BaseController<SessionController>
    {
        private GrpcChannel channel;
        private ExperienceServices.ExperienceServicesClient experienceClient;
        private ExperienceSessionServices.ExperienceSessionServicesClient experienceSessionClient;
        private AttractionServices.AttractionServicesClient attractionServicesClient;
        private DestinationServices.DestinationServicesClient destinationServicesClient;

        public SessionController(IMapper mapper, ILogger<SessionController> logger) : base(logger, mapper)
        {
        }

        private void initChanel()
        {
            channel = new GrpcChannelHepper().CreateDbDataChanel(AccessToken);
            experienceClient = new ExperienceServices.ExperienceServicesClient(channel);
            experienceSessionClient = new ExperienceSessionServices.ExperienceSessionServicesClient(channel);
            attractionServicesClient = new AttractionServices.AttractionServicesClient(channel);
            destinationServicesClient = new DestinationServices.DestinationServicesClient(channel);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(ExperienceModel model)
        {
            initChanel();
            int status = 0;
            string mess = "";
            ExperienceSessionStruct data = new ExperienceSessionStruct();
            ExperienceStruct experienceResponse = new ExperienceStruct();
            var lstTag = new List<TagStruct>();
            var regex = new Regex(@"#\w+");            

            try
            {
                //If Upload New Section
                // First: Upload Main Section
                if (string.IsNullOrEmpty(model.ExperienceId))
                {
                    if ((string.IsNullOrEmpty(model.MainTitle) || string.IsNullOrEmpty(model.MainDescription)))
                    {
                        mess = "Main Section Invalid";
                        return Ok(new
                        {
                            status = status,
                            mess = mess,
                            data = data
                        });
                    }
                    else
                    {
                        var mainSection = new ExperienceSessionStruct
                        {
                            Title = model.MainTitle,
                            Detail = dStr.EndRowToBr(model.MainDescription),
                            AttractionId = model.AttractionId,
                            LangCode = CurrentLanguage,
                            UserId = CurrentUserId.Value.ToString()
                        };

                        var mainResponse = await experienceSessionClient.AddAsync(mainSection);
                        if (mainResponse.Message.Status != (int)EnumMessageStatus.Success)
                        {
                            mess = "Main Section Invalid";
                            return Ok(new
                            {
                                status = status,
                                mess = mess,
                                data = data
                            });
                        }
                        else
                        {
                            model.ExperienceId = mainResponse.Session.ExperienceId;
                        }

                        //get Hashtag
                        var matchesMain = regex.Matches(model.MainDescription);
                        foreach (var match in matchesMain)
                        {
                            lstTag.Add(new TagStruct { 
                                Name = match.ToString().Replace("#", "")
                            });
                        }
                    }
                }
                
                if (!string.IsNullOrEmpty(mess) || string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Description))
                {
                    mess = "Data Invalid";
                }
                else
                {
                    var exS = new ExperienceSessionStruct
                    {
                        Title = model.Title,
                        Detail = dStr.EndRowToBr(model.Description),
                        ExperienceId = model.ExperienceId,
                        AttractionId = model.AttractionId,
                        LangCode = CurrentLanguage,
                        UserId = CurrentUserId.Value.ToString()
                    };

                    if (model.Files != null && model.Files.Count > 0)
                    {
                        var i = 0;
                        foreach (var img in model.Files)
                        {
                            i++;
                            exS.Images.Add(new ExperienceSessionImageStruct
                            {
                                ImagerUrl = img,
                                Ordinal = i
                            });
                        }
                    }

                    var response = await experienceSessionClient.AddAsync(exS);
                    mess = response.Message.Message;
                    if (response.Message.Status == (int)EnumMessageStatus.Success)
                    {
                        status = 1;
                        data = response.Session;

                        //Get Hashtag
                        var matchesSection = regex.Matches(model.Description);
                        foreach (var match in matchesSection)
                        {
                            lstTag.Add(new TagStruct
                            {
                                Name = match.ToString().Replace("#", "")
                            });
                        }

                        //Check Update Img for Experience If Experience have not thumb
                        var experience = await experienceClient.GetByAsync(new IdRequest { 
                            Id = model.ExperienceId
                        });

                        experienceResponse = experience;

                        if (experience != null)
                        {
                            //Update Img Thumb
                            if(string.IsNullOrEmpty(experience.ThumbnailUrl) && model.Files.Count > 0)
                            {
                                experience.ThumbnailUrl = model.Files.First();
                            }

                            //Update Hashtag
                            if(lstTag.Count > 0)
                            {
                                experience.Tags.AddRange(lstTag);
                            }

                            await experienceClient.EditAsync(experience);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Upload New Section Failed. Message: {ex.Message}");
                mess = ex.Message;
            }

            var result = new
            {
                status = status,
                mess = mess,
                data = data,
                experience = experienceResponse
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetSession(string Id)
        {
            try
            {
                initChanel();
                var response = await experienceSessionClient.GetAsync(new IdLangRequest
                {
                    Id = Id,
                    LangCode = CurrentLanguage
                });

                var attraction = new AttractionStruct();
                if (response != null && !string.IsNullOrEmpty(response.AttractionId) && !response.AttractionId.Equals(Guid.Empty.ToString()))
                {
                    attraction = await attractionServicesClient.GetByAsync(new IdRequest
                    {
                        Id = response.AttractionId
                    });
                }

                var destination = new DestinationStruct();
                if (!string.IsNullOrEmpty(attraction.DestinationId) && attraction.DestinationId != Guid.Empty.ToString())
                {
                    destination = await destinationServicesClient.GetByAsync(new IdRequest
                    {
                        Id = attraction.DestinationId
                    });
                }

                var result = new
                {
                    Experience = mapper.Map<EExperienceSession>(response),
                    Attraction = mapper.Map<EAttraction>(attraction),
                    Destination = mapper.Map<EDestination>(destination)
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Session Fail. Message: {ex.Message}");
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ExperienceModel model)
        {
            initChanel();
            int status = 0;
            string mess = "";
            ExperienceSessionStruct data = new ExperienceSessionStruct();
            try
            {
                if (string.IsNullOrEmpty(model.Id)
                        || string.IsNullOrEmpty(model.Title)
                        || string.IsNullOrEmpty(model.Description))
                {
                    mess = "Data Invalid";
                }
                else
                {
                    var session = await experienceSessionClient.GetAsync(new IdLangRequest
                    {
                        Id = model.Id,
                        LangCode = CurrentLanguage
                    });

                    if (session != null)
                    {
                        session.Title = model.Title;
                        session.Detail = model.Description;
                        session.AttractionId = model.AttractionId;

                        if (model.Files != null && model.Files.Count > 0)
                        {
                            var i = 0;
                            session.Images.Clear();
                            foreach (var img in model.Files)
                            {
                                i++;
                                session.Images.Add(new ExperienceSessionImageStruct
                                {
                                    ImagerUrl = img.Replace(UrlList.FileServer, ""),
                                    Ordinal = i
                                });
                            }
                        }

                        var response = await experienceSessionClient.EditAsync(session);
                        mess = response.Message;
                        if (int.Parse(response.StatusCode) == 200)
                        {
                            status = 1;
                            data = await experienceSessionClient.GetAsync(new IdLangRequest
                            {
                                Id = model.Id,
                                LangCode = CurrentLanguage
                            });
                        }
                    }
                    else
                    {
                        mess = "Data Invalid!";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Update Session failed. Message: {ex.Message}");
                mess = ex.Message;
            }

            var result = new
            {
                status = status,
                mess = mess,
                data = data
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ExperienceSessionModel model)
        {
            initChanel();
            int status = 0;
            string mess = "";
            try
            {
                var response = await experienceSessionClient.DeleteAsync(new IdRequest
                {
                    Id = model.Id
                });

                if (int.Parse(response.StatusCode) == 200)
                {
                    status = 1;
                    var experienceSessions = await experienceSessionClient.GetListAsync(new IdLangRequest
                    {
                        Id = model.ExperienceId
                    });
                    if (experienceSessions == null || experienceSessions.TotalRecords == 0)
                    {
                        await experienceClient.DeleteAsync(new IdRequest
                        {
                            Id = model.ExperienceId
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Session failed. Message: {ex.Message}");
                mess = ex.Message;
            }

            var result = new
            {
                status = status,
                mess = mess
            };

            return Ok(result);
        }
    }
}
