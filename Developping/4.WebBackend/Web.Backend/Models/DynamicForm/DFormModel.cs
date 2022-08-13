using DynamicData.Entities;
using DynamicData.Protos;
using Mic.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using Mic.Core.Website;

namespace Web.Backend.Models.DynamicForm
{
    public class DFormModel : BaseModel, IDFormViewModel
    {
        public DFormModel() : base()
        {
        }

        public string TableId { get; set; }
        public SysTableStruct Table { get; set; }
        public IList<SysColumnStruct> Columns { get; set; }
        public IDictionary<string, Select2Datasource> RefTables { get; set; }
        public IList<SysCustomTypeStruct> CustomTypes { get; set; }

        public List<SysColumnStruct> GetPkColumns()
        {
            var listPkColumns = Columns.Where(c => (int)EnumFieldType.PrimaryKey == c.FieldType).ToList();
            if (!listPkColumns.Any())
            {
                listPkColumns = Columns.Where(c => ((int)EnumFieldOption.UniqueValue & c.FieldOptions) > 0).ToList();
            }
            return listPkColumns;
        }

    }
}
