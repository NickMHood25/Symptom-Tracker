namespace SymptomTracker;

/// <summary>
/// Addpage class to add records
/// </summary>
public partial class AddPage : ContentPage
{
	public AddPage()
	{
		InitializeComponent();
        intensity.SelectedIndex = 0;
	}

    /// <summary>
    /// Add button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Add_Clicked(object sender, EventArgs e)
    {
        Symptom symptom = new Symptom
        {
            Date = dateAdded.Date,
            Time = timeAdded.Time,
            Intensity = intensity.SelectedIndex + 1,
            Description = description.Text
        };

        DB.AddSymptiom(symptom);
        await Navigation.PopModalAsync();
    }

    /// <summary>
    /// Cancel button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}