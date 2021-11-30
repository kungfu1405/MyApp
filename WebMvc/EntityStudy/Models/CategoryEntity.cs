using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityStudy.Models
{
    [Table("CategoryTbl")]
    public class CategoryEntity
    {
        public int Id { get; set; }
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
