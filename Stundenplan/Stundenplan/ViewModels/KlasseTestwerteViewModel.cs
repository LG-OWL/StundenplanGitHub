﻿using Stundenplan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stundenplan.ViewModels
{
    public class KlasseTestwerteViewModel
    {
        public int Id { get; set; }

        public string Bezeichnung { get; set; }
        public IList<Stunden> Stunden { get; set; }
    }
}
