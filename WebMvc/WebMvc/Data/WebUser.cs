using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Data
{
    public class WebUser: IdentityUser
    {
        public int Id { get; set; }
        public string Address { get; set; }
    }

}
