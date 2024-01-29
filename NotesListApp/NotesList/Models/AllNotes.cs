using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesList.Models
{
    internal class AllNotes
    {
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        public AllNotes() => LoadAllNotes();

        public void LoadAllNotes()
        {
            Notes.Clear();

            //Get the folder path where notes are stored
            string appDataPath = FileSystem.AppDataDirectory;

            IEnumerable<Note> notes = Directory

                                       // select the file names from the direcotry
                                       .EnumerateFiles(appDataPath, "*.notes.txt")

                                       //Each file name is used to created a new Note
                                       .Select(filename => new Note()
                                       {
                                           FileName = filename,
                                           Text = File.ReadAllText(filename),
                                           Date = File.GetCreationTime(filename)
                                       })
                                       .OrderBy(note => note.Date);

            //Add each note into the Observable collection.
            foreach (Note note in notes)
            {
                Notes.Add(note);
            }
        }
    }
}

