using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication1.Data.WebApplicationContext _context;

        public IndexModel(WebApplication1.Data.WebApplicationContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        //public async Task OnGetAsync()
        //{
        //    Movie = await _context.Movie.ToListAsync();
        //}
        public IList<Movie> OnGetAsync()
        {
            return Movie = _context.Movie.ToList();
        }
    }
}
