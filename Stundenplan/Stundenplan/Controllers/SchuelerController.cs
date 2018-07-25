using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stundenplan.Data;
using Stundenplan.Models;

namespace Stundenplan.Controllers
{
    public class SchuelerController : Controller
    {
        private readonly StundenplanDbContext _context;

        public SchuelerController(StundenplanDbContext context)
        {
            _context = context;
        }

        // GET: Schueler
        public async Task<IActionResult> Index()
        {
            var stundenplanDbContext = _context.Schueler.Include(s => s.Schulklasse);
            return View(await stundenplanDbContext.ToListAsync());
        }

        // GET: Schueler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schueler = await _context.Schueler
                .Include(s => s.Schulklasse)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (schueler == null)
            {
                return NotFound();
            }

            return View(schueler);
        }

        // GET: Schueler/Create
        public IActionResult Create()
        {
            ViewData["KlasseId"] = new SelectList(_context.Klasse, "Id", "Id");
            return View();
        }

        // POST: Schueler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,KlasseId")] Schueler schueler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schueler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KlasseId"] = new SelectList(_context.Klasse, "Id", "Id", schueler.KlasseId);
            return View(schueler);
        }

        // GET: Schueler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schueler = await _context.Schueler.SingleOrDefaultAsync(m => m.Id == id);
            if (schueler == null)
            {
                return NotFound();
            }
            ViewData["KlasseId"] = new SelectList(_context.Klasse, "Id", "Id", schueler.KlasseId);
            return View(schueler);
        }

        // POST: Schueler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,KlasseId")] Schueler schueler)
        {
            if (id != schueler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schueler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchuelerExists(schueler.Id))
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
            ViewData["KlasseId"] = new SelectList(_context.Klasse, "Id", "Id", schueler.KlasseId);
            return View(schueler);
        }

        // GET: Schueler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schueler = await _context.Schueler
                .Include(s => s.Schulklasse)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (schueler == null)
            {
                return NotFound();
            }

            return View(schueler);
        }

        // POST: Schueler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schueler = await _context.Schueler.SingleOrDefaultAsync(m => m.Id == id);
            _context.Schueler.Remove(schueler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchuelerExists(int id)
        {
            return _context.Schueler.Any(e => e.Id == id);
        }
    }
}
