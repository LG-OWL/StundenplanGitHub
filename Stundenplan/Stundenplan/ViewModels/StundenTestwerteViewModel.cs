﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stundenplan.ViewModels
{
    public class StundenTestwerteViewModel
    {
        public int Id { get; set; }

        public string Wochentag { get; set; }
        public int Stunde { get; set; }
        public string Fach { get; set; }

        //Foreign Key
        public int KlasseId { get; set; }
        //Navigation Property
        public KlasseTestwerteViewModel Schulklasse { get; set; }
    }
}
