using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Entities.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage ="user name is requied")]
        [MaxLength(ErrorMessage ="error max length")]
        public string userName { get; set; }
        [Required(ErrorMessage = "pass word is requied")]
        [DataType(DataType.Password)]        
        public string  password { get; set; }
        [Required(ErrorMessage = "email is requied")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="the email is invalid")]
        public string email { get; set; }
        [Required(ErrorMessage = "password is requied")]
        [DataType(DataType.Password)]
        public string repassword { get; set; }
        public string fullName { get; set; }
        public string givenName { get; set; }
        public string familyName { get; set; }
        public string address { get; set; }
        public string Message { get; set; }
    }
}
