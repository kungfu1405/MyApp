using AutoMapper;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Backend.Commons;
using DbData.Protos;
using UserDb.Protos;
using Mic.Core.Entities;
using DbData.Entities;
using Web.Backend.Models;
using Mic.UserDb.Entities;
using Mic.Core.Website;
using Mic.Core.DataTypes;
using System.Text.RegularExpressions;

namespace Web.Backend.Controllers
{
    public class AttractionController : BaseController<AttractionController>
    {
        //private readonly IMapper mapper;
        private GrpcChannel dbChannel;

        private AttractionServices.AttractionServicesClient attractionClient;
        private LanguageServices.LanguageServicesClient languageClient;
        private AttractionLanguageServices.AttractionLanguageServicesClient attractionLanguageServicesClient;
        private TagServices.TagServicesClient tagServicesClient;
        


        public AttractionController(IMapper mapper, ILogger<AttractionController> logger) : base(logger, mapper)
        {
        }

        public IActionResult Index(string msg = "")
        {
            var model = new AttractionListViewModel();

            if (!string.IsNullOrWhiteSpace(msg))
                model.LoadMessageCache(msg);
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> GetDatatable(AttractionListViewModel model)
        {
            try
            {
                initChanel();
                model.LangCode = (CurrentLanguage);                
                var request = mapper.Map<AttractionFilter>(model);
                request.Paging = mapper.Map<PagingType>(model.Pagination);

                var response = await attractionClient.GetListAsync(request);
                model.Pagination.Total = response.TotalRecords;
                model.Pagination.Pages = (int)Math.Ceiling(response.TotalRecords * 1.0 / model.Pagination.Perpage);
                if (model.Sort != null)
                {
                    model.Pagination.Field = model.Sort.Field;
                    model.Pagination.Sort = model.Sort.Sort;
                }
                //var list =  mapper.Map<List<EAttraction>>(response.Data);
                //foreach (var item in list)
                //{
                    
                //    if ((item.AttractionTypes & (int)EnumAttractionType.PlaceToEat) != 0)
                //    {
                //        item.AttractionTypes
                //    }    
                //}
                var result = new
                {
                    meta = model.Pagination,
                    data = mapper.Map<List<EAttraction>>(response.Data)
                    
                };
              
                return Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Attraction List failed!. ERROR: {ex.Message}");
            }
            var emptyResult = new
            {
                meta = new KTPagination(),
                data = new List<EAttraction>()
            };
            return Json(emptyResult);
        }
        public async Task<IActionResult> AddEdit(string id , string msg = "")
        {
            var model = new AttractionViewModel();

            model.Languages = Languages;
            try
            {
                initChanel();
                if (!string.IsNullOrEmpty(id))
                {
                    var entry = await attractionClient.GetByAsync(new IdRequest { Id = id });
                    if (!string.IsNullOrEmpty(entry.Id))
                    {
                        model.Attraction = mapper.Map<EAttraction>(entry);
                        if(model.Attraction.AttractionTags.Count>0)
                        {
                            string stringTag="";
                            int i = 0;
                            foreach (var item in model.Attraction.AttractionTags)
                            {                             
                                var tagResult = await tagServicesClient.GetAsync(new IdRequest { Id = item.TagId.ToString() });
                                ETag tag = mapper.Map<ETag>(tagResult);
                                stringTag += (i==0?tag.Name : "," + tag.Name) ; 
                                i++;
                            }
                            model.StringTags = stringTag;
                        }    
                        model.ActionMode = FormActionMode.Edit;
                        model.AttractionTypeViews = new List<AttractionTypeView>();
                        EnumAttractionType AttractionTypeOption = (EnumAttractionType)model.Attraction.AttractionTypes;
                        #region option
                        if ((AttractionTypeOption & EnumAttractionType.PlaceToEat) != 0)
                        {
                            model.AttractionTypeViews.Add(new AttractionTypeView { Checked = true, EnumAttractionType = EnumAttractionType.PlaceToEat });
                        }
                        else
                        {
                            model.AttractionTypeViews.Add(new AttractionTypeView { Checked = false, EnumAttractionType = EnumAttractionType.PlaceToEat });
                        }
                        if ((AttractionTypeOption & EnumAttractionType.PlaceToStay) != 0)
                        {
                            model.AttractionTypeViews.Add(new AttractionTypeView { Checked = true, EnumAttractionType = EnumAttractionType.PlaceToStay });
                        }
                        else
                        {
                            model.AttractionTypeViews.Add(new AttractionTypeView { Checked = false, EnumAttractionType = EnumAttractionType.PlaceToStay });

                        }
                        if ((AttractionTypeOption & EnumAttractionType.PlaceToVisit) != 0)
                        {
                            model.AttractionTypeViews.Add(new AttractionTypeView { Checked = true, EnumAttractionType = EnumAttractionType.PlaceToVisit });
                        }
                        else
                        {
                            model.AttractionTypeViews.Add(new AttractionTypeView { Checked = false, EnumAttractionType = EnumAttractionType.PlaceToVisit });
                        }
                        //tour class
                         EnumTouristClass TouristClassOption = (EnumTouristClass)model.Attraction.TouristClasses;
                        model.TouristClassViews = new List<TouristClassView>();
                        if ((TouristClassOption & EnumTouristClass.Budget) != 0)
                        {
                            model.TouristClassViews.Add(new TouristClassView { Checked = true, EnumTouristClass = EnumTouristClass.Budget});
                        }
                        else
                        {
                            model.TouristClassViews.Add(new TouristClassView { Checked = false, EnumTouristClass = EnumTouristClass.Budget });
                        }
                        if ((TouristClassOption & EnumTouristClass.MidRange) != 0)
                        {
                            model.TouristClassViews.Add(new TouristClassView { Checked = true, EnumTouristClass = EnumTouristClass.MidRange });
                        }
                        else
                        {
                            model.TouristClassViews.Add(new TouristClassView { Checked = false, EnumTouristClass = EnumTouristClass.MidRange });
                        }
                        if ((TouristClassOption & EnumTouristClass.Luxury) != 0)
                        {
                            model.TouristClassViews.Add(new TouristClassView { Checked = true, EnumTouristClass = EnumTouristClass.Luxury });
                        }
                        else
                        {
                            model.TouristClassViews.Add(new TouristClassView { Checked = false, EnumTouristClass = EnumTouristClass.Luxury });
                        }
                        EnumTouristObject TouristObjectOption = (EnumTouristObject)model.Attraction.TouristObjects;
                        model.TouristObjectViews = new List<TouristObjectView>();
                        if((TouristObjectOption & EnumTouristObject.Couple) ==0)
                        {
                            model.TouristObjectViews.Add(new TouristObjectView { Checked = false, EnumTouristObject = EnumTouristObject.Couple });
                        }    
                        else
                        {
                            model.TouristObjectViews.Add(new TouristObjectView { Checked = true, EnumTouristObject = EnumTouristObject.Couple });
                        }
                          if((TouristObjectOption & EnumTouristObject.Family) ==0)
                        {
                            model.TouristObjectViews.Add(new TouristObjectView { Checked = false, EnumTouristObject = EnumTouristObject.Family });
                        }    
                        else
                        {
                            model.TouristObjectViews.Add(new TouristObjectView { Checked = true, EnumTouristObject = EnumTouristObject.Family });
                        }
                          if((TouristObjectOption & EnumTouristObject.Friends) ==0)
                        {
                            model.TouristObjectViews.Add(new TouristObjectView { Checked = false, EnumTouristObject = EnumTouristObject.Friends });
                        }    
                        else
                        {
                            model.TouristObjectViews.Add(new TouristObjectView { Checked = true, EnumTouristObject = EnumTouristObject.Friends });
                        }
                          if((TouristObjectOption & EnumTouristObject.Group) ==0)
                        {
                            model.TouristObjectViews.Add(new TouristObjectView { Checked = false, EnumTouristObject = EnumTouristObject.Group });
                        }    
                        else
                        {
                            model.TouristObjectViews.Add(new TouristObjectView { Checked = true, EnumTouristObject = EnumTouristObject.Group });
                        }
                         if((TouristObjectOption & EnumTouristObject.Solo) ==0)
                        {
                            model.TouristObjectViews.Add(new TouristObjectView { Checked = false, EnumTouristObject = EnumTouristObject.Solo });
                        }    
                        else
                        {
                            model.TouristObjectViews.Add(new TouristObjectView { Checked = true, EnumTouristObject = EnumTouristObject.Solo });
                        }
                        EnumPropertyGroup PropertyGroupOption = (EnumPropertyGroup)model.Attraction.PropertyGroups;
                        model.PropertyGroupViews = new List<PropertyGroupView>();
                        if ((PropertyGroupOption & EnumPropertyGroup.Beach) == 0)
                        {
                            model.PropertyGroupViews.Add(new PropertyGroupView { Checked = false, EnumPropertyGroup = EnumPropertyGroup.Beach });
                        }
                        else
                        {
                            model.PropertyGroupViews.Add(new PropertyGroupView { Checked = true, EnumPropertyGroup = EnumPropertyGroup.Beach });
                        }
                        if ((PropertyGroupOption & EnumPropertyGroup.Hotel) == 0)
                        {
                            model.PropertyGroupViews.Add(new PropertyGroupView { Checked = false, EnumPropertyGroup = EnumPropertyGroup.Hotel });
                        }
                        else
                        {
                            model.PropertyGroupViews.Add(new PropertyGroupView { Checked = true, EnumPropertyGroup = EnumPropertyGroup.Hotel });
                        }
                        if ((PropertyGroupOption & EnumPropertyGroup.Restaurant) == 0)
                        {
                            model.PropertyGroupViews.Add(new PropertyGroupView { Checked = false, EnumPropertyGroup = EnumPropertyGroup.Restaurant });
                        }
                        else
                        {
                            model.PropertyGroupViews.Add(new PropertyGroupView { Checked = true, EnumPropertyGroup = EnumPropertyGroup.Restaurant });
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"AddEdit:: Get RecordID '{id}' failed!");
                model.AlertError("Data not found!");
            }

            if (!string.IsNullOrWhiteSpace(msg))
            {
                if (msg.IndexOf('&') > 0)
                {
                    string[] words = msg.Split('&');
                    msg = words[words.Length];
                }
               
            }
                model.LoadMessageCache(msg);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(AttractionViewModel model)
        {
            List<string> stringTags = null;
            if(!string.IsNullOrEmpty(model.StringTags))
            {
                stringTags = model.StringTags.Split(',').ToList<string>();
            }            
            IList<ETag> listTags = new List<ETag>();
            if (stringTags != null)
            {
                foreach (var item in stringTags)
                {
                    ETag tag = new ETag { Id = Guid.NewGuid(), Name = item };
                    listTags.Add(tag);
                }
            }
            model.Attraction.Tags = listTags;
            List<int> optionInts = new List<int>();
            EnumAttractionType optionAttractionNumber = 0;
            for (int i = 0; i < model.AttractionTypeViews.Count; i++)
            {                
                if (model.AttractionTypeViews[i].Checked == true)
                {
                    //option = model.AttractionTypeViews[i].EnumAttractionType;
                    optionInts.Add((int)model.AttractionTypeViews[i].EnumAttractionType);
                    optionAttractionNumber |= model.AttractionTypeViews[i].EnumAttractionType;
                }    
            }
            model.Attraction.AttractionTypes = (int)optionAttractionNumber;

            optionInts.Clear();
            EnumTouristClass optionTourClassNumber = 0;
            for (int i = 0; i < model.TouristClassViews.Count; i++)
            {
                if(model.TouristClassViews[i].Checked == true)
                {
                    optionInts.Add((int)model.TouristClassViews[i].EnumTouristClass);
                    optionTourClassNumber |= model.TouristClassViews[i].EnumTouristClass;
                }    
            }
            model.Attraction.TouristClasses = (int)optionTourClassNumber;

             optionInts.Clear();
            EnumPropertyGroup optionPropertyGroupNumber = 0;
            for (int i = 0; i < model.PropertyGroupViews.Count; i++)
            {
                if(model.PropertyGroupViews[i].Checked == true)
                {
                    optionInts.Add((int)model.PropertyGroupViews[i].EnumPropertyGroup);
                    optionPropertyGroupNumber |= model.PropertyGroupViews[i].EnumPropertyGroup;
                }    
            }
            model.Attraction.PropertyGroups = (int)optionPropertyGroupNumber;

            optionInts.Clear();
            EnumTouristObject optionTourObjectNumber = 0;
            for (int i = 0; i < model.TouristObjectViews.Count; i++)
            {
                if (model.TouristObjectViews[i].Checked == true)
                {
                    optionInts.Add((int)model.TouristObjectViews[i].EnumTouristObject);
                    optionTourObjectNumber |= model.TouristObjectViews[i].EnumTouristObject;
                }
            }
            model.Attraction.TouristObjects = (int)optionTourObjectNumber;

            //EnumAttractionType option = EnumAttractionType.PlaceToEat;
            //EnumAttractionType option2 = EnumAttractionType.PlaceToEat | EnumAttractionType.PlaceToVisit ;
            //EnumAttractionType option3 = EnumAttractionType.PlaceToEat | EnumAttractionType.PlaceToVisit | EnumAttractionType.PlaceToStay;
            //EnumAttractionType option4 = (option & EnumAttractionType.PlaceToVisit);

            //if ((optionTotal & EnumAttractionType.PlaceToEat) != 0)
            //{
            //    string a = "";
            //}    
            //if((optionTotal & EnumAttractionType.PlaceToStay) != 0)
            //{
            //    string b = "";
            //}    
            //   if((optionTotal & EnumAttractionType.PlaceToVisit) != 0)
            //{
            //    string b = "";
            //}    

            var plaintext = dStr.ToASCII(model.Attraction.DefaultName).ToLower();           
            model.Attraction.RouteUri = Regex.Replace(plaintext, @"\s+", "-");
            if (ModelState.IsValid && model != null)
            {
                try
                {
                    initChanel();
                    ResponseMessage response = null;                      
                    if (FormActionMode.Edit.Equals(model.ActionMode)
                            && "delete".Equals(model.CommandName))
                        {
                            model.ActionMode = FormActionMode.Delete;
                            response = await attractionClient.DeleteAsync(new IdRequest { Id = model.Attraction.Id.ToString() });
                        }
                        else
                        {
                            var request = mapper.Map<AttractionStruct>(model.Attraction);
                            if (FormActionMode.Edit.Equals(model.ActionMode))
                            {
                                response = await attractionClient.EditAsync(request);                                
                             }
                            else
                            {
                                response = await attractionClient.AddAsync(request);
                               
                            }
                        }
                    if (response != null)
                    {
                        var alertmsg = mapper.Map<AlertMessage>(response);
                        model.AlertMessages.Add(alertmsg);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"{model.ActionMode} Attraction failed!");
                    model.AlertError($"{model.ActionMode} Attraction failed!");
                }
            }           
            var msgCode = model.SaveMessageCache();
            if (!model.IsSuccessStatus()
                || "continue".Equals(model.CommandName))
            {
                if (model.ReturnUrl.IndexOf('?') > 0)
                    model.ReturnUrl += "&msg=" + msgCode;
                else
                    model.ReturnUrl += "?msg=" + msgCode;
                return Redirect(model.ReturnUrl);
            }
            return RedirectToAction("Index", new { msg = msgCode });
        }
        [HttpDelete]
        public async Task<JsonResult> Delete(string id)
        {
            AlertMessage respMsg = null;
            try
            {
                initChanel();
                var response = await attractionClient.DeleteAsync(new IdRequest { Id = id.ToString() });
                respMsg = mapper.Map<AlertMessage>(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Attraction ID '{id}' failed!. ERROR: {ex.Message}");
                respMsg = new AlertMessage
                {
                    StatusValue = EnumMessageStatus.Danger,
                    Message = "Delete record failed!",
                    StatusCode = "500"
                };
            }

            return Json(respMsg);
        }
        public async Task<PartialViewResult> Detail(string id)
        {
            var model = new EAttraction();
            try
            {
                initChanel();
                var entry = await attractionClient.GetByAsync(new IdRequest { Id = id.ToString() });
                if (string.IsNullOrEmpty(entry.Id))
                    return PartialView("Error");

                model = mapper.Map<EAttraction>(entry);
                if (model == null)
                    return PartialView("Error");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Detail:: Get Attraction ID '{id}' failed!");
                return PartialView("Error");
            }
            return PartialView(model);
        }

        public async Task<JsonResult> LanguageSearch()
        {
            IList<ELanguage> list = Languages;

            return Json(list);
        }
        public async Task<JsonResult> AttractionLanguageAddEdit(AttractionLanguageViewModel model)
        {                      
            try
            {
                initChanel();
                if (!string.IsNullOrEmpty(model.AttractionLanguage.LangCode))
                {
                    var request = mapper.Map<AttractionLanguageStruct>(model.AttractionLanguage);
                    var item = await attractionLanguageServicesClient.GetAsync(
                                new IdLangRequest
                                {
                                    Id = model.AttractionLanguage.AttractionId.ToString(),
                                    LangCode = model.AttractionLanguage.LangCode
                                });
                    if (model.ActionMode == FormActionMode.Add)
                    {
                        if (item.Id != null)
                        {
                            var mesg = new AlertMessage
                            {
                                Message = "Title Attraction with " + model.AttractionLanguage.LangCode + "are already exist !",
                                Status = (int)EnumMessageStatus.Info,
                                StatusCode = "300"
                            };
                            model.AlertMessages.Add(mesg);


                        }
                        else
                        {
                            var result = await attractionLanguageServicesClient.AddAsync(request);
                            if (result != null)
                            {
                                var alertmsg = mapper.Map<AlertMessage>(result);
                                model.AlertMessages.Add(alertmsg);
                            }
                        }
                    }
                    else if (model.ActionMode == FormActionMode.Delete)
                    {
                        var result = await attractionLanguageServicesClient.DeleteAsync(new IdRequest { Id = request.Id });
                        if (result != null)
                        {
                            var alertmsg = mapper.Map<AlertMessage>(result);
                            model.AlertMessages.Add(alertmsg);
                        }
                    }
                    else
                    {
                        var result = await attractionLanguageServicesClient.EditAsync(request);
                        if (result != null)
                        {
                            var alertmsg = mapper.Map<AlertMessage>(result);
                            model.AlertMessages.Add(alertmsg);
                        }
                    }
                }
                else
                {
                    model.AlertMessages.Add(new AlertMessage { StatusValue = EnumMessageStatus.Danger, Message = "language is required" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"add Attraction Language failed!. ERROR: {ex.Message}");
                model.AlertMessages.Add(new AlertMessage {StatusValue=EnumMessageStatus.Danger , Message = ex.Message });
            }
            var emptyResult = new ResponseMessage();
            var msgCode = model.SaveMessageCache();    
            if (model.ReturnUrl.IndexOf("?msg") > 0)
            {
                model.ReturnUrl = model.ReturnUrl.Replace(model.ReturnUrl.Substring(model.ReturnUrl.IndexOf("?msg")), string.Empty);
                model.ReturnUrl += "?msg=" + msgCode;
            }
            else
            {
                model.ReturnUrl += "?msg=" + msgCode;
            }
            return Json(model.ReturnUrl);
        }
        public async Task<JsonResult> AttractionLanguageGet(AttractionLanguageViewModel model)
        {
            try
            {
                initChanel();                
                var result = await attractionLanguageServicesClient.GetAsync(new IdLangRequest { Id = model.AttractionLanguage.AttractionId.ToString() , LangCode= model.AttractionLanguage.LangCode });
                if (result != null)
                {
                    model.AttractionLanguage = mapper.Map<EAttractionLanguage>(result);
                    return Json(model);
                }
                else
                {
                    model.ReturnUrl = "";
                    model.AlertMessages.Add(new AlertMessage { Message = "" });
                }    
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"get Attraction Language failed!. ERROR: {ex.Message}");
                 model = new AttractionLanguageViewModel();
                return Json(model);
            }
            return Json(model);
        }
        private void initChanel()
        {
            dbChannel = new GrpcChannelHepper().CreateDbDataChanel(AccessToken);
            attractionClient = new AttractionServices.AttractionServicesClient(dbChannel);
            languageClient = new LanguageServices.LanguageServicesClient(dbChannel);
            attractionLanguageServicesClient = new AttractionLanguageServices.AttractionLanguageServicesClient(dbChannel);
            tagServicesClient = new TagServices.TagServicesClient(dbChannel);
        }
    }
}
