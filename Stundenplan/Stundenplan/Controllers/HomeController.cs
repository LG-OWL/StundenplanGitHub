﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stundenplan.Models;
using Stundenplan.ViewModels;
using Stundenplan.Tests;

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
            //vm.Stunden = Testing.CreateTestValues();
            vm.Stunden = _context.Klasse.FirstOrDefault(k => k.Id == 1).Stundens;
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
