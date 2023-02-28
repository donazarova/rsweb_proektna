using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCTurizam.Data;
using MVCTurizam.Models;

namespace MVCTurizam.Controllers
{
    public class PatuvanjesController : Controller
    {
        private readonly MVCTurizamContext _context;

        public PatuvanjesController(MVCTurizamContext context)
        {
            _context = context;
        }

        // GET: Patuvanjes
        public async Task<IActionResult> Index()
        {
            var mVCTurizamContext = _context.Patuvanje.Include(p => p.Destinacija).Include(p => p.Klient);
            return View(await mVCTurizamContext.ToListAsync());
        }

        // GET: Patuvanjes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patuvanje == null)
            {
                return NotFound();
            }

            var patuvanje = await _context.Patuvanje
                .Include(p => p.Destinacija)
                .Include(p => p.Klient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patuvanje == null)
            {
                return NotFound();
            }

            return View(patuvanje);
        }

        // GET: Patuvanjes/Create
        public IActionResult Create()
        {
            ViewData["DestinacijaId"] = new SelectList(_context.Destinacija, "Id", "Id");
            ViewData["KlientId"] = new SelectList(_context.Klient, "Id", "ImePrezime");
            return View();
        }

        // POST: Patuvanjes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KlientId,DestinacijaId,DatumOd,DatumDo")] Patuvanje patuvanje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patuvanje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DestinacijaId"] = new SelectList(_context.Destinacija, "Id", "Id", patuvanje.DestinacijaId);
            ViewData["KlientId"] = new SelectList(_context.Klient, "Id", "ImePrezime", patuvanje.KlientId);
            return View(patuvanje);
        }

        // GET: Patuvanjes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patuvanje == null)
            {
                return NotFound();
            }

            var patuvanje = await _context.Patuvanje.FindAsync(id);
            if (patuvanje == null)
            {
                return NotFound();
            }
            ViewData["DestinacijaId"] = new SelectList(_context.Destinacija, "Id", "Id", patuvanje.DestinacijaId);
            ViewData["KlientId"] = new SelectList(_context.Klient, "Id", "ImePrezime", patuvanje.KlientId);
            return View(patuvanje);
        }

        // POST: Patuvanjes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KlientId,DestinacijaId,DatumOd,DatumDo")] Patuvanje patuvanje)
        {
            if (id != patuvanje.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patuvanje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatuvanjeExists(patuvanje.Id))
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
            ViewData["DestinacijaId"] = new SelectList(_context.Destinacija, "Id", "Id", patuvanje.DestinacijaId);
            ViewData["KlientId"] = new SelectList(_context.Klient, "Id", "ImePrezime", patuvanje.KlientId);
            return View(patuvanje);
        }

        // GET: Patuvanjes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patuvanje == null)
            {
                return NotFound();
            }

            var patuvanje = await _context.Patuvanje
                .Include(p => p.Destinacija)
                .Include(p => p.Klient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patuvanje == null)
            {
                return NotFound();
            }

            return View(patuvanje);
        }

        // POST: Patuvanjes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patuvanje == null)
            {
                return Problem("Entity set 'MVCTurizamContext.Patuvanje'  is null.");
            }
            var patuvanje = await _context.Patuvanje.FindAsync(id);
            if (patuvanje != null)
            {
                _context.Patuvanje.Remove(patuvanje);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatuvanjeExists(int id)
        {
          return _context.Patuvanje.Any(e => e.Id == id);
        }
    }
}
