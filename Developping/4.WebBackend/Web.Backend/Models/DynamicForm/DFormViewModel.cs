using DynamicData.Protos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Web.Backend.Models.DynamicForm
{
    public class DFormViewModel : DFormModel, IDFormViewModel
    {
        public DFormViewModel() : base()
        {
        }
        public Dictionary<string, string> Data { get; set; }

        public string ColumnsJson { get; set; }
        public void ToJson()
        {
            if (Columns == null || !Columns.Any()) return;
            ColumnsJson = JsonConvert.SerializeObject(Columns);
        }
        public void FromJson()
        {
            if (string.IsNullOrEmpty(TableId) || string.IsNullOrEmpty(ColumnsJson))
            {
                return;
            }
            Columns = JsonConvert.DeserializeObject<IList<SysColumnStruct>>(ColumnsJson);
        }
    }
}
