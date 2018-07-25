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
    public class KlasseController : Controller
    {
        private readonly StundenplanDbContext _context;

        public KlasseController(StundenplanDbContext context)
        {
            _context = context;
        }

        // GET: Klasse
        public async Task<IActionResult> Index()
        {
            return View(await _context.Klasse.ToListAsync());
        }

        // GET: Klasse/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klasse = await _context.Klasse
                .SingleOrDefaultAsync(m => m.Id == id);
            if (klasse == null)
            {
                return NotFound();
            }

            return View(klasse);
        }

        // GET: Klasse/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Klasse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Bezeichnung")] Klasse klasse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klasse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(klasse);
        }

        // GET: Klasse/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klasse = await _context.Klasse.SingleOrDefaultAsync(m => m.Id == id);
            if (klasse == null)
            {
                return NotFound();
            }
            return View(klasse);
        }

        // POST: Klasse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Bezeichnung")] Klasse klasse)
        {
            if (id != klasse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klasse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlasseExists(klasse.Id))
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
            return View(klasse);
        }

        // GET: Klasse/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klasse = await _context.Klasse
                .SingleOrDefaultAsync(m => m.Id == id);
            if (klasse == null)
            {
                return NotFound();
            }

            return View(klasse);
        }

        // POST: Klasse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var klasse = await _context.Klasse.SingleOrDefaultAsync(m => m.Id == id);
            _context.Klasse.Remove(klasse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlasseExists(int id)
        {
            return _context.Klasse.Any(e => e.Id == id);
        }
    }
}
