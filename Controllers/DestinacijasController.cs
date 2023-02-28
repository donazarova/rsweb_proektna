using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCTurizam.Data;
using MVCTurizam.Models;
using MVCTurizam.ViewModels;

namespace MVCTurizam.Controllers
{
    public class DestinacijasController : Controller
    {
        private readonly MVCTurizamContext _context;

        public DestinacijasController(MVCTurizamContext context)
        {
            _context = context;
        }

        // GET: Destinacijas
        public async Task<IActionResult> Index(string destinacijaKontinent, string searchString)
        {
            IQueryable<Destinacija> destinacii = _context.Destinacija.AsQueryable();
            IQueryable<string> kontinentQuery = _context.Destinacija.OrderBy(m => m.Kontinent).Select(m => m.Kontinent).Distinct();
            if (!string.IsNullOrEmpty(searchString))
            {
                destinacii = destinacii.Where(s => s.Ime.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(destinacijaKontinent))
            {
                destinacii = destinacii.Where(x => x.Kontinent == destinacijaKontinent);
            }
            destinacii = destinacii.Include(m => m.Vodic)
            .Include(m => m.Patuvanje).ThenInclude(m => m.Klient);
            var destinacijaKontinentVM = new DestinacijaFilterViewModel
            {
                Kontinenti = new SelectList(await kontinentQuery.ToListAsync()),
                Destinacii = await destinacii.ToListAsync()
            };
            return View(destinacijaKontinentVM);

        }

        // GET: Destinacijas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Destinacija == null)
            {
                return NotFound();
            }

            var destinacija = await _context.Destinacija
                .Include(d => d.Vodic)
                .Include(m => m.Patuvanje).ThenInclude(m => m.Klient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (destinacija == null)
            {
                return NotFound();
            }

            return View(destinacija);
        }

        // GET: Destinacijas/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["VodicId"] = new SelectList(_context.Set<Vodic>(), "Id", "ImePrezime");
            return View();
        }

        // POST: Destinacijas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ime,Drzava,Kontinent,Dalecina,Temperatura,CenaKarta,SlikaOdDestinacija,VodicId")] Destinacija destinacija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(destinacija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VodicId"] = new SelectList(_context.Set<Vodic>(), "Id", "ImePrezime", destinacija.VodicId);
            return View(destinacija);
        }

        // GET: Destinacijas/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Destinacija == null)
            {
                return NotFound();
            }

            var destinacija = _context.Destinacija.Where(m => m.Id == id).Include(m => m.Patuvanje).First();
            if (destinacija == null)
            {
                return NotFound();
            }

            var klients = _context.Klient.AsEnumerable();
            klients = klients.OrderBy(s => s.ImePrezime);
            DestinacijaKlientsEditViewModel viewmodel = new DestinacijaKlientsEditViewModel
            {
                Destinacija = destinacija,
                KlientList = new MultiSelectList(klients, "Id", "ImePrezime"),
                SelectedKlients = destinacija.Patuvanje.Select(sa => sa.KlientId)
            };
            ViewData["VodicId"] = new SelectList(_context.Set<Vodic>(), "Id", "ImePrezime", destinacija.VodicId);
            return View(viewmodel);
        }

        // POST: Destinacijas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DestinacijaKlientsEditViewModel viewmodel)
        {
            if (id != viewmodel.Destinacija.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.Destinacija);
                    await _context.SaveChangesAsync();
                    IEnumerable<int> listKlients = viewmodel.SelectedKlients;
                    IQueryable<Patuvanje> toBeRemoved = _context.Patuvanje.Where(s => !listKlients.Contains(s.KlientId) && s.DestinacijaId == id);
                    _context.Patuvanje.RemoveRange(toBeRemoved);
                    IEnumerable<int> existKlients = _context.Patuvanje.Where(s => listKlients.Contains(s.KlientId) && s.DestinacijaId == id).Select(s => s.KlientId);
                    IEnumerable<int> newKlients = listKlients.Where(s => !existKlients.Contains(s));
                    foreach (int klientId in newKlients)
                        _context.Patuvanje.Add(new Patuvanje { KlientId = klientId, DestinacijaId = id });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestinacijaExists(viewmodel.Destinacija.Id))
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
            ViewData["VodicId"] = new SelectList(_context.Set<Vodic>(), "Id", "ImePrezime", viewmodel.Destinacija.VodicId);
            return View(viewmodel);
        }

        // POST: Knigas/EditPicture/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPicutre(int id, AddImageDestinacija viewModel)
        {
            if (id != viewModel.Destinacija.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = UploadedFile(viewModel);
                    viewModel.Destinacija.SlikaOdDestinacija = uniqueFileName;
                    _context.Update(viewModel.Destinacija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestinacijaExists(viewModel.Destinacija.Id))
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
            return View(viewModel);
        }


        // GET: Destinacijas/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Destinacija == null)
            {
                return NotFound();
            }

            var destinacija = await _context.Destinacija
                .Include(d => d.Vodic)
                .Include(m => m.Patuvanje).ThenInclude(m => m.Klient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (destinacija == null)
            {
                return NotFound();
            }

            return View(destinacija);
        }

        // POST: Destinacijas/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Destinacija == null)
            {
                return Problem("Entity set 'MVCTurizamContext.Destinacija'  is null.");
            }
            var destinacija = await _context.Destinacija.FindAsync(id);
            if (destinacija != null)
            {
                _context.Destinacija.Remove(destinacija);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DestinacijaExists(int id)
        {
          return _context.Destinacija.Any(e => e.Id == id);
        }

        private string UploadedFile(AddImageDestinacija model)
        {
            string uniqueFileName = null;
            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ProfileImage.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream); //model.ProfileImage of type IFormFile?
                }
            }
            return uniqueFileName;
        }
    }
}
