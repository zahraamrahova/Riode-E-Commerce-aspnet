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
    public class FaqsController : Controller
    {
        private readonly RiodeDbContext _db;

        public FaqsController(RiodeDbContext db)
        {
            _db = db;
        }

        // GET: Admin/Faqs
        public async Task<IActionResult> Index()
        {
              return _db.Faqs != null ? 
                          View(await _db.Faqs.ToListAsync()) :
                          Problem("Entity set 'RiodeDbContext.Faqs'  is null.");
        }

        // GET: Admin/Faqs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Faqs == null)
            {
                return NotFound();
            }

            var faq = await _db.Faqs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }

        // GET: Admin/Faqs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Faqs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Question,Answer,Id,CreatedByUserId,CreatedDate,DeletedByUserId,DeletedDate")] Faq faq)
        {
            if (ModelState.IsValid)
            {
                _db.Add(faq);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }

        // GET: Admin/Faqs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Faqs == null)
            {
                return NotFound();
            }

            var faq = await _db.Faqs.FindAsync(id);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

        // POST: Admin/Faqs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Question,Answer,Id,CreatedByUserId,CreatedDate,DeletedByUserId,DeletedDate")] Faq faq)
        {
            if (id != faq.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(faq);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaqExists(faq.Id))
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
            return View(faq);
        }

        // GET: Admin/Faqs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Faqs == null)
            {
                return NotFound();
            }

            var faq = await _db.Faqs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }

        // POST: Admin/Faqs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Faqs == null)
            {
                return Problem("Entity set 'RiodeDbContext.Faqs'  is null.");
            }
            var faq = await _db.Faqs.FindAsync(id);
            if (faq != null)
            {
                _db.Faqs.Remove(faq);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaqExists(int id)
        {
          return (_db.Faqs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
