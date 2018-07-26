using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Stundenplan.Models;
using Stundenplan.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stundenplan.Services;

namespace Stundenplan.Controllers
{
    public class HomeController : Controller
    {
        private IStundenService _service;

        public HomeController(IStundenService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            KlasseTestwerteViewModel vm = new KlasseTestwerteViewModel();
            var klassenliste = _service.GetKlassen();
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
                vm.Stunden = _service.GetStundenByKlasseId(klassen.Value);
            }
            var klassenliste = _service.GetKlassen();
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
