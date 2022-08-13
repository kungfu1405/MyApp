using Mic.Core.Entities;
using Mic.UserDb.Entities;
using Mic.Core.Website;

namespace Web.Backend.Models.User
{
    public class LanguageViewModel : BaseModel
    {
        public LanguageViewModel()
        {
            ActionMode = FormActionMode.Add;
            Language = new ELanguage();
        }
        public ELanguage Language { get; set; }
    }
}
