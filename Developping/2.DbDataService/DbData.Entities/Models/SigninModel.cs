using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mic.UserDb.Entities;
namespace DbData.Entities.Models
{
    public class SigninModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
       
        public List<ELanguage> Languages { get; set; }
    }
}
 