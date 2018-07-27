using Stundenplan.Data;
using Stundenplan.ViewModels;
using System.Collections.Generic;

namespace Stundenplan.Services
{
    public interface IStundenService
    {
        List<Klasse> GetKlassen();

        List<Stunden> GetStundenByKlasseId(int klasseId);

        List<Lehrer> GetLehrer();

        List<Raum> GetRaeume();

        void AddVertretungslehrer(KlasseTestwerteViewModel vm, int klasseId, int wochentag, int stunde, int lehrerId, int raumId);
        bool CheckWhetherLehrerExists(string name);

        int GetLehrerIdByLehrerName(string name);

        bool IsAdmin(string name, string passwort);

        bool IsTokenKnown(string token);

        string GetAdminToken(string name, string passwort);
    }
}
