using Microsoft.EntityFrameworkCore;
using Stundenplan.Data;
using Stundenplan.Models;
using Stundenplan.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Stundenplan.Services
{
    public class StundenService : IStundenService
    {
        private StundenplanDbContext _context;

        public StundenService(StundenplanDbContext context)
        {
            _context = context;
        }

        public List<Klasse> GetKlassen()
        {
            return _context.Klasse.ToList();
        }

        public List<Stunden> GetStundenByKlasseId(int klasseId)
        {
            var result = _context.Klasse
                .Include(klasse => klasse.Stundens)
                .ThenInclude(s => s.Lehrer)
                .FirstOrDefault(k => k.Id == klasseId)
                .Stundens;
            return result.ToList();
        }

        public List<Lehrer> GetLehrer()
        {
            return _context.Lehrer.ToList();
        }

        public List<Raum> GetRaeume()
        {
            return _context.Raum.ToList();
        }

        public void AddVertretungslehrer(KlasseTestwerteViewModel vm,int klasseId, int wochentag, int stunde, int lehrerId, int raumId)
        {
            vm.Stunden = GetStundenByKlasseId(klasseId);
            var vertretungsstunde = vm.Stunden.First(s => s.Wochentag == wochentag && s.Stunde == stunde);
            vertretungsstunde.VertretungslehrerId = lehrerId;
            vertretungsstunde.RaumId = raumId;
            _context.SaveChanges();
        }

        public bool CheckWhetherLehrerExists(string name)
        {
            if (_context.Lehrer.Any(l => l.Name == name))
                return true;
            return false;
        }

        public int GetLehrerIdByLehrerName(string name)
        {
            return _context.Lehrer.First(s => s.Name == name).Id;
        }

        public bool IsAdmin(string name, string passwort)
        {
            if (_context.Admin.Any(a => a.Name == name && a.Passwort == passwort))
                return true;
            return false;
        }

    }
}
