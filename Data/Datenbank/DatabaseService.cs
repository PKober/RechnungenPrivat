using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using SQLite;

namespace RechnungenPrivat.Data.Datenbank
{
    class DatabaseService : IDatabaseService
    {

        private SQLiteAsyncConnection _database;

        private static string DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RechnungenPrivat.db3");
        /// <summary>
        /// This method initializes the database connection and creates the tables if they do not exist.
        /// </summary>
        /// <returns></returns>
        private async Task Init()
        {
            if (_database != null)
            {
                return;
            }
            _database = new SQLiteAsyncConnection(DbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
            await _database.CreateTableAsync<Kunde>();
            await _database.CreateTableAsync<Auftrag>();


        }
        /// <summary>
        /// This method deletes a customer from the database.
        /// </summary>
        /// <param name="kunde"></param>
        /// <returns></returns>
        public async Task<int> DeleteKundeAsync(Kunde kunde)
        {
            await Init();
            return await _database.DeleteAsync(kunde);
        }
        /// <summary>
        /// This method retrieves all customers from the database.
        /// </summary>
        /// <returns>List</returns>
        public async Task<List<Kunde>> GetAllKundenAsync()
        {
            await Init();
            return await _database.Table<Kunde>().ToListAsync();
        }
        /// <summary>
        /// This method retrieves a customer by its ID from the database.   
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Kunde</returns>
        public async Task<Kunde> GetKundeByIdAsync(int id)
        {
            await Init();
            return await _database.Table<Kunde>().Where(k => k.Id == id).FirstOrDefaultAsync();
        }
        /// <summary>
        /// This method saves a customer to the database.
        /// Checks if the customer already exists in the database.
        /// Based on the check, it either updates or inserts the customer.
        /// If the customer already exists, it returns 0.
        /// </summary>
        /// <param name="kunde"></param>
        /// <returns></returns>
        public async Task<int> SaveKundeAsync(Kunde kunde)
        {
            await Init();
            if (kunde.Id != 0)
            {
                return await _database.UpdateAsync(kunde);
            }
            else
            {
                var kundenCheck = await GetAllKundenAsync();
                if (kundenCheck != null)
                {
                    foreach (var k in kundenCheck)
                    {
                        if (kunde.KundenName == k.KundenName)
                        {
                            return 0;
                        }
                        else
                        {
                            return await _database.InsertAsync(kunde);
                        }
                    }
                    return await _database.InsertAsync(kunde);
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// This method retrieves a customer by its name from the database. 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Kunde> GetKundeByNameAsync(string name)
        {
            await Init();
            return await _database.Table<Kunde>().Where(k => k.KundenName == name).FirstOrDefaultAsync();
        }
        /// <summary>
        /// This Method retrieves a Customer by his ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetKundeByID(int id)
        {
            await Init();
            var kunde = await _database.Table<Kunde>().Where(k => k.Id == id).FirstOrDefaultAsync();
            if (kunde != null)
            {
                return kunde.KundenName;
            }
            else
            {
                return "Kunde nicht gefunden";
            }
        }
        /// <summary>
        /// This method deletes a customer by its name from the database.   
        /// </summary>
        /// <param name="kundennamen"></param>
        /// <returns></returns>
        /// 
        public async Task<int> DeleteKundeByName(string kundennamen)
        {
            await Init();
            var kunde = await GetKundeByNameAsync(kundennamen);
            if (kunde != null)
            {
                return await _database.DeleteAsync(kunde);
            }
            return 0;
        }
        /// <summary>
        /// This method Deletes one order from the database.
        /// </summary>
        /// <param name="auftrag"></param>
        /// <returns></returns>
        public async Task<int> DeleteAuftragAsync(Auftrag auftrag)
        {
            await Init();
            return await _database.DeleteAsync(auftrag);
        }
        /// <summary>
        /// This method retrieves all orders from the database.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Auftrag>> GetAllAuftraegeAsync()
        {
            await Init();
            return await _database.Table<Auftrag>().ToListAsync();
        }
        /// <summary>
        /// This method gives the order by its ID from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Auftrag> GetAuftragByIdAsync(int id)
        {
            await Init();
            return await _database.Table<Auftrag>().Where(a => a.Id == id).FirstOrDefaultAsync();
        }
        /// <summary>
        /// This method saves an order to the database.
        /// </summary>
        /// <param name="auftrag"></param>
        /// <returns></returns>
        public async Task<int> SaveAuftragAsync(Auftrag auftrag)
        {
            await Init();
            if (auftrag.Id != 0)
            {
                return await _database.UpdateAsync(auftrag);
            }
            else
            {
                return await _database.InsertAsync(auftrag);
            }
        }
        /// <summary>
        /// This method retrieves an order by its name from the database.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Auftrag> GetAuftragByNameAsync(string name)
        {
            await Init();
            return await _database.Table<Auftrag>().Where(a => a.Auftragsname == name).FirstOrDefaultAsync();
        }
        /// <summary>
        /// This method deletes an order by its name from the database.
        /// </summary>
        /// <param name="auftragsname"></param>
        /// <returns></returns>
        public async Task<int> DeleteAuftragByName(string auftragsname)
        {
            await Init();
            var auftrag = await GetAuftragByNameAsync(auftragsname);
            if (auftrag != null)
            {
                return await _database.DeleteAsync(auftrag);
            }
            return 0;
        }
        /// <summary>
        /// This method retrieves all orders by the customer ID from the database.
        /// </summary>
        /// <param name="kundeId"></param>
        /// <returns></returns>
        public async Task<List<Auftrag>> GetAllAuftraegeByKundeIdAsync(int kundeId)
        {
            await Init();
            return await _database.Table<Auftrag>().Where(a => a.KundeId == kundeId).ToListAsync();
        }
        /// <summary>
        /// This method retrieves all customers by the order ID from the database.
        /// </summary>
        /// <param name="auftragId"></param>
        /// <returns></returns>
        public async Task<List<Kunde>> GetAllKundenByAuftragIdAsync(int auftragId)
        {
            await Init();
            return await _database.Table<Kunde>().Where(k => k.Id == auftragId).ToListAsync();
        }
    }
}
