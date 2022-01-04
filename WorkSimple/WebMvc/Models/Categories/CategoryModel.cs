using Core.Entity.SelfLearning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models.Categories
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            Category = new ECategory();
        }
        public ECategory Category{ get; set; }
    }
}
