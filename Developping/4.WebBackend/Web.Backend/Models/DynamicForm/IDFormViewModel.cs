using DynamicData.Protos;
using Mic.Core.Entities;
using System.Collections.Generic;

namespace Web.Backend.Models.DynamicForm
{
    public interface IDFormViewModel
    {
        string TableId { get; set; }
        SysTableStruct Table { get; set; }
        IList<SysColumnStruct> Columns { get; set; }
        IDictionary<string, Select2Datasource> RefTables { get; set; }
        IList<SysCustomTypeStruct> CustomTypes { get; set; }
    }
}
