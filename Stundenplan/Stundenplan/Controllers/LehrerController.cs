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
    public class LehrerController : Controller
    {
        private readonly StundenplanDbContext _context;

        public LehrerController(StundenplanDbContext context)
        {
            _context = context;
        }

        // GET: Lehrer
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lehrer.ToListAsync());
        }

        // GET: Lehrer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lehrer = await _context.Lehrer
                .SingleOrDefaultAsync(m => m.Id == id);
            if (lehrer == null)
            {
                return NotFound();
            }

            return View(lehrer);
        }

        // GET: Lehrer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lehrer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Lehrer lehrer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lehrer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lehrer);
        }

        // GET: Lehrer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lehrer = await _context.Lehrer.SingleOrDefaultAsync(m => m.Id == id);
            if (lehrer == null)
            {
                return NotFound();
            }
            return View(lehrer);
        }

        // POST: Lehrer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Lehrer lehrer)
        {
            if (id != lehrer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lehrer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LehrerExists(lehrer.Id))
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
            return View(lehrer);
        }

        // GET: Lehrer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lehrer = await _context.Lehrer
                .SingleOrDefaultAsync(m => m.Id == id);
            if (lehrer == null)
            {
                return NotFound();
            }

            return View(lehrer);
        }

        // POST: Lehrer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lehrer = await _context.Lehrer.SingleOrDefaultAsync(m => m.Id == id);
            _context.Lehrer.Remove(lehrer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LehrerExists(int id)
        {
            return _context.Lehrer.Any(e => e.Id == id);
        }
    }
}
