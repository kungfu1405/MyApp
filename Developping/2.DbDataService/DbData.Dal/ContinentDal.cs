using Mic.Core.Dal;
using DbData.Entities;
using DbData.Dal.Interfaces;

namespace DbData.Dal
{
    public class ContinentDal : DalBase<IDbDataContext, EContinent>, IContinent
    {
        public ContinentDal(IDbDataContext context) : base(context)
        {

        }
    }
}
