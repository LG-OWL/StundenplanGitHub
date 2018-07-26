using Stundenplan.Data;
using System.Collections.Generic;

namespace Stundenplan.Services
{
    public interface IStundenService
    {
        List<Klasse> GetKlassen();

        List<Stunden> GetStundenByKlasseId(int klasseId);
    }
}
