using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
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

        NoteListControl.deleteButton.Click += (sender, e) =>
        {
            deleteNote();
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
        int noteID = NoteListControl.FindHighestID() + 1;

        //saves the note to the storage
        //if the app crashes, feel free to comment out for the time being
        StorageController.Notes.Create(noteTitle, noteData, noteID, noteDate);

        //adds it to the viewable list
        NoteListControl.AddNoteToList(noteTitle, noteDate.ToString("yyyy-MM-dd"), noteID);
    }


    /// <summary>
    /// Deletes the selected note.
    /// </summary>
    public void deleteNote()
    {
        // Get the index of selected note.
        int noteIndex = NoteListControl.lastSelectionIndex;

        if (noteIndex == -1)
        {
            NoteViewControl.confirmationPopUp.FindControl<TextBlock>("NotePopupText").Text = "Please select a note to delete.";
            NoteViewControl.notePopup.IsOpen = true;
            return;
        }

        // Update the content of the popup with confirmation message
        NoteViewControl.confirmationPopUp.FindControl<TextBlock>("NotePopupText").Text = "Are you sure you want to delete this note?";
        NoteViewControl.confirmationPopUp.IsOpen = true;

        // Find buttons for Yes and No
        var yesButton = NoteViewControl.confirmationPopUp.FindControl<Button>("YesButton");
        var noButton = NoteViewControl.confirmationPopUp.FindControl<Button>("NoButton");

        // Add event handlers for buttons
        yesButton.Click += (sender, e) =>
        {
            // Deletes the selected note from the storage.
            StorageController.Notes.Delete(noteIndex);

            // Deletes the selected note from the visible list.
            NoteListControl.RemoveNoteFromList(noteIndex);

            // Close the popup
            NoteViewControl.confirmationPopUp.IsOpen = false;
        };

        noButton.Click += (sender, e) =>
        {
            // Close the popup
            NoteViewControl.confirmationPopUp.IsOpen = false;
        };
    }
    public void deleteNote1()
    {
        // Get the index of selected note.
        int noteIndex = NoteListControl.lastSelectionIndex;

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
        int noteIndex = NoteListControl.lastSelectionIndex + 1;

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