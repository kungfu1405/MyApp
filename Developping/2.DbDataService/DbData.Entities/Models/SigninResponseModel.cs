using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Entities.Models
{
    public class SigninResponseModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public IList<string> Roles { get; set; }
        public int Timeout { get; set; }

    }
  
}
