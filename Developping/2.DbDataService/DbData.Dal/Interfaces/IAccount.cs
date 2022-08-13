using Mic.Core.Dal;
using Mic.UserDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Dal.Interfaces
{
    public interface IAccount : IBaseRepository<EUser>
    {
        EUser GetAccount(string username, bool accStatus = false);
    }
}
