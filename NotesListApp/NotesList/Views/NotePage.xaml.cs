namespace NotesList.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{

    string _fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");

    public string ItemId {
        set { LoadNote(value); }
    }

    public NotePage()
    {
        InitializeComponent();

        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

        LoadNote(Path.Combine(appDataPath, randomFileName));
    }

    private void LoadNote(string fileName)
    {
        Models.Note noteModel = new Models.Note();
        noteModel.FileName = fileName;

        if(File.Exists(fileName)) {
            noteModel.Text = File.ReadAllText(fileName);
            noteModel.Date = File.GetCreationTime(fileName);
        }

        BindingContext = noteModel;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
        {
            File.WriteAllText(note.FileName ??"", textEditor.Text);
        }
        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
        {
            //Delete the file
            if (File.Exists(note.FileName))
            {
                File.Delete(note.FileName);
            }
        }

        textEditor.Text = string.Empty;
        await Shell.Current.GoToAsync("..");
    }
}