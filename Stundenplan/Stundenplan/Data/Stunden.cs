using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stundenplan.Data
{
    public class Stunden
    {
        public int Id { get; set; }

        public string Wochentag { get; set; }
        public int Stunde { get; set; }
        public string Fach { get; set; }

        public int LehrerId { get; set; }
        public Lehrer Lehrer { get; set; }

        //Foreign Key
        public int KlasseId { get; set; }
        //Navigation Property
        public Klasse Schulklasse { get; set; }

        public override string ToString() { return Wochentag + "/" + Stunde + " - " + Fach; }
    }
}
