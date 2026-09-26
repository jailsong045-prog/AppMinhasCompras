using AppMinhasCompras.Models;
using SQLite;

namespace AppMinhasCompras.Helper
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _db;

        public SQLiteDatabaseHelper(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p)
        {
            return _db.InsertAsync(p);
        }

        public Task<List<Produto>> GetAll()
        {
            return _db.Table<Produto>().ToListAsync();
        }

        public Task<int> Update(Produto p)
        {
            return _db.UpdateAsync(p);
        }

        public Task<int> Delete(int id)
        {
            return _db.Table<Produto>().DeleteAsync(i => i.Id == id);
        }
    }
}