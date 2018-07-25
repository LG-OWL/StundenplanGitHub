using Stundenplan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stundenplan.Tests
{
    public class Testing
    {
        public static List<StundenTestwerteViewModel> CreateTestValues()
        {
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
            return stundenlist;
        }
    }
}
