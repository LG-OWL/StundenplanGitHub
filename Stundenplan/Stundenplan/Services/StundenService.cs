using Microsoft.EntityFrameworkCore;
using Stundenplan.Data;
using Stundenplan.Models;
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

    }
}
