using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    public class CustomerEntity:IdentityUser
    {
        //public int UserId { get; set; }
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Address { get; set; }
        public ICollection<OrderEnity> Orders { get; set; }

    }
}
