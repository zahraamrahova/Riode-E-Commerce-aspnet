using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductColorsController : Controller
    {
        private readonly RiodeDbContext _db;

        public ProductColorsController(RiodeDbContext db)
        {
            _db = db;
        }

        // GET: Admin/ProductColors
        public async Task<IActionResult> Index()
        {
              return _db.ProductColors != null ? 
                          View(await _db.ProductColors.ToListAsync()) :
                          Problem("Entity set 'RiodeDbContext.ProductColors'  is null.");
        }

        // GET: Admin/ProductColors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.ProductColors == null)
            {
                return NotFound();
            }

            var productColor = await _db.ProductColors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productColor == null)
            {
                return NotFound();
            }

            return View(productColor);
        }

        // GET: Admin/ProductColors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HexCode,Name,Description,Id,CreatedByUserId,CreatedDate,DeletedByUserId,DeletedDate")] ProductColor productColor)
        {
            if (ModelState.IsValid)
            {
                _db.Add(productColor);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productColor);
        }

        // GET: Admin/ProductColors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.ProductColors == null)
            {
                return NotFound();
            }

            var productColor = await _db.ProductColors.FindAsync(id);
            if (productColor == null)
            {
                return NotFound();
            }
            return View(productColor);
        }

        // POST: Admin/ProductColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HexCode,Name,Description,Id,CreatedByUserId,CreatedDate,DeletedByUserId,DeletedDate")] ProductColor productColor)
        {
            if (id != productColor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(productColor);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductColorExists(productColor.Id))
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
            return View(productColor);
        }

        // GET: Admin/ProductColors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.ProductColors == null)
            {
                return NotFound();
            }

            var productColor = await _db.ProductColors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productColor == null)
            {
                return NotFound();
            }

            return View(productColor);
        }

        // POST: Admin/ProductColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.ProductColors == null)
            {
                return Problem("Entity set 'RiodeDbContext.ProductColors'  is null.");
            }
            var productColor = await _db.ProductColors.FindAsync(id);
            if (productColor != null)
            {
                _db.ProductColors.Remove(productColor);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductColorExists(int id)
        {
          return (_db.ProductColors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
