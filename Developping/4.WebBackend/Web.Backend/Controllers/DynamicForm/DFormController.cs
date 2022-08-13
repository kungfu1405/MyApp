using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using DynamicData.Protos;
using Web.Backend.Commons;
using DynamicData.Entities;
using Web.Backend.Models.DynamicForm;
using AutoMapper;
using System.Data;
using Mic.Core.MemCache;
using System.Collections.Generic;
using Mic.Core.Entities;
using Mic.Core.DataTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Mic.Core.Website;

namespace Web.Backend.Controllers.DynamicForm
{
    //[Authorize]
    public class DFormController : BaseController<DFormController>
    {
        private GrpcChannel channel;
        private SysTable.SysTableClient tableClient;
        private DData.DDataClient ddataClient;

        public DFormController(IMapper mapper, ILogger<DFormController> logger) : base(logger, mapper)
        {
        }

        public async Task<IActionResult> Index(string fid, string msg = "")
        {
            var model = new DFormListViewModel { TableId = fid };
            try
            {
                initChanel();
                var response = await tableClient.GetAsync(new IdRequest { Id = fid }); //table ID
                //check Table's accessable permission
                if (response.Table == null || !response.Table.Enabled
                    || ((int)EnumTablePermission.View & response.Table.Permissions) == 0
                    || !response.Columns.Any())
                    return RedirectToAction("AccessDenied", "Account");

                model = mapper.Map<DFormListViewModel>(response);
                if (model.Table == null || model.Columns == null || !model.Columns.Any())
                    return RedirectToAction("Error", "Home");
                await loadRefTables(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"DForm:: Get structure of Table '{fid}' failed!");
                model.AlertError("Get data structure failed! " + fid);
                return RedirectToAction("Error", "Home", new { msg = model.SaveMessageCache() });
            }
            if (!string.IsNullOrWhiteSpace(msg))
                model.LoadMessageCache(msg);
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetDatatable(DFormListViewModel model)
        {
            try
            {
                initChanel();
                var filters = new QueryFilter { TableId = model.TableId };
                if (model.Pagination != null)
                {
                    filters.Paging = new PagingType
                    {
                        Start = (model.Pagination.Page - 1) * model.Pagination.Perpage,
                        Length = model.Pagination.Perpage
                    };
                }
                if (model.Conditions != null && model.Conditions.Any())
                {
                    filters.Conditions.Add(model.Conditions.Select(e => new QueryColData
                    {
                        ColumnId = e.Key.Replace("\"", ""),
                        Value = dStr.ToString(e.Value)
                    }));
                }
                if (model.Sort != null && !string.IsNullOrEmpty(model.Sort["field"]))
                {
                    filters.Sort = new QueryColData
                    {
                        ColumnId = model.Sort["field"],
                        Value = string.IsNullOrEmpty(model.Sort["sort"]) ? "asc" : model.Sort["sort"]
                    };
                }
                var response = await ddataClient.GetListAsync(filters);
                model.Pagination.Total = response.TotalRecords;
                model.Pagination.Pages = (int)Math.Ceiling(response.TotalRecords * 1.0 / model.Pagination.Perpage);
                model.Pagination.Field = response.Sort.ColumnId;
                model.Pagination.Sort = response.Sort.Value;

                var result = new
                {
                    meta = model.Pagination,
                    data = toDataTable(response)
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get datatable of Table '{model.TableId}' failed!. ERROR: {ex.Message}");
            }
            return null;
        }

        public async Task<JsonResult> Search(SysColumnStruct refCol, string keyword = "")
        {
            var result = new Select2Datasource();
            try
            {
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    initChanel();
                    result = await searcRefTableData(refCol, keyword, true);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get datatable of Table '{refCol.ReferenceTableId}' failed!. ERROR: {ex.Message}");
            }
            return Json(result);
        }

        public async Task<FileResult> Export(DFormListViewModel model)
        {
            try
            {
                var filters = new QueryFilter { TableId = model.TableId };
                if (model.Conditions != null && model.Conditions.Any())
                {
                    filters.Conditions.Add(model.Conditions.Where(e => e.Value != "null").Select(e => new QueryColData
                    {
                        ColumnId = e.Key.Replace("\"", ""),
                        Value = dStr.ToString(e.Value)
                    }));
                }
                if (model.Sort != null && !string.IsNullOrEmpty(model.Sort["field"]))
                {
                    filters.Sort = new QueryColData
                    {
                        ColumnId = model.Sort["field"],
                        Value = string.IsNullOrEmpty(model.Sort["sort"]) ? "asc" : model.Sort["sort"]
                    };
                }
                initChanel();
                var response = await ddataClient.ExportAsync(filters);
                if (response.Content != null)
                {
                    var fileName = $"Export_{DateTime.Now.ToString("ddMMMyy_HHmmss")}.xlsx";
                    string contentType;
                    var provider = new FileExtensionContentTypeProvider();
                    if (!provider.TryGetContentType(fileName, out contentType))
                    {
                        contentType = "application/octet-stream";
                    }

                    return File(response.Content.ToByteArray(), contentType, fileName); // this is the key!
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get datatable of Table '{model.TableId}' failed!. ERROR: {ex.Message}");
            }
            return null;
        }

        public async Task<PartialViewResult> Detail(string fid)
        {
            var model = new DFormViewModel { TableId = fid };

            try
            {
                initChanel();
                var tableResponse = await tableClient.GetAsync(new IdRequest { Id = fid }); //table ID
                //check Table's accessable permission
                if (tableResponse.Table == null
                    || ((int)EnumTablePermission.View & tableResponse.Table.Permissions) == 0
                    || !tableResponse.Table.Enabled
                    || !tableResponse.Columns.Any())
                    return PartialView("Error");

                model = mapper.Map<DFormViewModel>(tableResponse);
                if (model.Table == null || model.Columns == null || !model.Columns.Any())
                    return PartialView("Error");

                // check and get query params if exists (for edit record)
                var query = new QueryRequest { TableId = fid };
                foreach (var col in model.Columns)
                {
                    if (!string.IsNullOrEmpty(Request.Query[col.Id]))
                    {
                        query.Conditions.Add(new QueryColData
                        {
                            ColumnId = col.Id,
                            Value = Request.Query[col.Id]
                        });
                    }
                }
                if (!query.Conditions.Any())
                {
                    return PartialView("Error");
                }
                var dataResponse = await ddataClient.GetAsync(query);
                if (dataResponse.Columns == null || !dataResponse.Columns.Any())
                    return PartialView("Error");

                model.Data = toRowData(dataResponse);
                await loadRefTables(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Detail:: Get structure of Table '{fid}' failed!");
                model.AlertError("Get data structure failed! " + fid);
                return PartialView("Error");
            }
            return PartialView(model);
        }

        public async Task<IActionResult> AddEdit(string fid, string msg = "")
        {
            var model = new DFormViewModel { TableId = fid };

            try
            {
                initChanel();
                var tableResponse = await tableClient.GetAsync(new IdRequest { Id = fid }); //table ID
                //check Table's accessable permission
                if (tableResponse.Table == null
                    || !tableResponse.Table.Enabled
                    || !tableResponse.Columns.Any())
                    return RedirectToAction("AccessDenied", "Account");

                model = mapper.Map<DFormViewModel>(tableResponse);
                if (model.Table == null || model.Columns == null || !model.Columns.Any())
                    return RedirectToAction("Error", "Home");
                await loadRefTables(model);

                // check and get query params if exists (for edit record)
                var query = new QueryRequest { TableId = fid };
                foreach (var col in model.Columns)
                {
                    if (!string.IsNullOrEmpty(Request.Query[col.Id]))
                    {
                        query.Conditions.Add(new QueryColData
                        {
                            ColumnId = col.Id,
                            Value = Request.Query[col.Id]
                        });
                    }
                }
                if (query.Conditions.Any())
                {
                    //check Edit permission
                    if (((int)EnumTablePermission.Edit & tableResponse.Table.Permissions) == 0)
                        return RedirectToAction("AccessDenied", "Account");

                    var dataResponse = await ddataClient.GetAsync(query);
                    if (dataResponse.Columns == null || !dataResponse.Columns.Any())
                        return RedirectToAction("Error", "Home");

                    model.Data = toRowData(dataResponse);
                    model.ActionMode = FormActionMode.Edit;
                }
                else
                {
                    //check Add permission
                    if (((int)EnumTablePermission.Add & tableResponse.Table.Permissions) == 0)
                        return RedirectToAction("AccessDenied", "Account");
                    model.ActionMode = FormActionMode.Add;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"AddEdit:: Get structure of Table '{fid}' failed!");
                model.AlertError("Get data structure failed! " + fid);
                return RedirectToAction("Index", new { msg = model.SaveMessageCache() });
            }
            if (!string.IsNullOrWhiteSpace(msg))
                model.LoadMessageCache(msg);
            model.ToJson();
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEdit(DFormViewModel model, IFormCollection form)
        {
            if (ModelState.IsValid && model != null)
            {
                try
                {
                    initChanel();
                    if (FormActionMode.Edit.Equals(model.ActionMode)
                        && "delete".Equals(model.CommandName))
                    {
                        model.ActionMode = FormActionMode.Delete;
                    }
                    await saveData(model, form);

                    if (model.AlertMessages.All(e => e.IsSuccess())
                        && MemCacheManager.App.ExistFormat(MemCacheConfigs.DFormRefDataKey, model.TableId))
                    {
                        MemCacheManager.App.Remove(string.Format(MemCacheConfigs.DFormRefDataKey, model.TableId));
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Update data to Table '{model.TableId}' failed!");
                    model.AlertError($"{model.ActionMode} record failed!");
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
            return RedirectToAction("Index", new { fid = model.TableId, msg = msgCode });
        }


        [HttpDelete]
        public async Task<JsonResult> Delete(string fid)
        {
            AlertMessage respMsg = null;
            try
            {
                var request = new QueryRequest { TableId = fid };
                foreach (var rkey in Request.Query.Keys)
                {
                    if (rkey == "fid") continue;
                    request.Conditions.Add(new QueryColData
                    {
                        ColumnId = rkey,
                        Value = Request.Query[rkey]
                    });
                }
                initChanel();
                var response = await ddataClient.DeleteAsync(request);
                respMsg = mapper.Map<AlertMessage>(response);

                if (respMsg.IsSuccess() && MemCacheManager.App.ExistFormat(MemCacheConfigs.DFormRefDataKey, fid))
                {
                    MemCacheManager.App.Remove(string.Format(MemCacheConfigs.DFormRefDataKey, fid));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Delete record of Table '{fid}' failed!. ERROR: {ex.Message}");
                respMsg = new AlertMessage
                {
                    StatusValue = EnumMessageStatus.Danger,
                    Message = "Delete record failed!",
                    StatusCode = "500"
                };
            }
            return Json(respMsg);
        }

        private void initChanel()
        {
            channel = new GrpcChannelHepper().CreateDDataChanel(AccessToken);

            tableClient = new SysTable.SysTableClient(channel);
            ddataClient = new DData.DDataClient(channel);
        }
        private async Task saveData(DFormViewModel model, IFormCollection form)
        {
            model.FromJson();
            ResponseMessage response = null;

            if (FormActionMode.Delete.Equals(model.ActionMode))
            {
                model.ActionMode = FormActionMode.Delete;
                var request = new QueryRequest { TableId = model.TableId };

                var colPks = model.GetPkColumns();
                if (colPks.Any())
                {
                    foreach (var pk in colPks)
                    {
                        request.Conditions.Add(new QueryColData
                        {
                            ColumnId = pk.Id,
                            Value = form[pk.Id]
                        });
                    }
                    response = await ddataClient.DeleteAsync(request);
                }
                else
                {
                    model.AlertWarning($"Table {model.TableId} not have any Key columns!");
                }
            }
            else
            {
                var request = new QueryRowData { TableId = model.TableId };
                foreach (var col in model.Columns)
                {
                    request.Columns.Add(new QueryColData
                    {
                        ColumnId = col.Id,
                        Value = form[col.Id]
                    });
                }
                if (FormActionMode.Edit.Equals(model.ActionMode))
                    response = await ddataClient.EditAsync(request);
                else
                    response = await ddataClient.AddAsync(request);
            }

            if (response != null)
            {
                var alertmsg = mapper.Map<AlertMessage>(response);
                model.AlertMessages.Add(alertmsg);
            }
        }
        private List<Dictionary<string, string>> toDataTable(QueryTableData data)
        {
            var lstRows = new List<Dictionary<string, string>>();

            foreach (var row in data.Rows)
            {
                var dictRow = toRowData(row);
                lstRows.Add(dictRow);
            }
            return lstRows;
        }
        private Dictionary<string, string> toRowData(QueryRowData row)
        {
            var dictRow = new Dictionary<string, string>();
            foreach (var col in row.Columns)
            {
                dictRow.Add(col.ColumnId, col.Value);
            }
            return dictRow;
        }
        private async Task loadRefTables(IDFormViewModel model)
        {
            // check and get Ref data if exists
            var refCols = model.Columns.Where(c =>
                c.DisplayType != (int)EnumFieldDisplayType.DropdownAjax
                && !string.IsNullOrEmpty(c.ReferenceTableId)
                && !string.IsNullOrEmpty(c.ReferenceColumnId));
            if (refCols.Any())
            {
                model.RefTables = new Dictionary<string, Select2Datasource>();
                foreach (var refcol in refCols)
                {
                    if (!model.RefTables.ContainsKey(refcol.ReferenceTableId))
                    {
                        var refdata = await searcRefTableData(refcol, allowCached: true);
                        if (refdata.Results.Any())
                            model.RefTables.Add(refcol.ReferenceTableId, refdata);
                        else
                            model.RefTables.Add(refcol.ReferenceTableId, null);
                    }
                }
            }
        }
        private async Task<Select2Datasource> searcRefTableData(SysColumnStruct refCol, string keyword = "", bool allowCached = false)
        {
            var result = new Select2Datasource();
            try
            {
                if (allowCached && MemCacheManager.App.ExistFormat(MemCacheConfigs.DFormRefDataKey, refCol.ReferenceTableId))
                {
                    result = MemCacheManager.App.GetFormat<Select2Datasource>(MemCacheConfigs.DFormRefDataKey, refCol.ReferenceTableId);
                }
                else
                {
                    var filters = new QueryFilter { TableId = refCol.ReferenceTableId, ActivedOnly = true };
                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        filters.Conditions.Add(new QueryColData
                        {
                            ColumnId = "_keyword",
                            Value = keyword
                        });
                        filters.Conditions.Add(new QueryColData
                        {
                            ColumnId = refCol.ReferenceColumnId,
                            Value = ""
                        });
                        if (!string.IsNullOrEmpty(refCol.ReferenceText1Id))
                        {
                            filters.Conditions.Add(new QueryColData
                            {
                                ColumnId = refCol.ReferenceText1Id,
                                Value = ""
                            });
                        }
                        if (!string.IsNullOrEmpty(refCol.ReferenceText2Id))
                        {
                            filters.Conditions.Add(new QueryColData
                            {
                                ColumnId = refCol.ReferenceText1Id,
                                Value = ""
                            });
                        }
                    }

                    var response = await ddataClient.SearchAsync(filters);
                    foreach (var row in response.Rows)
                    {
                        var refColId = row.Columns.SingleOrDefault(c => c.ColumnId == refCol.ReferenceColumnId);
                        if (refColId == null)
                        {
                            logger.LogError("********* RESCAN DB Required!!! *********");
                            logger.LogError($"Get ref data of Table '{refCol.TableId}' ERROR! ColumnID '{refCol.ReferenceTableId}{refCol.ReferenceColumnId}' does not exists");
                            return result;
                        }

                        var refTextValue = refColId.Value;
                        if (!string.IsNullOrEmpty(refCol.ReferenceText1Id))
                        {
                            var refText1Id = row.Columns.SingleOrDefault(c => c.ColumnId == refCol.ReferenceText1Id);
                            if (refText1Id == null)
                            {
                                logger.LogError("********* RESCAN DB Required!!! *********");
                                logger.LogError($"Get ref data of Table '{refCol.TableId}' ERROR! ColumnID '{refCol.ReferenceTableId}{refCol.ReferenceText1Id}' does not exists");
                                return result;
                            }
                            refTextValue = refText1Id.Value;

                            if (!string.IsNullOrEmpty(refCol.ReferenceText2Id))
                            {
                                var refText2Id = row.Columns.SingleOrDefault(c => c.ColumnId == refCol.ReferenceText2Id);
                                if (refText2Id == null)
                                {
                                    logger.LogError("********* RESCAN DB Required!!! *********");
                                    logger.LogError($"Get ref data of Table '{refCol.TableId}' ERROR! ColumnID '{refCol.ReferenceTableId}{refCol.ReferenceText2Id}' does not exists");
                                    return result;
                                }
                                refTextValue += $" ({refText2Id.Value})";
                            }
                        }
                        result.Results.Add(new Select2DatasourceItem(refColId.Value, refTextValue));
                    }
                    if (allowCached && response.Cacheable)
                    {
                        MemCacheManager.App.SetFormat(result, MemCacheConfigs.DFormRefDataKey, refCol.ReferenceTableId);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get ref data of Table '{refCol.TableId}' failed!");
            }
            return result;
        }

    }
}
