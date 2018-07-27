using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Stundenplan.Models;
using Stundenplan.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stundenplan.Services;
using System.Linq;

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

        [HttpPost]
        public IActionResult Index(int? klassen, int? wochentag, int? stunde, int? lehrer, int? raum, string token)
        {
            KlasseTestwerteViewModel vm = new KlasseTestwerteViewModel();
            if (klassen != null && wochentag != null && stunde != null && lehrer != null && raum != null && _service.IsTokenKnown(token))
            {
                _service.AddVertretungslehrer(vm, klassen.Value, wochentag.Value, stunde.Value, lehrer.Value, raum.Value);
            }
            ////////////////////////////////////////////////////////////////////////////
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
            ViewBag.admin = false;
            FillingDropDownLists();

            ViewData["Message"] = "Vertretungsstunden - Eintrag";

            return View();
        }
        [HttpPost]
        public IActionResult Contact(string name, string passwort)
        {
            if (_service.IsAdmin(name, passwort))
            {
                ViewBag.admin = true;
                ViewData["Token"] = _service.GetAdminToken(name,passwort);
            }
            else
                ViewBag.admin = false;

            FillingDropDownLists();

            ViewData["Message"] = "Vertretungsstunden - Eintrag";

            return View();
        }

        public void FillingDropDownLists()
        {
            var klassenliste = _service.GetKlassen();
            SelectList dropbownlistKlasse = new SelectList(klassenliste, "Id", "Bezeichnung");
            ViewBag.klasselist = dropbownlistKlasse;

            var lehrerliste = _service.GetLehrer();
            SelectList dropbownlistLehrer = new SelectList(lehrerliste, "Id", "Name");
            ViewBag.lehrerlist = dropbownlistLehrer;

            var raumliste = _service.GetRaeume();
            SelectList dropbownlistRaum = new SelectList(raumliste, "Id", "Bezeichnung");
            ViewBag.raumlist = dropbownlistRaum;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
