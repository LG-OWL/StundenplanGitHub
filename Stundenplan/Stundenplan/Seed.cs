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
                    var lehrer1 = AddLehrer(dbContext,null, "Müller"); 
                    AddStunde(dbContext, klasse6a.Id, wochentagCounter, stundenCounter, lehrer1.Id,null);
                    AddStunde(dbContext, klasse6b.Id, wochentagCounter, stundenCounter,null,null);
                }
            }
            var vertretungsraum = AddRaum(dbContext, "B1.25");
            var vertretungslehrer = AddLehrer(dbContext, null, "MAX!");
            var vertretungsstunde = klasse6a.Stundens.FirstOrDefault(s=> s.Id == 1);
            vertretungsstunde.VertretungslehrerId = vertretungslehrer.Id;
            vertretungsstunde.RaumId = vertretungsraum.Id;

            //Admin
            Token token = new Token() { Inhalt = "ADHdvJPdjg42)$/AS,D)E§SADasdDASDkasd-_aA123DAS-dlk99232DDD..AD;daWEaD1!dasdADasDa7-adad-___dasdaiudad" };
            dbContext.Admin.Add(new Admin() { Name = "Lorenz", Passwort = "123", Token = token });
            dbContext.SaveChanges();

        }

        private static Raum AddRaum(StundenplanDbContext dbContext, string bezeichnung)
        {
            var raum = new Raum() { Bezeichnung = bezeichnung};
            dbContext.Raum.Add(raum);
            dbContext.SaveChanges();
            return raum;
        }

        private static Klasse AddKlasse(StundenplanDbContext dbContext, string name)
        {
            var klasse = new Klasse() { Bezeichnung = name };
            dbContext.Klasse.Add(klasse);
            dbContext.SaveChanges();
            klasse.Stundens = new List<Stunden>();
            return klasse;
        }

        private static Lehrer AddLehrer(StundenplanDbContext dbContext,int? stundenId, string name)
        {
            Lehrer lehrer = new Lehrer()
            {
                Name = name,
                StundenId = stundenId
            };
            dbContext.Lehrer.Add(lehrer);
            dbContext.SaveChanges();
            return lehrer;
        }



        private static Stunden AddStunde(StundenplanDbContext dbContext, int klasseId, int wochentag, int stundeNr, int? lehrerId, int? vertretungslehrerId)
        {
            string lessonStr = GetRandomLesson();
            Stunden stunden = new Stunden()
            {
                Stunde = stundeNr,
                Wochentag = wochentag,
                Fach = lessonStr,
                KlasseId = klasseId,
                LehrerId = lehrerId,
                VertretungslehrerId = vertretungslehrerId
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
