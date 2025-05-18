using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.Data.Datenbank
{
    class DatabaseService : IDatabaseService
    {

        private SQLiteAsyncConnection _database;

        private static string DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RechnungenPrivat.db3");

        private async Task Init()
        {
            if (_database != null)
            {
               return;
            }
            _database = new SQLiteAsyncConnection(DbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
            await _database.CreateTableAsync<Kunde>();
        }

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
                        if (kunde.Kundenname == k.Kundenname)
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


    }
}
