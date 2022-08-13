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
    public class DestinationController : BaseController<DestinationController>
    {
        private GrpcChannel dbChannel;

        private DestinationServices.DestinationServicesClient destinationClient;
        private LanguageServices.LanguageServicesClient languageClient;
        private ContinentServices.ContinentServicesClient continentServicesClient;
        private CountryServices.CountryServicesClient countryServicesClient;
        private CityServices.CityServicesClient cityServicesClient;
        private StateServices.StateServicesClient stateServicesClient;
        private DestinationLanguageServices.DestinationLanguageServicesClient destinationLanguageServicesClient;
        public DestinationController(IMapper mapper, ILogger<DestinationController> logger) : base(logger, mapper)
        {
        }

        public IActionResult Index(string msg = "")
        {
            var model = new DestinationViewModel();
            if (!string.IsNullOrWhiteSpace(msg))
                model.LoadMessageCache(msg);
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> GetDatatable(DestinationListViewModel model)
        {
            try
            {
                initChanel();
                model.langCode = (CurrentLanguage);
                var request = mapper.Map<DestinationFilter>(model);
                request.Paging = mapper.Map<PagingType>(model.Pagination);
              
                
                var response = await destinationClient.GetListAsync(request);
                model.Pagination.Total = response.TotalRecords;
                model.Pagination.Pages = (int)Math.Ceiling(response.TotalRecords * 1.0 / model.Pagination.Perpage);
                if (model.Sort != null)
                {
                    model.Pagination.Field = model.Sort.Field;
                    model.Pagination.Sort = model.Sort.Sort;
                }
                var result = new
                {
                    meta = model.Pagination,
                    data = mapper.Map<List<EDestination>>(response.Data)
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Destination List failed!. ERROR: {ex.Message}");
            }
            var emptyResult = new
            {
                meta = new KTPagination(),
                data = new List<EDestination>()
            };
            return Json(emptyResult);
        }
        public async Task<IActionResult> AddEdit(string id , string msg = "")
        {
            var model = new DestinationViewModel();
            model.Languages = Languages;
            try
            {
                initChanel();
                if (!string.IsNullOrEmpty(id))
                {
                    var entry = await destinationClient.GetByAsync(new IdRequest { Id = id });
                    if (!string.IsNullOrEmpty(entry.Id))
                    {
                        model.Destination = mapper.Map<EDestination>(entry);
                        model.ActionMode = FormActionMode.Edit;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"AddEdit:: Get RecordID '{id}' failed!");
                model.AlertError("Data not found!");
            }

            if (!string.IsNullOrWhiteSpace(msg))
                model.LoadMessageCache(msg);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(DestinationViewModel model)
        {
             var plaintext = dStr.ToASCII(model.Destination.DefaultName).ToLower();

            model.Destination.RouteUri = Regex.Replace(plaintext, @"\s+", "-");
            model.Destination.CountryId = model.Destination.CountryId == null ? 0 : model.Destination.CountryId;
            //model.Destination.CityId = 0;
            //model.Destination.StateId = 0;
            if (ModelState.IsValid && model != null)
            {
                try
                {
                    initChanel();
                    ResponseMessage response = null;
                    var cityGet =await cityServicesClient.GetAsync(new IdRequest { Id = model.Destination.CityId.ToString() });                    
                    var stateGet = await stateServicesClient.GetAsync(new IdRequest { Id = model.Destination.StateId.ToString() });                    
                    var countryGet = await countryServicesClient.GetAsync(new IdRequest { Id = model.Destination.CountryId.ToString() });
                    //var continentGet = continentServicesClient.GetAsync(new IdRequest { Id = model.Destination.Continent..ToString() });   
                    model.Destination.CountryName = countryGet.Name;
                    model.Destination.StateName = stateGet.Name;
                    model.Destination.CityName = cityGet.Name;

                    if (FormActionMode.Edit.Equals(model.ActionMode)
                        && "delete".Equals(model.CommandName))
                    {
                        model.ActionMode = FormActionMode.Delete;
                        response = await destinationClient.DeleteAsync(new IdRequest { Id = model.City.Id.ToString() });
                    }
                    else
                    {
                        var request = mapper.Map<DestinationStruct>(model.Destination);
                        if (FormActionMode.Edit.Equals(model.ActionMode))
                        {
                            response = await destinationClient.EditAsync(request);
                        }
                        else
                        {
                            response = await destinationClient.AddAsync(request);
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
                    logger.LogError(ex, $"{model.ActionMode} Destination failed!");
                    model.AlertError($"{model.ActionMode} Destination failed!");
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
        public async Task<JsonResult> Search(string keyword = "")
        {
          var result = new Select2Datasource();
            try
            {          
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    initChanel();
                    var request = new DestinationFilter
                    {
                        LangCode = CurrentLanguage,
                        Name = keyword
                    };
                    var response = await destinationClient.GetListAsync(request);
                    if (response.TotalRecords > 0)
                    {
                        // result.Results = response.Data.Select(e => new Select2DatasourceItem(e.Id.ToString(), $"{e.DefaultName} ")).ToList();                        
                        var model = mapper.Map<IList<EDestination>>(response.Data);

                        foreach (var item in model)
                        {
                            Select2DatasourceItem itemSelect = new Select2DatasourceItem();
                            itemSelect.Id = item.Id.ToString();
                            itemSelect.Text = "";
                            itemSelect.Text += (item.Continent != null ? " " + item.Continent + " - " : "");
                            itemSelect.Text += (item.CountryName != null ? " " + item.CountryName + " - " : "");
                            itemSelect.Text += (item.StateName != null ? " " + item.StateName + " - " : "");
                            itemSelect.Text += (item.CityName != null ? " " + item.CityName + " - " : "");
                            itemSelect.Text += (item.DefaultName != null ? " " + item.DefaultName : "");
                            result.Results.Add(itemSelect);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Search Destination '{keyword}' failed!. ERROR: {ex.Message}");
            }
            return Json(result);
        }
        [HttpDelete]
        public async Task<JsonResult> Delete(string id)
        {
            AlertMessage respMsg = null;
            try
            {
                initChanel();
                var response = await destinationClient.DeleteAsync(new IdRequest { Id = id.ToString() });
                respMsg = mapper.Map<AlertMessage>(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete Destination ID '{id}' failed!. ERROR: {ex.Message}");
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
            var model = new EDestination();
            try
            {
                initChanel();
                var entry = await destinationClient.GetByAsync(new IdRequest { Id = id.ToString() });
                if (string.IsNullOrEmpty(entry.Id))
                    return PartialView("Error");

                model = mapper.Map<EDestination>(entry);
                if (model == null)
                    return PartialView("Error");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Detail:: Get destination ID '{id}' failed!");
                return PartialView("Error");
            }
            return PartialView(model);
        }
        public async Task<JsonResult> DestinationLanguageAddEdit(DestinationLanguageViewModel model)
        {
            try
            {
                initChanel();
                if(!string.IsNullOrEmpty(model.DestinationLanguage.LangCode))
                { 
                    var request = mapper.Map<DestinationLanguageStruct>(model.DestinationLanguage);
                    var item = await destinationLanguageServicesClient.GetAsync(
                                new IdLangRequest
                                {
                                    Id = model.DestinationLanguage.DestinationId.ToString(),
                                    LangCode = model.DestinationLanguage.LangCode
                                });
                    if (model.ActionMode == FormActionMode.Add)
                    {
                        if (item.Id != null)
                        {
                            var mesg = new AlertMessage
                            {
                                Message = "Title Destination with " + model.DestinationLanguage.LangCode + "are already exist !",
                                Status = (int)EnumMessageStatus.Info,
                                StatusCode = "300"
                            };
                            model.AlertMessages.Add(mesg);


                        }
                        else
                        {
                            var result = await destinationLanguageServicesClient.AddAsync(request);
                            if (result != null)
                            {
                                var alertmsg = mapper.Map<AlertMessage>(result);
                                model.AlertMessages.Add(alertmsg);
                            }
                        }
                    }
                    else if (model.ActionMode == FormActionMode.Delete)
                    {
                        var result = await destinationLanguageServicesClient.DeleteAsync(new IdRequest { Id = request.Id });
                        if (result != null)
                        {
                            var alertmsg = mapper.Map<AlertMessage>(result);
                            model.AlertMessages.Add(alertmsg);
                        }
                    }
                    else
                    {
                        if( model.DestinationLanguage.Id != Guid.Empty )
                        { 
                            var result = await destinationLanguageServicesClient.EditAsync(request);
                            if (result != null)
                            {
                                var alertmsg = mapper.Map<AlertMessage>(result);
                                model.AlertMessages.Add(alertmsg);
                            }
                        }
                        else
                        {
                            model.AlertMessages.Add(new AlertMessage { StatusValue = EnumMessageStatus.Danger, Message = "you have'nt selected current content to update" });
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
                logger.LogError(ex, $"add Destination Language failed!. ERROR: {ex.Message}");
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
        public async Task<JsonResult> DestinationLanguageGet(DestinationLanguageViewModel model)
        {
            try
            {
                initChanel();
                var result = await destinationLanguageServicesClient.GetAsync(new IdLangRequest { Id = model.DestinationLanguage.DestinationId.ToString(), LangCode = model.DestinationLanguage.LangCode });
                if (result != null)
                {
                    model.DestinationLanguage = mapper.Map<EDestinationLanguage>(result);
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
                logger.LogError(ex, $"get Destination Language failed!. ERROR: {ex.Message}");
                model = new DestinationLanguageViewModel();
                return Json(model);
            }
            return Json(model);
        }
        private void initChanel()
        {
            dbChannel = new GrpcChannelHepper().CreateDbDataChanel(AccessToken);
            destinationClient = new DestinationServices.DestinationServicesClient(dbChannel);
            languageClient = new LanguageServices.LanguageServicesClient(dbChannel);
            continentServicesClient = new ContinentServices.ContinentServicesClient(dbChannel);
            countryServicesClient = new CountryServices.CountryServicesClient(dbChannel);
            stateServicesClient = new StateServices.StateServicesClient(dbChannel);
            cityServicesClient = new CityServices.CityServicesClient(dbChannel);
            destinationLanguageServicesClient = new DestinationLanguageServices.DestinationLanguageServicesClient(dbChannel);

        }
    }
}
