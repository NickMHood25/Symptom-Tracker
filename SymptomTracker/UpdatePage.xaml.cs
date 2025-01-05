namespace SymptomTracker;

/// <summary>
/// Update class to change the currently selected symptom in the list
/// </summary>
public partial class UpdatePage : ContentPage
{
    private Symptom currentSymptom;

    public UpdatePage(Symptom symptom)
    {
        InitializeComponent();

        currentSymptom = symptom;

        dateAdded.Date = symptom.Date;
        timeAdded.Time = symptom.Time;
        intensity.SelectedIndex = symptom.Intensity;
        description.Text = symptom.Description;
    }

    /// <summary>
    /// Updates the attributes of a symptom
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Update_Clicked(object sender, EventArgs e)
    {
        currentSymptom.Date = dateAdded.Date;
        currentSymptom.Time = timeAdded.Time;
        currentSymptom.Intensity = (short)intensity.SelectedItem;
        currentSymptom.Description = description.Text;

        DB.updateSymptom(currentSymptom);

        await DisplayAlert("Alert", "Record updated", "Okay");
        await Navigation.PopModalAsync();
    }

    /// <summary>
    /// Cancel will back out and exit from the update page, nothing will be changed when this is clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
