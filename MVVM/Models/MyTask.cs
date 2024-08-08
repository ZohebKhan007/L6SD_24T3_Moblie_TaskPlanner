using PropertyChanged;
using SQLite;

namespace TaskPlanner.MVVM.Models
{
    [AddINotifyPropertyChangedInterface]
    public class MyTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string TaskName { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public int CategoryID { get; set; }
        public string TaskColor { get; set; } = string.Empty;
    }
}
