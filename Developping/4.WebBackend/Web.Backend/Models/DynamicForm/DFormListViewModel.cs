using Mic.Core.Entities;
using System.Collections.Generic;

namespace Web.Backend.Models.DynamicForm
{
    public class DFormListViewModel : DFormModel, IDFormViewModel
    {
        public DFormListViewModel() : base()
        {
            Conditions = new Dictionary<string, string>();
        }
        public string ExportType { get; set; }
        public Dictionary<string, string> Conditions { get; set; }
        public Dictionary<string, string> Sort { get; set; }
        public KTPagination Pagination { get; set; }
    }
}
