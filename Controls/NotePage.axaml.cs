using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Controls.Converters;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;

namespace NotABook;

public partial class NotePage : UserControl
{
    public NotePage()
    {
        InitializeComponent();

        NoteViewControl.saveButton.Click += (sender, e) => {
            createNote();
        };

        NoteListControl.deleteButton.Click += (sender, e) =>
        {
            deleteNotes();
        };
    }

    /// <summary>
    /// creates a new note
    /// </summary>
    public void createNote()
    {
        string noteTitle = NoteViewControl.noteTitle.Text;
        string noteData = NoteViewControl.noteContent.Text;
        DateTime noteDate = DateTime.Now; //change to selected date instead of 'Now' once calendar is implemented
        int noteID = int.Parse(NoteViewControl.noteID.Content.ToString());
        if (noteID <= 0) noteID = NoteListControl.FindHighestID() + 1;

        //saves the note to the storage
        //if the app crashes, feel free to comment out for the time being
        StorageController.Note.Create(noteTitle, noteData, noteID, noteDate);

        //adds it to the viewable list
        NoteListControl.UpdateNoteList();
        // NoteListControl.AddNoteToList(noteTitle, noteDate.ToString("yyyy-MM-dd"), noteID);

        // Clear the textboxes
        NoteViewControl.noteTitle.Text = "";
        NoteViewControl.noteContent.Text = "";
        NoteViewControl.noteID.Content = "0";
    }


    /// <summary>
    /// Deletes the selected note.
    /// </summary>
    public void deleteNotes()
    {
        // Get the index of selected note.
        List<int> selectedNotes = NoteListControl.selectedNotes;

        if (selectedNotes.Count == 0)
        {
            NoteViewControl.emptyDelPopup.IsOpen = true;
            var closeEmptyDelButton = NoteViewControl.emptyDelPopup.FindControl<Button>("CloseEmptyDeletionPopupButton");
            closeEmptyDelButton.Click += (sender, e) => { NoteViewControl.emptyDelPopup.IsOpen = false; };
            return;
        }

        if (selectedNotes.Count == 1)
        {
            NoteViewControl.confirmationPopUp.FindControl<TextBlock>("ConfirmationText").Text = "Are you sure you want to delete this note?";
            NoteViewControl.confirmationPopUp.IsOpen = true;
        }

        else if (selectedNotes.Count > 1)
        {
            NoteViewControl.confirmationPopUp.FindControl<TextBlock>("ConfirmationText").Text = "Are you sure you want to delete these notes?";
            NoteViewControl.confirmationPopUp.IsOpen = true;
        }

        // Find buttons for Yes and No
        var yesButton = NoteViewControl.confirmationPopUp.FindControl<Button>("YesButton");
        var noButton = NoteViewControl.confirmationPopUp.FindControl<Button>("NoButton");

        // Add event handlers for buttons
        yesButton.Click += (sender, e) =>
        {
            foreach (int id in NoteListControl.selectedNotes)
            {
                // Deletes the selected note from the storage.
                StorageController.Note.Delete(id);
            }

            NoteListControl.UpdateNoteList();

            NoteViewControl.confirmationPopUp.IsOpen = false;

            selectedNotes.Clear();
        };

        noButton.Click += (sender, e) =>
        {
            // Close the popup
            NoteViewControl.confirmationPopUp.IsOpen = false;
        };
    }
}