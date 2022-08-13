using DbData.Dal;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Entities;
using Mic.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Bll
{
    public class VehicleBll : BllDbDataBase
    {
        public VehicleBll(IDbDataContext context) : base(context)
        {
        }

        public async Task<EVehicle> Add(EVehicle entry)
        {
            if (string.IsNullOrEmpty(entry.Name))
                throw new InvalidInputException("Invalid Input");

            return await VehicleDao.Add(entry);
        }

        public async Task Edit(EVehicle entry, bool hasUpdatePassword = false)
        {
            if (string.IsNullOrEmpty(entry.Id.ToString()))
                throw new InvalidInputException("Invalid ID");

            if (string.IsNullOrEmpty(entry.Name))
                throw new InvalidInputException("Invalid Input");

            await VehicleDao.Edit(entry);
        }

        public async Task Delete(Guid id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
                throw new InvalidInputException("Invalid ID");

            await VehicleDao.Delete(id);
        }

        public async Task<EVehicle> Get(Guid id)
        {
            return string.IsNullOrEmpty(id.ToString()) ? null : await VehicleDao.Get(id);
        }

        public async Task<PagingResult<EVehicle>> GetList(VehicleFilter filter = null)
        {
            return await VehicleDao.GetList(filter);
        }
    }
}
