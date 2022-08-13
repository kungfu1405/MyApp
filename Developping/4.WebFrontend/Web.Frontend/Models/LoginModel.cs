using Mic.UserDb.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Frontend.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User name is required , please enter")]
        //[DataType(DataType.EmailAddress)]
        //[EmailAddress(ErrorMessage = "Hmm...that doesn't look like an email address")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required , please enter")]

        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public bool IsPersistent { get; set; }

        public string SelectedLanguage { get; set; }

        //public MvcCaptcha LoginCaptcha { get; set; }

        public bool Locked { get; set; }

        [Display(Name = "Change language")]
        public List<ELanguage> Languages { get; set; }
    }
}
