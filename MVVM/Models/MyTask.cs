using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner.MVVM.Models
{
    [AddINotifyPropertyChangedInterface]
    public class MyTask
    {
        public string TaskName { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public int CategoryID { get; set; }
        public string TaskColor { get; set; } = string.Empty;
    }
}
