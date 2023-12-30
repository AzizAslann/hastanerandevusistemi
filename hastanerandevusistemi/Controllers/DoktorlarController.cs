using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hastanerandevusistemi.Models;

namespace hastanerandevusistemi.Controllers
{
    public class DoktorlarController : Controller
    {
        private readonly ConnectionStringClass _context;

        public DoktorlarController(ConnectionStringClass context)
        {
            _context = context;
        }

        // GET: Doktorlar
        public async Task<IActionResult> Index()
        {
              return _context.Doktorlars != null ? 
                          View(await _context.Doktorlars.ToListAsync()) :
                          Problem("Entity set 'ConnectionStringClass.Doktorlars'  is null.");
        }

        // GET: Doktorlar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Doktorlars == null)
            {
                return NotFound();
            }

            var doktorlar = await _context.Doktorlars
                .FirstOrDefaultAsync(m => m.DoktorID == id);
            if (doktorlar == null)
            {
                return NotFound();
            }

            return View(doktorlar);
        }

        // GET: Doktorlar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doktorlar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoktorID,klinik,isim,durum")] Doktorlar doktorlar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doktorlar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doktorlar);
        }

        // GET: Doktorlar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Doktorlars == null)
            {
                return NotFound();
            }

            var doktorlar = await _context.Doktorlars.FindAsync(id);
            if (doktorlar == null)
            {
                return NotFound();
            }
            return View(doktorlar);
        }

        // POST: Doktorlar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoktorID,klinik,isim,durum")] Doktorlar doktorlar)
        {
            if (id != doktorlar.DoktorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doktorlar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoktorlarExists(doktorlar.DoktorID))
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
            return View(doktorlar);
        }

        // GET: Doktorlar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Doktorlars == null)
            {
                return NotFound();
            }

            var doktorlar = await _context.Doktorlars
                .FirstOrDefaultAsync(m => m.DoktorID == id);
            if (doktorlar == null)
            {
                return NotFound();
            }

            return View(doktorlar);
        }

        // POST: Doktorlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doktorlars == null)
            {
                return Problem("Entity set 'ConnectionStringClass.Doktorlars'  is null.");
            }
            var doktorlar = await _context.Doktorlars.FindAsync(id);
            if (doktorlar != null)
            {
                _context.Doktorlars.Remove(doktorlar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoktorlarExists(int id)
        {
          return (_context.Doktorlars?.Any(e => e.DoktorID == id)).GetValueOrDefault();
        }
    }
}
