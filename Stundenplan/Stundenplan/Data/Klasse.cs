using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stundenplan.Data
{
    public class Klasse
    {
        public int Id { get; set; }

        public string Bezeichnung { get; set; }

        //Navigation Property
        public IList<Schueler> Schuelers { get; set; }

        public IList<Stunden> Stundens { get; set; }
    }
}
