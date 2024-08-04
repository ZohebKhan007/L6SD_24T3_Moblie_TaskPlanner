using TaskPlanner.MVVM.ViewModels;
using TaskPlanner.Services;

namespace TaskPlanner.MVVM.Views
{
    public partial class MainView : ContentPage
    {
        private MainViewModels mainViewModels;

        public MainView(DatabaseService databaseService)
        {
            InitializeComponent();
            mainViewModels = new MainViewModels(databaseService);
            BindingContext = mainViewModels;
        }


        private void checkBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            mainViewModels.UpdateData();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var taskView = new NewTaskView(mainViewModels.DatabaseService, mainViewModels.Categories, mainViewModels.Tasks);
            Navigation.PushAsync(taskView);
        }
    }
}
