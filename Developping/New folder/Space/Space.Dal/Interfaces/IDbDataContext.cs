using Mic.Core.Dal;
using Microsoft.EntityFrameworkCore;
using Space.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space.Dal.Interfaces
{
    public interface IDbDataContext : IEfContext
    {
        DbSet<ECity> Cities { get; set; }
    }
}
