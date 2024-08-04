using SQLite;
using System.IO;
using TaskPlanner.MVVM.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TaskPlanner.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "TaskPlanner.db");
            _database = new SQLiteAsyncConnection(databasePath);

            // Initialize the tables
            InitializeTables();
        }

        private async Task InitializeTables()
        {
            await _database.CreateTableAsync<Category>();
            await _database.CreateTableAsync<MyTask>();
        }

        // CRUD operations for Categories
        public async Task<List<Category>> GetCategoriesAsync() => await _database.Table<Category>().ToListAsync();
        public async Task<int> SaveCategoryAsync(Category category) => await _database.InsertAsync(category);
        public async Task<int> UpdateCategoryAsync(Category category) => await _database.UpdateAsync(category);
        public async Task<int> DeleteCategoryAsync(Category category) => await _database.DeleteAsync(category);

        // CRUD operations for Tasks
        public async Task<List<MyTask>> GetTasksAsync() => await _database.Table<MyTask>().ToListAsync();
        public async Task<int> SaveTaskAsync(MyTask task) => await _database.InsertAsync(task);
        public async Task<int> UpdateTaskAsync(MyTask task) => await _database.UpdateAsync(task);
        public async Task<int> DeleteTaskAsync(MyTask task) => await _database.DeleteAsync(task);
    }
}
