namespace NotesList.Views;

public partial class AllNotesPage : ContentPage
{
	public AllNotesPage()
	{
		InitializeComponent();

		BindingContext = new Models.AllNotes();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		((Models.AllNotes)BindingContext).LoadAllNotes();
    }

	protected async void Add_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(NotePage));
	}

	protected async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if(e.CurrentSelection.Count != 0)
		{
			//Get the note model
			var note = (Models.Note)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(NotePage)}?{nameof(NotePage.ItemId)}={note.FileName}");

			//Unselect the UI
			notesCollection.SelectedItem = null;
        }
    }
}