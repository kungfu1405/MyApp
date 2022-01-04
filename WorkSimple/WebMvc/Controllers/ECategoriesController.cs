using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entity.SelfLearning;
using WebMvc.Data;

namespace WebMvc.Controllers
{
    public class ECategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ECategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ECategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: ECategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eCategory = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eCategory == null)
            {
                return NotFound();
            }

            return View(eCategory);
        }

        // GET: ECategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ECategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] ECategory eCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eCategory);
        }

        // GET: ECategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eCategory = await _context.Categories.FindAsync(id);
            if (eCategory == null)
            {
                return NotFound();
            }
            return View(eCategory);
        }

        // POST: ECategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] ECategory eCategory)
        {
            if (id != eCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ECategoryExists(eCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(eCategory);
        }

        // GET: ECategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eCategory = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eCategory == null)
            {
                return NotFound();
            }

            return View(eCategory);
        }

        // POST: ECategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eCategory = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(eCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ECategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
