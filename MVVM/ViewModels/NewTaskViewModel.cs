using System.Collections.ObjectModel;
using TaskPlanner.MVVM.Models;
using TaskPlanner.Services;

namespace TaskPlanner.MVVM.ViewModels
{
    public class NewTaskViewModel
    {
        private readonly DatabaseService _databaseService;

        public string Task { get; set; }
        public ObservableCollection<MyTask> Tasks { get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        public NewTaskViewModel(DatabaseService databaseService, ObservableCollection<Category> categories, ObservableCollection<MyTask> tasks)
        {
            _databaseService = databaseService;
            Categories = categories;
            Tasks = tasks;
        }

        public async Task AddTaskAsync(MyTask task)
        {
            await _databaseService.SaveTaskAsync(task);
            Tasks.Add(task);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _databaseService.SaveCategoryAsync(category);
            Categories.Add(category);
        }
    }

}
