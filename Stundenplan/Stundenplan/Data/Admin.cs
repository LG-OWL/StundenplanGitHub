using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stundenplan.Data
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Passwort { get; set; }

        public int TokenId { get; set; }
        public Token Token { get; set; }
    }
}
