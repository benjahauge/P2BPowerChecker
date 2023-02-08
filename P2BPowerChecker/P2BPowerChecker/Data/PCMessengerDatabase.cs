using P2BPowerChecker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace P2BPowerChecker.Data
{
    public class PCMessengerDatabase
    {
        readonly SQLiteAsyncConnection database;

        public PCMessengerDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<PCMessenger>();
        }

        public Task<List<PCMessenger>> GetItemsAsync()
        {
            return database.Table<PCMessenger>().ToListAsync();
        }

        public Task<int> SaveItemAsync(PCMessenger item)
        {
            return database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(PCMessenger item)
        {
            return database.DeleteAsync(item);
        }

        public Task<int> UpdateItemAsync(PCMessenger item) 
        {
            return database.UpdateAsync(item);
        }
    }
}
