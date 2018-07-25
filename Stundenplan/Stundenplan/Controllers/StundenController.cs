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
    public class StundenController : Controller
    {
        private readonly StundenplanDbContext _context;

        public StundenController(StundenplanDbContext context)
        {
            _context = context;
        }

        // GET: Stunden
        public async Task<IActionResult> Index()
        {
            var stundenplanDbContext = _context.Stunden.Include(s => s.Schulklasse);
            return View(await stundenplanDbContext.ToListAsync());
        }

        // GET: Stunden/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stunden = await _context.Stunden
                .Include(s => s.Schulklasse)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stunden == null)
            {
                return NotFound();
            }

            return View(stunden);
        }

        // GET: Stunden/Create
        public IActionResult Create()
        {
            ViewData["KlasseId"] = new SelectList(_context.Klasse, "Id", "Id");
            return View();
        }

        // POST: Stunden/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Wochentag,Stunde,Fach,KlasseId")] Stunden stunden)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stunden);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KlasseId"] = new SelectList(_context.Klasse, "Id", "Id", stunden.KlasseId);
            return View(stunden);
        }

        // GET: Stunden/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stunden = await _context.Stunden.SingleOrDefaultAsync(m => m.Id == id);
            if (stunden == null)
            {
                return NotFound();
            }
            ViewData["KlasseId"] = new SelectList(_context.Klasse, "Id", "Id", stunden.KlasseId);
            return View(stunden);
        }

        // POST: Stunden/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Wochentag,Stunde,Fach,KlasseId")] Stunden stunden)
        {
            if (id != stunden.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stunden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StundenExists(stunden.Id))
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
            ViewData["KlasseId"] = new SelectList(_context.Klasse, "Id", "Id", stunden.KlasseId);
            return View(stunden);
        }

        // GET: Stunden/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stunden = await _context.Stunden
                .Include(s => s.Schulklasse)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stunden == null)
            {
                return NotFound();
            }

            return View(stunden);
        }

        // POST: Stunden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stunden = await _context.Stunden.SingleOrDefaultAsync(m => m.Id == id);
            _context.Stunden.Remove(stunden);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StundenExists(int id)
        {
            return _context.Stunden.Any(e => e.Id == id);
        }
    }
}
