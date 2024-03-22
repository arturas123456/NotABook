using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Converters;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using System;
using static NotABook.StorageController;

namespace NotABook;

public partial class NotePage : UserControl
{
    public NotePage()
    {
        InitializeComponent();

        NoteViewControl.saveButton.Click += (sender, e) => {
            createNote();
        };
        
        NoteViewControl.viewButton.Click += (sender, e) =>
        {
            viewNote();
        };
    }

    private void ClosePopup_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        NoteViewControl.notePopup.IsOpen = false;
    }

    /// <summary>
    /// creates a new note
    /// </summary>
    public void createNote()
    {
        string noteTitle = NoteViewControl.noteTitle.Text;
        string noteData = NoteViewControl.noteContent.Text;
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
        // Get the index of selected note.
        int noteIndex = NoteListControl._SelectedNoteIndex;

        if (noteIndex == -1)
        {
            NoteViewControl.notePopup.FindControl<TextBlock>("NotePopupText").Text = "Please select a note to delete.";
            NoteViewControl.notePopup.IsOpen = true;
            return;
        }
        
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
            NoteViewControl.notePopup.FindControl<TextBlock>("NotePopupText").Text = $"Title: {noteData.Name}\nDate: {noteData.Date.ToString("yyyy-MM-dd")}\nData: {noteData.Data}";
            NoteViewControl.notePopup.IsOpen = true;
        }
        else
        {
            NoteViewControl.notePopup.FindControl<TextBlock>("NotePopupText").Text = "Note not found";
            NoteViewControl.notePopup.IsOpen = true;
        }
    }
}