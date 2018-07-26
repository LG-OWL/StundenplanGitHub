using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stundenplan.Models;
using Stundenplan.ViewModels;
using Stundenplan.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Stundenplan.Controllers
{
    public class HomeController : Controller
    {
        private StundenplanDbContext _context;

        public HomeController(StundenplanDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            KlasseTestwerteViewModel vm = new KlasseTestwerteViewModel();
            var klassenliste = _context.Klasse.ToList();
            SelectList list = new SelectList(klassenliste, "Id", "Bezeichnung");
            ViewBag.klasselist = list;

            return View(vm);
        }
        [HttpGet]
        public IActionResult Index(int? klassen)
        {
            KlasseTestwerteViewModel vm = new KlasseTestwerteViewModel();
            if (klassen != null)
            {
                var result = _context.Klasse
                .Include(klasse => klasse.Stundens)
                .ThenInclude(s => s.Lehrer)
                .FirstOrDefault(k => k.Id == klassen)
                .Stundens;
                vm.Stunden = result.ToList();
            }
            var klassenliste = _context.Klasse.ToList();
            SelectList list = new SelectList(klassenliste, "Id", "Bezeichnung");
            ViewBag.klasselist = list;

            return View(vm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
