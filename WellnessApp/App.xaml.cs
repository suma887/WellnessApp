namespace WellnessApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // ✅ This should reference your LoginPage correctly
        MainPage = new NavigationPage(new LoginPage());
    }
}
