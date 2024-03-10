using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace NotABook;

public partial class NotePage : UserControl
{
    private Button saveBtn;
    public NotePage()
    {
        InitializeComponent();

        saveBtn = this.FindControl<Button>("SaveButton");

        // creates a new note when the "save" button is clicked
        saveBtn.Click += (sender, e) => {
            createNote();
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
}