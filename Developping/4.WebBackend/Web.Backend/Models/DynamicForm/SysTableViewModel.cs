using DynamicData.Entities;
using Mic.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mic.Core.Website;

namespace Web.Backend.Models.DynamicForm
{
    public class SysTableViewModel : BaseModel
    {
        public SysTableViewModel()
        {
            SysTable = new ESysTable();
            ActionMode = FormActionMode.Edit;
        }

        public ESysTable SysTable { get; set; }
        public IList<int> TablePermissions { get; set; }
    }

    public class SysTableListViewModel : BaseModel
    {
        public SysTableListViewModel()
        {
            ViewEnabled = true;
        }

        [Display(Name = "Table Name")]
        public string Name { get; set; }

        [Display(Name = "Enabled Only")]
        public bool ViewEnabled { get; set; }
        public KTPagination Pagination { get; set; }
        public KTSort Sort { get; set; }
    }
}
