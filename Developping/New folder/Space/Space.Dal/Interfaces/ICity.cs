using Mic.Core.Entities;
using Space.Dal.Entities;
using Space.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space.Dal.Interfaces
{
    public interface ICity
    {
        Task<PagingResult<ECity>> GetList(CityFilters filter = null);
    }
}
