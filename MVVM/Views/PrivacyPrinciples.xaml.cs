using TaskPlanner.Services;

namespace TaskPlanner.MVVM.Views;

public partial class PrivacyPrinciples : ContentPage
{
    private readonly DatabaseService _databaseService;
	public PrivacyPrinciples(DatabaseService databaseService)
	{
		InitializeComponent();
        _databaseService = databaseService;
	}

    private async void OnAcceptClicked(object sender, EventArgs e)
    {
        // Directed to the Landing Page
        await Navigation.PushAsync(new LandingPage(_databaseService));
    }

    private void OnDeclineClicked(object sender, EventArgs e)
    {
        // Exit the app
        // System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
#if ANDROID
        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#endif
    }
}