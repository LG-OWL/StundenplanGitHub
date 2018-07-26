using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stundenplan.Data
{
    public class Lehrer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        [NotMapped]
        public int? StundenId { get; set; }
        [NotMapped]
        public Stunden Stunden { get; set; }
        
    }
}
