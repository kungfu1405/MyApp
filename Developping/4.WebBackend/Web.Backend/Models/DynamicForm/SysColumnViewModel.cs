using DynamicData.Entities;
using Mic.Core.Entities;
using System.Collections.Generic;

namespace Web.Backend.Models.DynamicForm
{
    public class SysColumnViewModel : DFormModel, IDFormViewModel
    {
        public SysColumnViewModel()
        {
            SysColumn = new ESysColumn();
            ActionMode = FormActionMode.Edit;
        }
        public ESysColumn SysColumn { get; set; }
        public IList<int> FieldOptions { get; set; }
        public IList<ESysCustomType> AllCustomTypes { get; set; }
        public IList<ESysTable> AllTables { get; set; }
        public IList<ESysColumn> AllColumns { get; set; }
    }
}
