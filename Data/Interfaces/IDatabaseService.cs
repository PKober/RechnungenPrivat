using RechnungenPrivat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.Data.Interfaces
{
    public interface IDatabaseService
    {
        Task<Kunde> GetKundeByIdAsync(int id);
        Task<List<Kunde>> GetAllKundenAsync();
        Task<Kunde> GetKundeByNameAsync(string name);
        Task<int> SaveKundeAsync(Kunde kunde);
        Task<int> DeleteKundeAsync(Kunde kunde);
        Task<int> DeleteKundeByName(string kundennamen);
    }
}
