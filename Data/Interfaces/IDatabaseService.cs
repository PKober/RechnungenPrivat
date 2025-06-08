using RechnungenPrivat.Models;

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
        Task<int> DeleteAuftragAsync(Auftrag auftrag);
        Task<List<Auftrag>> GetAllAuftraegeAsync();
        Task<Auftrag> GetAuftragByIdAsync(int id);
        Task<int> SaveAuftragAsync(Auftrag auftrag);
        Task<Auftrag> GetAuftragByNameAsync(string name);
        Task<int> DeleteAuftragByName(string auftragsname);
        Task<List<Auftrag>> GetAllAuftraegeByKundeIdAsync(int kundeId);
        Task<string> GetKundeByID(int id);


    }
}
