namespace WellnessApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // Optional: show welcome message
                await DisplayAlert("Welcome", $"Hello {username}!", "OK");

                // Navigate to MainPage
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Error", "Please enter username and password", "OK");
            }
        }
    }
}
