using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hastanerandevusistemi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using hastanerandevusistemi.Migrations.ConnectionStringClassMigrations;

namespace hastanerandevusistemi.Controllers
{
    public class RandevuAlController : Controller
    {
        private readonly ConnectionStringClass _context;

        public RandevuAlController(ConnectionStringClass context)
        {
            _context = context;
        }
        public async Task<IActionResult> Delete2(int? id)
        {
            if (id == null || _context.randevuAls == null)
            {
                return NotFound();
            }

            var randevuAl = await _context.randevuAls
                .FirstOrDefaultAsync(m => m.randID == id);
            if (randevuAl == null)
            {
                return NotFound();
            }

            return View(randevuAl);
        }
        // POST: RandevuAl/Delete/5
        [HttpPost, ActionName("Delete2")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed2(int id)
        {
            if (_context.randevuAls == null)
            {
                return Problem("Entity set 'ConnectionStringClass.randevuAls'  is null.");
            }
            var randevuAl = await _context.randevuAls.FindAsync(id);
            if (randevuAl != null)
            {
                _context.randevuAls.Remove(randevuAl);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index1));
        }
        public async Task<IActionResult> Index1()
        {
            return _context.randevuAls != null ?
                        View(await _context.randevuAls.ToListAsync()) :
                        Problem("Entity set 'ConnectionStringClass.randevuAls'  is null.");
        }

        // GET: RandevuAl
        public async Task<IActionResult> Index()
        {
            // Kullanıcının id'sini al
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Kullanıcının id'sine ait randevuları getir
            var userRandevulars = await _context.randevuAls
                .Where(r => r.randsahip == userId)
                .ToListAsync();

            // Eğer kullanıcıya ait randevu varsa, View'a gönder
            return userRandevulars != null
                ? View(userRandevulars)
                : Problem("Kullanıcıya ait randevu bulunamadı.");
        }

        // GET: RandevuAl/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.randevuAls == null)
            {
                return NotFound();
            }

            var randevuAl = await _context.randevuAls
                .FirstOrDefaultAsync(m => m.randID == id);
            if (randevuAl == null)
            {
                return NotFound();
            }

            return View(randevuAl);
        }

        [HttpGet]
        public JsonResult GetDoktorlar(string klinik)
        {
            var doktorlar = _context.Doktorlars
                .Where(d => d.durum == "Aktif" && d.klinik == klinik)
                .Select(d => new SelectListItem
                {
                    Value = d.isim,
                    Text = d.isim
                })
                .ToList();

            return Json(doktorlar);
        }

        [Authorize]
        // GET: RandevuAl/Create
        public IActionResult Create()
        {  
            // Saatleri burada tanımladım.
            var saatler = new List<string>
            {
                "09:00", "09:15","09:30","09:45",
                "10:00", "10:15","10:30","10:45",
                "11:00", "11:15","11:30","11:45",
                "13:00", "13:15","13:30","13:45",
                "14:00", "14:15","14:30","14:45",
                "15:00", "15:15","15:30","15:45",
                "16:00", "16:15","16:30","16:45"
            };

            ViewBag.Saatler = new SelectList(saatler);

            //doktor isimlerini getiriyoruz
            var klinikler = _context.Doktorlars.Select(d => d.klinik).Distinct().ToList();
            ViewBag.Klinikler = new SelectList(klinikler);

            var randevuModel = new RandevuAl
            {
                // Giriş yapan kullanıcının id'sini otomatik olarak randsahip alanına atayalım
                randsahip = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            return View(randevuModel);
        }

        // POST: RandevuAl/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("randID,randklinik,randhekim,randtarih,randsaat,randsahip")] RandevuAl randevuAl)
        {

            // Eğer kullanıcı doğrudan URL üzerinden Create action'ına erişirse, randsahip alanını kontrol et
            if (randevuAl.randsahip != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                // Kullanıcı izni yok, giriş yapmış kullanıcıya ait bir randevu olmalı
                return Forbid();
            }

            // Aynı doktor, tarih ve saatte randevu olup olmadığını kontrol et
            bool isDuplicate = await _context.randevuAls
                .AnyAsync(r => r.randhekim == randevuAl.randhekim &&
                               r.randtarih == randevuAl.randtarih &&
                               r.randsaat == randevuAl.randsaat);

            if (isDuplicate)
            {
                // Aynı doktor, tarih ve saatte randevu bulunuyorsa kullanıcıya uyarı ver
                ModelState.AddModelError(string.Empty, "Bu doktorun belirtilen tarih ve saatte başka bir randevusu zaten var.");
                var klinikler = _context.Doktorlars.Select(d => d.klinik).Distinct().ToList();
                ViewBag.Klinikler = new SelectList(klinikler);
                var doktorlar = _context.Doktorlars.Select(f => f.isim).ToList();
                ViewBag.Doktorlar = new SelectList(doktorlar);
                return View(randevuAl);
            }

            if (ModelState.IsValid)
            {
                _context.Add(randevuAl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(randevuAl);
        }

        // GET: RandevuAl/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.randevuAls == null)
            {
                return NotFound();
            }

            var randevuAl = await _context.randevuAls.FindAsync(id);
            if (randevuAl == null)
            {
                return NotFound();
            }
            return View(randevuAl);
        }

        // POST: RandevuAl/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("randID,randklinik,randhekim,randtarih,randsaat,randsahip")] RandevuAl randevuAl)
        {
            if (id != randevuAl.randID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevuAl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuAlExists(randevuAl.randID))
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
            return View(randevuAl);
        }

        // GET: RandevuAl/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.randevuAls == null)
            {
                return NotFound();
            }

            var randevuAl = await _context.randevuAls
                .FirstOrDefaultAsync(m => m.randID == id);
            if (randevuAl == null)
            {
                return NotFound();
            }

            return View(randevuAl);
        }

        // POST: RandevuAl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.randevuAls == null)
            {
                return Problem("Entity set 'ConnectionStringClass.randevuAls'  is null.");
            }
            var randevuAl = await _context.randevuAls.FindAsync(id);
            if (randevuAl != null)
            {
                _context.randevuAls.Remove(randevuAl);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevuAlExists(int id)
        {
          return (_context.randevuAls?.Any(e => e.randID == id)).GetValueOrDefault();
        }
    }
}
