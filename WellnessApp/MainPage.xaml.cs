namespace AutismSupportApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        // Default values
        MoodSlider.Value = 2;
        FocusSlider.Value = 5;
        ActivitySlider.Value = 30;
    }

    private void MoodSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        int mood = (int)Math.Round(e.NewValue);
        MoodLabel.Text = mood switch
        {
            0 => "Sad 😞",
            1 => "Calm 😌",
            2 => "Happy 😊",
            3 => "Excited 🤩",
            4 => "Overstimulated 😣",
            _ => "Neutral 🙂"
        };
    }

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (sender == FocusSlider)
            FocusLabel.Text = e.NewValue.ToString("0");
        else if (sender == ActivitySlider)
            ActivityLabel.Text = $"{e.NewValue:0} min";
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        string childName = ChildNameEntry.Text?.Trim() ?? "Unknown";
        string date = DatePicker.Date.ToString("D");
        string mood = MoodLabel.Text;
        string focus = FocusLabel.Text;
        string communication = CommunicationPicker.SelectedItem?.ToString() ?? "Not selected";
        string activity = ActivityLabel.Text;
        string notes = NotesEditor.Text?.Trim() ?? "No additional notes.";

        string status = GetStatus(Convert.ToInt32(FocusSlider.Value), (int)MoodSlider.Value);
        string recommendation = GetRecommendation(status, communication);

        DisplayAlert("Daily Activity Summary",
            $"📅 Date: {date}\n" +
            $"👦 Child: {childName}\n\n" +
            $"Mood: {mood}\n" +
            $"Focus Level: {focus}/10\n" +
            $"Communication: {communication}\n" +
            $"Activity Duration: {activity}\n\n" +
            $"🧾 Status: {status}\n" +
            $"💡 Recommendation: {recommendation}\n\n" +
            $"🗒 Notes: {notes}",
            "OK");
    }

    private string GetStatus(int focus, int moodLevel)
    {
        if (focus >= 8 && moodLevel >= 2) return "Excellent Day";
        if (focus >= 5 && moodLevel >= 1) return "Good Day";
        if (focus >= 3 || moodLevel == 0) return "Challenging Day";
        return "Needs Attention";
    }

    private string GetRecommendation(string status, string communication)
    {
        return status switch
        {
            "Excellent Day" => "Maintain current strategies and provide positive feedback.",
            "Good Day" => "Encourage consistent communication and light activities.",
            "Challenging Day" => communication == "No communication"
                ? "Use visual aids or gentle sensory breaks to encourage engagement."
                : "Allow frequent short breaks and maintain a calm environment.",
            "Needs Attention" => "Offer rest and monitor for potential sensory overload. Provide calm reassurance.",
            _ => "Continue observing and documenting behavioral patterns."
        };
    }
}
