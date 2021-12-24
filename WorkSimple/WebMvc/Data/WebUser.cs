using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Data
{
    
    public class WebUser:IdentityUser
    {
        public string CustomTag { get; set; }
        public string DOB { get; set; }
    }
}
