using DbData.Dal.Interfaces;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Dal;
using Mic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Dal
{
    public class CommentImageDal : DalBase<IDbDataContext, ECommentImage>, ICommentImage
    {
        public CommentImageDal(IDbDataContext context) : base(context)
        {
        }
    }
}
