using DbData.Entities;
using Mic.Core.Website;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Backend.Models
{
    public class AttractionLanguageViewModel : BaseModel
    {
        public EAttractionLanguage AttractionLanguage { get; set; }
        public string ReturnUrl { get; set; }
    }
}
