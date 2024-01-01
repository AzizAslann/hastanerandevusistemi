using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hastanerandevusistemi.Models;
using Microsoft.AspNetCore.Authorization;
using hastanerandevusistemi.Models.Domain;
using hastanerandevusistemi.Models.DTO;
using System.Security.Claims;

namespace hastanerandevusistemi.Controllers
{
    public class RandevularController : Controller
    {
        private readonly ConnectionStringClass _context;

        public RandevularController(ConnectionStringClass context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index2()
        {
            return _context.Randevulars != null ?
                        View(await _context.Randevulars.ToListAsync()) :
                        Problem("Entity set 'ConnectionStringClass.Randevulars'  is null.");
        }

        // GET: Randevular
        public async Task<IActionResult> Index()
        {
            // Kullanıcının id'sini al
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Kullanıcının id'sine ait randevuları getir
            var userRandevulars = await _context.Randevulars
                .Where(r => r.randsahip == userId)
                .ToListAsync();

            // Eğer kullanıcıya ait randevu varsa, View'a gönder
            return userRandevulars != null
                ? View(userRandevulars)
                : Problem("Kullanıcıya ait randevu bulunamadı.");
        }

        // GET: Randevular/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Randevulars == null)
            {
                return NotFound();
            }

            var randevular = await _context.Randevulars
                .FirstOrDefaultAsync(m => m.randID == id);
            if (randevular == null)
            {
                return NotFound();
            }

            return View(randevular);
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

        // GET: Randevular/Create
        [Authorize]
        public IActionResult Create()
        {
            var klinikler = _context.Doktorlars.Select(d=>d.klinik).Distinct().ToList();
            ViewBag.Klinikler = new SelectList(klinikler);

            //var aktifdoktorlar =_context.Doktorlars.Where(p=>p.durum=="Aktif").ToList();

            //var dropdownData = aktifdoktorlar.Select(doktor => new SelectListItem
            //{
            //    Value = doktor.DoktorID.ToString(),
            //    Text=doktor.isim
            //})
            //    .ToList();
            //ViewBag.DoktorlarDropdown = dropdownData;


            var randevuModel = new Randevular
            {
                // Giriş yapan kullanıcının id'sini otomatik olarak randsahip alanına atayalım
                randsahip = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            return View(randevuModel);
        }

        // POST: Randevular/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("randID,randklinik,randhekim,randtarih,randsahip")] Randevular randevular)
        {
            // Eğer kullanıcı doğrudan URL üzerinden Create action'ına erişirse, randsahip alanını kontrol et
            if (randevular.randsahip != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                // Kullanıcı izni yok, giriş yapmış kullanıcıya ait bir randevu olmalı
                return Forbid();
            }

            // Aynı doktor, tarih ve saatte randevu olup olmadığını kontrol et
            bool isDuplicate = await _context.Randevulars
                .AnyAsync(r => r.randhekim == randevular.randhekim &&
                               r.randtarih == randevular.randtarih);

            if (isDuplicate)
            {
                // Aynı doktor, tarih ve saatte randevu bulunuyorsa kullanıcıya uyarı ver
                ModelState.AddModelError(string.Empty, "Bu doktorun belirtilen tarih ve saatte başka bir randevusu zaten var.");
                var klinikler = _context.Doktorlars.Select(d => d.klinik).Distinct().ToList();
                ViewBag.Klinikler = new SelectList(klinikler);
                var doktorlar = _context.Doktorlars.Select(f => f.isim).ToList();
                ViewBag.Doktorlar = new SelectList(doktorlar);
                return View(randevular);
            }

            if (ModelState.IsValid)
            {
                _context.Add(randevular);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(randevular);
        }

        // GET: Randevular/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Randevulars == null)
            {
                return NotFound();
            }

            var randevular = await _context.Randevulars.FindAsync(id);
            if (randevular == null)
            {
                return NotFound();
            }
            return View(randevular);
        }

        // POST: Randevular/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("randID,randklinik,randhekim,randtarih,randsahip")] Randevular randevular)
        {
            if (id != randevular.randID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevular);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevularExists(randevular.randID))
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
            return View(randevular);
        }

        // GET: Randevular/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Randevulars == null)
            {
                return NotFound();
            }

            var randevular = await _context.Randevulars
                .FirstOrDefaultAsync(m => m.randID == id);
            if (randevular == null)
            {
                return NotFound();
            }

            return View(randevular);
        }

        // POST: Randevular/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Randevulars == null)
            {
                return Problem("Entity set 'ConnectionStringClass.Randevulars'  is null.");
            }
            var randevular = await _context.Randevulars.FindAsync(id);
            if (randevular != null)
            {
                _context.Randevulars.Remove(randevular);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevularExists(int id)
        {
          return (_context.Randevulars?.Any(e => e.randID == id)).GetValueOrDefault();
        }
    }
}
