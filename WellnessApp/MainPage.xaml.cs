// MainPage.xaml.cs
using System.Reflection;

namespace WellnessApp;

public partial class MainPage : ContentPage
{
    // Private fields to hold the selected gender and the current wellness score
    private string _selectedGender = "Male"; // Default to male
    private double _wellnessScore;

    public MainPage()
    {
        InitializeComponent();

        // Set default slider values as specified in the assignment
        SleepHoursSlider.Value = 7.0;
        StressLevelSlider.Value = 4.0;
        ActivityMinutesSlider.Value = 30.0;

        // Initially select the male gender and update the UI
        UpdateGenderSelection(_selectedGender);
    }

    /// <summary>
    /// Event handler for slider value changes.
    /// This method only updates the live value label.
    /// </summary>
    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        var slider = (Slider)sender;
        if (slider == SleepHoursSlider)
        {
            SleepHoursLabel.Text = $"{e.NewValue:F1} h";
        }
        else if (slider == StressLevelSlider)
        {
            StressLevelLabel.Text = $"{e.NewValue:F1}";
        }
        else if (slider == ActivityMinutesSlider)
        {
            ActivityMinutesLabel.Text = $"{e.NewValue:F1} min";
        }
    }

    /// <summary>
    /// Event handler for the gender image taps.
    /// This method updates the selected gender and re-highlights the UI.
    /// </summary>
    private void GenderImage_Tapped(object sender, TappedEventArgs e)
    {
        string newGender = e.Parameter.ToString();
        if (_selectedGender != newGender)
        {
            _selectedGender = newGender;
            UpdateGenderSelection(_selectedGender);
        }
    }

    /// <summary>
    /// Updates the visual state of the gender selection images.
    /// </summary>
    private void UpdateGenderSelection(string gender)
    {
        // Reset both borders to a neutral state
        MaleBorder.Stroke = Colors.Transparent;
        FemaleBorder.Stroke = Colors.Transparent;
        MaleBorder.StrokeThickness = 3;
        FemaleBorder.StrokeThickness = 3;

        // Highlight the selected gender's border
        if (gender == "Male")
        {
            MaleBorder.Stroke = Colors.CornflowerBlue;
        }
        else
        {
            FemaleBorder.Stroke = Colors.LightPink;
        }
    }

    /// <summary>
    /// Event handler for the Calculate button.
    /// </summary>
    private void CalculateButton_Clicked(object sender, EventArgs e)
    {
        // 1. Get the values from the sliders
        double sleepHours = SleepHoursSlider.Value;
        double stressLevel = StressLevelSlider.Value;
        double activityMinutes = ActivityMinutesSlider.Value;

        // 2. Calculate the raw score using the formula
        double rawScore = (sleepHours * 8) - (stressLevel * 5) + (activityMinutes * 0.5);

        // 3. Clamp the score between 0 and 100
        _wellnessScore = Math.Max(0, Math.Min(100, rawScore));

        // 4. Round the final score to the nearest whole number
        int finalScore = (int)Math.Round(_wellnessScore);

        // 5. Get the status and recommendations
        string status = GetStatus(finalScore);
        string recommendations = GetRecommendation(finalScore, _selectedGender);

        // 6. Display the results in a pop-up
        string genderIcon = _selectedGender == "Male" ? "♂️" : "♀️";
        DisplayAlert($"Your Wellness Score: {finalScore}", $"Status: {status}\n\nRecommendations: {recommendations}\n\nSelected Gender: {_selectedGender} {genderIcon}", "OK");
    }

    /// <summary>
    /// Classifies the wellness score into a status string.
    /// </summary>
    private string GetStatus(int score)
    {
        if (score >= 80) return "Excellent";
        if (score >= 60) return "Good";
        if (score >= 40) return "Fair";
        return "Poor";
    }

    /// <summary>
    /// Provides gender-aware recommendations based on the wellness score.
    /// </summary>
    private string GetRecommendation(int score, string gender)
    {
        if (gender == "Male")
        {
            if (score >= 80) return "Maintain routine; include resistance training 2–3× per week; ensure protein intake across meals.";
            if (score >= 60) return "Improve recovery with an earlier bedtime; add 15 min of light cardio or stretching; keep hydration steady.";
            if (score >= 40) return "Aim for +1 hour of sleep; reduce caffeine after noon; schedule light mobility or an easy walk.";
            return "Rest today; avoid strenuous workouts; focus on hydration and 20–30 min of gentle walking.";
        }
        else // Female
        {
            if (score >= 80) return "Keep strong habits; add yoga/pilates for recovery; prioritize calcium + vitamin D intake.";
            if (score >= 60) return "Boost energy with a balanced breakfast; add 15 min of walking; focus on iron-rich foods if feeling low.";
            if (score >= 40) return "Increase sleep consistency; reduce evening screen time; include calming routines like meditation or journaling.";
            return "Prioritize rest and self-care; consider a short nap if possible; gentle yoga/stretching only.";
        }
    }
}
