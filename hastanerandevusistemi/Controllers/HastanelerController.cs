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
    public class HastanelerController : Controller
    {
        private readonly ConnectionStringClass _context;

        public HastanelerController(ConnectionStringClass context)
        {
            _context = context;
        }

        // GET: Hastaneler
        public async Task<IActionResult> Hastaneler()
        {
              return _context.Hastaneler != null ? 
                          View(await _context.Hastaneler.ToListAsync()) :
                          Problem("Entity set 'ConnectionStringClass.Hastaneler'  is null.");
        }

        // GET: Hastaneler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hastaneler == null)
            {
                return NotFound();
            }

            var hastaneClass = await _context.Hastaneler
                .FirstOrDefaultAsync(m => m.hastid == id);
            if (hastaneClass == null)
            {
                return NotFound();
            }

            return View(hastaneClass);
        }

        // GET: Hastaneler/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hastaneler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("hastid,hastil,hastilce,hastisim")] HastaneClass hastaneClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hastaneClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Hastaneler));
            }
            return View(hastaneClass);
        }

        // GET: Hastaneler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hastaneler == null)
            {
                return NotFound();
            }

            var hastaneClass = await _context.Hastaneler.FindAsync(id);
            if (hastaneClass == null)
            {
                return NotFound();
            }
            return View(hastaneClass);
        }

        // POST: Hastaneler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("hastid,hastil,hastilce,hastisim")] HastaneClass hastaneClass)
        {
            if (id != hastaneClass.hastid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hastaneClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastaneClassExists(hastaneClass.hastid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Hastaneler));
            }
            return View(hastaneClass);
        }

        // GET: Hastaneler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hastaneler == null)
            {
                return NotFound();
            }

            var hastaneClass = await _context.Hastaneler
                .FirstOrDefaultAsync(m => m.hastid == id);
            if (hastaneClass == null)
            {
                return NotFound();
            }

            return View(hastaneClass);
        }

        // POST: Hastaneler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hastaneler == null)
            {
                return Problem("Entity set 'ConnectionStringClass.Hastaneler'  is null.");
            }
            var hastaneClass = await _context.Hastaneler.FindAsync(id);
            if (hastaneClass != null)
            {
                _context.Hastaneler.Remove(hastaneClass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Hastaneler));
        }

        private bool HastaneClassExists(int id)
        {
          return (_context.Hastaneler?.Any(e => e.hastid == id)).GetValueOrDefault();
        }
    }
}
