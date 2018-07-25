using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stundenplan.Data;

namespace Stundenplan.Models
{
    public class StundenplanDbContext : DbContext
    {
        public StundenplanDbContext (DbContextOptions<StundenplanDbContext> options)
            : base(options)
        {
        }

        public DbSet<Stundenplan.Data.Klasse> Klasse { get; set; }

        public DbSet<Stundenplan.Data.Schueler> Schueler { get; set; }

        public DbSet<Stundenplan.Data.Stunden> Stunden { get; set; }
    }
}
