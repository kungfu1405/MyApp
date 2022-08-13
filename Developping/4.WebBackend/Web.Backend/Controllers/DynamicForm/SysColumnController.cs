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
using System.Collections.Generic;
using Mic.Core.Entities;
using Mic.Core.DataTypes;
using Mic.Core.Website;

namespace Web.Backend.Controllers.DynamicForm
{
    public class SysColumnController : BaseController<SysColumnController>
    {
        private GrpcChannel channel;
        private SysTable.SysTableClient tableClient;
        private SysColumn.SysColumnClient columnClient;
        private SysCustomType.SysCustomTypeClient customTypeClient;

        public SysColumnController(IMapper mapper, ILogger<SysColumnController> logger) : base(logger, mapper)
        {
        }

        [HttpPost]
        public async Task<JsonResult> GetDatatable(string tableId)
        {
            try
            {
                initChanel();
                var response = await columnClient.GetListAsync(new IdRequest { Id = tableId });

                var lstColumns = mapper.Map<List<ESysColumn>>(response.Columns);

                var result = new
                {
                    data = lstColumns.Select(e=>new
                    {
                        e.Id,
                        e.Ordinal,
                        e.Name,
                        e.DisplayName,
                        e.IsNulable,
                        DataType = e.DataType.ToString(),
                        DisplayType = e.DisplayType.ToString()
                    })
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Get Columns List failed!. ERROR: {ex.Message}");
            }
            var emptyResult = new
            {
                data = new List<ESysColumn>()
            };
            return Json(emptyResult);
        }

        public async Task<PartialViewResult> AddEdit(string id, string msg = "")
        {
            var model = new SysColumnViewModel();
            try
            {
                initChanel();
                var response = await columnClient.GetAsync(new IdRequest { Id = id }); //table ID
                if (string.IsNullOrEmpty(response.Id))
                {
                    logger.LogError($"Column '{id}' not found!");
                    model.AlertError($"Column '{id}' not found!");
                }
                else
                {
                    model.SysColumn = mapper.Map<ESysColumn>(response);
                    model.ActionMode = FormActionMode.Edit;

                    var customTypes = await customTypeClient.GetAllAsync(new EmptyRequest());
                    model.AllCustomTypes = mapper.Map<IList<ESysCustomType>>(customTypes.CustomTypes);

                    var allTables = await tableClient.GetEnabledListAsync(new EmptyRequest());
                    model.AllTables = mapper.Map<IList<ESysTable>>(allTables.Tables);

                    var allColumns = await columnClient.GetEnabledListAsync(new EmptyRequest());
                    model.AllColumns = mapper.Map<IList<ESysColumn>>(allColumns.Columns);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Edit:: Get Column '{id}' failed!");
                model.AlertError("Data not found!");
            }

            if (!string.IsNullOrWhiteSpace(msg))
                model.LoadMessageCache(msg);
            return PartialView(model);
        }

        [HttpPost]
        public async Task<JsonResult> AddEdit(SysColumnViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                try
                {
                    if (model.FieldOptions != null)
                    {
                        model.SysColumn.FieldOptions = model.FieldOptions.Sum(p => dNum.ToInt(p));
                    }
                    else
                    {
                        model.SysColumn.FieldOptions = 0;
                    }

                    initChanel();
                    var request = mapper.Map<SysColumnStruct>(model.SysColumn);
                    var response = await columnClient.EditAsync(request);

                    if (response != null)
                    {
                        var alertmsg = mapper.Map<AlertMessage>(response);
                        model.AlertMessages.Add(alertmsg);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Update Column '{model.SysColumn.Name}' failed!");
                    model.AlertError($"Edit Column '{model.SysColumn.Name}' failed!");
                }
            }
            return Json(model.AlertMessages);
        }

        private void initChanel()
        {
            channel = new GrpcChannelHepper().CreateDDataChanel(AccessToken);
            tableClient = new SysTable.SysTableClient(channel);
            columnClient = new SysColumn.SysColumnClient(channel);
            customTypeClient = new SysCustomType.SysCustomTypeClient(channel);
        }
    }
}
