using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Converters;
using Avalonia.Markup.Xaml;
using System;
using static NotABook.StorageController;

namespace NotABook;

public partial class NotePage : UserControl
{
    private Button saveBtn;
    private Button delBtn;
    private Button viewBtn;
    public NotePage()
    {
        InitializeComponent();

        saveBtn = this.FindControl<Button>("SaveButton");
        delBtn = this.FindControl<Button>("DeleteButton");
        viewBtn = this.FindControl<Button>("ViewButton");

        // creates a new note when the "save" button is clicked
        saveBtn.Click += (sender, e) => {
            createNote();
        };

        // deletes the current note when the "delete" button is clicked
        delBtn.Click += (sender, e) =>
        {
            deleteNote();
        };

        viewBtn.Click += (sender, e) =>
        {
            viewNote();
        };
    }

    private void ClosePopup_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        NotePopup.IsOpen = false;
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
        int noteIndex = NoteListControl._SelectedNoteIndex;

        if (noteIndex == -1) return;

        // Deletes the selected note from the storage.
        StorageController.Notes.Delete(noteIndex);

        // Deletes the selected note from the visible list.
        NoteListControl.RemoveNoteFromList(noteIndex);
    }

    /// <summary>
    /// Views the selected note.
    /// </summary>
    public void viewNote()
    {
        //Getting the index of a selected note
        int noteIndex = NoteListControl._SelectedNoteIndex + 1;

        // Retrieve the data of the selected note
        Notes noteData = StorageController.Notes.Get(noteIndex);

        // Update the content of the popup with note information
        if (noteData != null)
        {
            NotePopupText.Text = $"Title: {noteData.Name}\nDate: {noteData.Date.ToString("yyyy-MM-dd")}\nData: {noteData.Data}";
            NotePopup.IsOpen = true;
        }
        else
        {
            NotePopupText.Text = "Note not found";
            NotePopup.IsOpen = true;
        }
    }
}