using Basic.Core.Dal;
using DbData.Entitties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Dal.Interfaces
{
    public interface IDbDataContext: IEfContext
    {
        DbSet<ECity> Cities { get; set; }
    }
}
