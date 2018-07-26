﻿using System;
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
        [ForeignKey("LehrerId")]
        public int? Lehrerid { get; set; }
        public Lehrer Lehrer { get; set; }

        public int? VertretungslehrerId { get; set; }
        public Lehrer Vertretungslehrer { get; set; }

        public int? RaumId { get; set; }

        public Raum Raum { get; set; }
        //Foreign Key
        public int KlasseId { get; set; }
        //Navigation Property
        public Klasse Schulklasse { get; set; }

        public override string ToString() { return Wochentag + "/" + Stunde + " - " + Fach + " - Lehrer: " + Lehrer.Name; }
    }
}
