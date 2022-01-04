using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Data;
using WebMvc.Models.Categories;

namespace WebMvc.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(int? id)
        {
            CategoryModel mode = new CategoryModel();
            if(id==null)
            {
                return NotFound();
            }
            else
            {
                var response = _context.Categories.FirstOrDefault(m => m.Id ==id);
                if(response == null)
                {
                    return NotFound();
                }
            }

            return View(mode.Category);
        }

    }
}
