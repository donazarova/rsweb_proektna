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
    public class VodicsController : Controller
    {
        private readonly MVCTurizamContext _context;

        public VodicsController(MVCTurizamContext context)
        {
            _context = context;
        }

        // GET: Vodics
        public async Task<IActionResult> Index()
        {
              return View(await _context.Vodic.ToListAsync());
        }

        // GET: Vodics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vodic == null)
            {
                return NotFound();
            }

            var vodic = await _context.Vodic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vodic == null)
            {
                return NotFound();
            }

            return View(vodic);
        }

        // GET: Vodics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vodics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImePrezime,Email,Telefon,Iskustvo")] Vodic vodic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vodic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vodic);
        }

        // GET: Vodics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vodic == null)
            {
                return NotFound();
            }

            var vodic = await _context.Vodic.FindAsync(id);
            if (vodic == null)
            {
                return NotFound();
            }
            return View(vodic);
        }

        // POST: Vodics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImePrezime,Email,Telefon,Iskustvo")] Vodic vodic)
        {
            if (id != vodic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vodic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VodicExists(vodic.Id))
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
            return View(vodic);
        }

        // GET: Vodics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vodic == null)
            {
                return NotFound();
            }

            var vodic = await _context.Vodic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vodic == null)
            {
                return NotFound();
            }

            return View(vodic);
        }

        // POST: Vodics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vodic == null)
            {
                return Problem("Entity set 'MVCTurizamContext.Vodic'  is null.");
            }
            var vodic = await _context.Vodic.FindAsync(id);
            if (vodic != null)
            {
                _context.Vodic.Remove(vodic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VodicExists(int id)
        {
          return _context.Vodic.Any(e => e.Id == id);
        }
    }
}
