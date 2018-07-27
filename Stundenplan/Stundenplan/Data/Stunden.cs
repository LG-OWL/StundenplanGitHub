using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stundenplan.Data
{
    public class Stunden
    {
        public int Id { get; set; }

        public int Wochentag { get; set; }
        public int Stunde { get; set; }
        public string Fach { get; set; }
        
        public int? LehrerId { get; set; }
        [ForeignKey("LehrerId")]
        public Lehrer Lehrer { get; set; }

        public int? VertretungslehrerId { get; set; }
        [ForeignKey("VertretungslehrerId")]
        public Lehrer Vertretungslehrer { get; set; }

        public int? RaumId { get; set; }
        [ForeignKey("RaumId")]
        public Raum Raum { get; set; }
        //Foreign Key
        public int KlasseId { get; set; }
        //Navigation Property
        public Klasse Schulklasse { get; set; }

        public override string ToString() { return Wochentag + "/" + Stunde + " - " + Fach + " - Lehrer: " + Lehrer.Name; }
    }
}
