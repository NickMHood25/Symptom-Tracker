/**
 * Name: Nick Hood
 * Date: October 27, 2023
 * Section: CSE 382 A
 * Description: App keeps track of a list of symptoms the user enters. They can add, delete, or modify any of the symptoms
 * in the list and they can sort it by either indensity (descending) or by date (standard time). If a user clicks on a 
 * symptom, several pop-ups will appear whether if they want to delete, modifiy the symptom, or hit cancel and back out of
 * the pop-up. Everything to my knowledge seems to be working as intended
 */


namespace SymptomTracker;

/// <summary>
/// Main page class to display, sort, and modify the list
/// </summary>
public partial class MainPage : ContentPage {

	public MainPage() {
		InitializeComponent();
	}

    AddPage addPage = new AddPage();

    /// <summary>
    /// Adds entry
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void addEntry_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(addPage, true);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var symptoms = DB.GetAllSymptoms();
        symptomsListView.ItemsSource = symptoms;
    }

    /// <summary>
    /// Display options to delete, modify, or cancel when selecting an entry
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void symptomsListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        string result1 = await DisplayActionSheet("Operation", "Cancel", null, "Modify", "Delete");

        if (result1 == "Delete")
        {
            bool result2 = await DisplayAlert("Warning", "Do you wish to delete this record?", "Yes", "No");
            if (result2 == true)
            {
                Symptom symptom = symptomsListView.SelectedItem as Symptom;
                DB.conn.Delete(symptom);
                symptomsListView.SelectedItem = null;
                await DisplayAlert("Alert", "Record deleted", "Okay");

                var symptoms = DB.GetAllSymptoms();
                symptomsListView.ItemsSource = symptoms;

            }
        }
        else if (result1 == "Modify")
        {
            Symptom symptom = symptomsListView.SelectedItem as Symptom;
            updateRecord(symptom);
        }
    }


    /// <summary>
    /// Update current record
    /// </summary>
    /// <param name="symptom"></param>
    private async void updateRecord(Symptom symptom)
    {
        UpdatePage updatePage = new UpdatePage(symptom);
        await Navigation.PushModalAsync(updatePage, true);
    }

    /// <summary>
    /// Sorts list by date
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void sortByDate_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var symptoms = DB.GetAllSymptoms();
        symptomsListView.ItemsSource = symptoms.OrderBy(s => s.Date);
    }

    /// <summary>
    /// Sorts list by intensity in descending order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void sortByIntensity_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var symptoms = DB.GetAllSymptoms();
        symptomsListView.ItemsSource = symptoms.OrderByDescending(s => s.Intensity);
    }
}

