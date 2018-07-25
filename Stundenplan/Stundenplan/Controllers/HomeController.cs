using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stundenplan.Models;
using Stundenplan.ViewModels;

namespace Stundenplan.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            KlasseTestwerteViewModel vm = new KlasseTestwerteViewModel();
            List<StundenTestwerteViewModel> stundenlist = new List<StundenTestwerteViewModel>();
            for (int i = 0; i < 5; i++)
            {
                Random rnd = new Random();
                string wochentag;
                switch (i)
                {
                    case 0:
                        wochentag = "Montag";
                        break;
                    case 1:
                        wochentag = "Dienstag";
                        break;
                    case 2:
                        wochentag = "Mittwoch";
                        break;
                    case 3:
                        wochentag = "Donnerstag";
                        break;
                    case 4:
                        wochentag = "Freitag";
                        break;
                    default:
                        wochentag = "";
                        break;
                }
                for (int j = 1; j <= 6; j++)
                {
                    int lessonDecision = rnd.Next(1, 4);
                    string lessonStr;
                    switch (lessonDecision)
                    {
                        case 1:
                            lessonStr = "Mathe";
                            break;
                        case 2:
                            lessonStr = "Deutsch";
                            break;
                        case 3:
                            lessonStr = "Englisch";
                            break;
                        default:
                            lessonStr = "Frei";
                            break;
                    }
                    StundenTestwerteViewModel stunden = new StundenTestwerteViewModel() { Stunde = j, Wochentag = wochentag, Fach = lessonStr };
                    stundenlist.Add(stunden);
                }
            }
            vm.Stunden = stundenlist;
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
