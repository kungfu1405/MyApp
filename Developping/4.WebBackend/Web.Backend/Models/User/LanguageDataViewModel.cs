using Mic.Core.Entities;
using Mic.UserDb.Entities;
using Mic.Core.Website;
using System.Collections.Generic;

namespace Web.Backend.Models.User
{
    public class LanguageDataViewModel : BaseModel
    {
        public LanguageDataViewModel()
        {
            ActionMode = FormActionMode.Add;
            LanguageData = new ELanguageData();
        }
        public ELanguageData LanguageData { get; set; }
    }

    public class LanguageDataListViewModel : BaseModel
    {
        public LanguageDataListViewModel()
        {
            IsGroupOnly = false;
        }

        public string LangKey { get; set; }
        public string LangCode { get; set; }
        public bool IsGroupOnly { get; set; }

        public IList<string> KeyGroups { get; set; }

        public KTPagination Pagination { get; set; }
        public KTSort Sort { get; set; }
    }
}