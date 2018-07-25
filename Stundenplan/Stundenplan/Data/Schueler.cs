using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stundenplan.Data
{
    public class Schueler
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Foreign Key
        public int KlasseId { get; set; }
        //Navigation Property
        public Klasse Schulklasse { get; set; }

    }
}
