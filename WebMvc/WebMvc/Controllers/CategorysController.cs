using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMvc.Data;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class CategorysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategorysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categorys
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorys.ToListAsync());
        }

        // GET: Categorys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryEntity = await _context.Categorys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            return View(categoryEntity);
        }

        // GET: Categorys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] CategoryEntity categoryEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryEntity);
        }

        // GET: Categorys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryEntity = await _context.Categorys.FindAsync(id);
            if (categoryEntity == null)
            {
                return NotFound();
            }
            return View(categoryEntity);
        }

        // POST: Categorys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] CategoryEntity categoryEntity)
        {
            if (id != categoryEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryEntityExists(categoryEntity.Id))
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
            return View(categoryEntity);
        }

        // GET: Categorys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryEntity = await _context.Categorys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            return View(categoryEntity);
        }

        // POST: Categorys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryEntity = await _context.Categorys.FindAsync(id);
            _context.Categorys.Remove(categoryEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryEntityExists(int id)
        {
            return _context.Categorys.Any(e => e.Id == id);
        }
    }
}
