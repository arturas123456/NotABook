using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Converters;
using Avalonia.Markup.Xaml;
using System;

namespace NotABook;

public partial class NotePage : UserControl
{
    private Button saveBtn;
    private Button delBtn;
    public NotePage()
    {
        InitializeComponent();

        saveBtn = this.FindControl<Button>("SaveButton");
        delBtn = this.FindControl<Button>("DeleteButton");

        // creates a new note when the "save" button is clicked
        saveBtn.Click += (sender, e) => {
            createNote();
        };

        // deletes the current note when the "delete" button is clicked
        delBtn.Click += (sender, e) =>
        {
            deleteNote();
        };
    }

    /// <summary>
    /// creates a new note
    /// </summary>
    public void createNote()
    {
        string noteTitle = NoteTitle.Text;
        string noteData = NoteText.Text;
        DateTime noteDate = DateTime.Now; //change to selected date instead of 'Now' once calendar is implemented

        //saves the note to the storage
        //if the app crashes, feel free to comment out for the time being
        StorageController.Notes.Create(noteTitle, noteData, noteDate);

        //adds it to the viewable list
        NoteListControl.AddNoteToList(noteTitle, noteDate.ToString("yyyy-MM-dd"));
    }


    /// <summary>
    /// Deletes the selected note.
    /// </summary>
    public void deleteNote()
    {
        // Get ID of selected note.
        int noteID = NoteListControl._SelectedNoteID;

        // Deletes the selected note from the storage.
        StorageController.Notes.Delete(noteID);

        // Deletes the selected note from the visible list.
        NoteListControl.RemoveNoteFromList(noteID);
    }
}