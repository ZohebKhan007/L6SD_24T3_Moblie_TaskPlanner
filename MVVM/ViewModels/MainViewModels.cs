using PropertyChanged;
using System.Collections.ObjectModel;
using TaskPlanner.MVVM.Models;
using TaskPlanner.Services;

namespace TaskPlanner.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModels
    {
        public DatabaseService DatabaseService { get; }

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
                var tasks = from t in Tasks
                            where t.CategoryID == c.Id
                            select t;

                var completed = from t in tasks
                                where t.Completed == true
                                select t;

                var notCompleted = from t in tasks
                                   where t.Completed == false
                                   select t;

                c.PendingTasks = notCompleted.Count();
                c.Percentage = (float)completed.Count() / (float)tasks.Count();
            }
            foreach (var t in Tasks)
            {
                var catColor =
                    (from c in Categories
                     where c.Id == t.CategoryID
                     select c.Color).FirstOrDefault();
                t.TaskColor = catColor;
            }
        }
    }
}
