using TaskPlanner.MVVM.Views;

namespace TaskPlanner
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            MainPage = new NavigationPage(serviceProvider.GetRequiredService<PrivacyPrinciples>());
        }
    }
}
