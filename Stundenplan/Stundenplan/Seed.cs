using Microsoft.Extensions.DependencyInjection;
using Stundenplan.Data;
using Stundenplan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stundenplan
{
    public class Seed
    {
        public static void DoSeed(IServiceCollection services)
        {
            // Holen des DB-Kontextes
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetRequiredService<StundenplanDbContext>();

            // Löschen und Anlegen
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            if (dbContext.Schueler.Any())
                return;

            // SampleDaten hinzufügen
            var klasse6a = AddKlasse(dbContext, "6a");
            var klasse6b = AddKlasse(dbContext, "6b");

            for (int wochentagCounter = 0; wochentagCounter < 5; wochentagCounter++)
            {
                for (int stundenCounter = 1; stundenCounter <= 6; stundenCounter++)
                {
                    AddStunde(dbContext, klasse6a.Id, wochentagCounter, stundenCounter);
                    AddStunde(dbContext, klasse6b.Id, wochentagCounter, stundenCounter);
                }
            }
        }

        private static Klasse AddKlasse(StundenplanDbContext dbContext, string name)
        {
            var klasse = new Klasse() { Bezeichnung = name };
            dbContext.Klasse.Add(klasse);
            dbContext.SaveChanges();
            klasse.Stundens = new List<Stunden>();
            return klasse;
        }

        private static Stunden AddStunde(StundenplanDbContext dbContext, int klasseId, int wochentag, int stundeNr)
        {
            string lessonStr = GetRandomLesson();
            Stunden stunden = new Stunden()
            {
                Stunde = stundeNr,
                Wochentag = wochentag,
                Fach = lessonStr,
                KlasseId = klasseId
            };

            dbContext.Stunden.Add(stunden);
            dbContext.SaveChanges();
            return stunden;
        }

        private static string GetRandomLesson()
        {
            string[] lessons = { "Mathe", "Deutsch", "Englisch", "Frei" };

            Random rnd = new Random();
            int lessonDecision = rnd.Next(1, 4);
            return lessons[lessonDecision];
        }
    }
}
