using PropertyChanged;
using System.Collections.ObjectModel;
using TaskPlanner.MVVM.Models;
using TaskPlanner.Services;

namespace TaskPlanner.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModels
    {

        public DatabaseService DatabaseService { get; set; }

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<MyTask> Tasks { get; set; }

        public MainViewModels(DatabaseService databaseService)
        {
            DatabaseService = databaseService;

            Categories = new ObservableCollection<Category>(); // Initialize the collection
            Tasks = new ObservableCollection<MyTask>(); // Initialize the collection
            Tasks.CollectionChanged += Tasks_CollectionChanged;

            LoadData();
        }


        private void Tasks_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateData();
        }

        private async void LoadData()
        {
            var categoriesFromDb = await DatabaseService.GetCategoriesAsync();
            foreach (var category in categoriesFromDb)
            {
                Categories.Add(category);
            }

            var tasksFromDb = await DatabaseService.GetTasksAsync();
            foreach (var task in tasksFromDb)
            {
                Tasks.Add(task);
            }

            UpdateData();
        }

        public void UpdateData()
        {
            foreach (var c in Categories)
            {
                var tasks = Tasks.Where(t => t.CategoryID == c.Id);
                var completed = tasks.Where(t => t.Completed).Count();
                var totalTasks = tasks.Count();

                c.PendingTasks = totalTasks - completed;
                c.Percentage = totalTasks == 0 ? 0 : (float)completed / totalTasks;
            }
            foreach (var t in Tasks)
            {
                var catColor = Categories.FirstOrDefault(c => c.Id == t.CategoryID)?.Color;
                t.TaskColor = catColor;
            }
        }

        public async Task DeleteTaskAsync(MyTask task)
        {
            if (task == null) return;
            await DatabaseService.DeleteTaskAsync(task);
            Tasks.Remove(task);
            UpdateData();
        }
    }
}
