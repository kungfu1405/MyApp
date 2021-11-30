using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebMvc.Models
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        [Display(Name ="Category Name")]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
