using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Controls.Converters;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using NotABook.Controllers;

namespace NotABook;

public partial class NotePage : UserControl
{
    public NotePage()
    {
        InitializeComponent();

        NoteView.saveButton.Click += (sender, e) => {
            createNote();
        };

        NoteList.deleteButton.Click += (sender, e) =>
        {
            deleteNotes();
        };
    }

    /// <summary>
    /// creates a new note
    /// </summary>
    public void createNote()
    {
        string noteTitle = NoteView.noteTitle.Text;
        string noteData = NoteView.noteContent.Text;
        DateTime noteDate = DateTime.Now; //change to selected date instead of 'Now' once calendar is implemented
        int noteID = int.Parse(NoteView.noteID.Content.ToString());
        if (noteID <= 0) noteID = NoteList.FindHighestID() + 1;

        //saves the note to the storage
        //if the app crashes, feel free to comment out for the time being
        StorageController.Note.Create(noteTitle, noteData, noteID, noteDate);

        //adds it to the viewable list
        NoteList.UpdateNoteList();
        // NoteList.AddNoteToList(noteTitle, noteDate.ToString("yyyy-MM-dd"), noteID);

        // Clear the textboxes
        NoteView.noteTitle.Text = "";
        NoteView.noteContent.Text = "";
        NoteView.noteID.Content = "0";
    }


    /// <summary>
    /// Deletes the selected note.
    /// </summary>
    public void deleteNotes()
    {
        // Get the index of selected note.
        List<int> selectedNotes = NoteList.selectedNotes;

        if (selectedNotes.Count == 0)
        {
            NoteView.emptyDelPopup.IsOpen = true;
            var closeEmptyDelButton = NoteView.emptyDelPopup.FindControl<Button>("CloseEmptyDeletionPopupButton");
            closeEmptyDelButton.Click += (sender, e) => { NoteView.emptyDelPopup.IsOpen = false; };
            return;
        }

        if (selectedNotes.Count == 1)
        {
            NoteView.confirmationPopUp.FindControl<TextBlock>("ConfirmationText").Text = "Are you sure you want to delete this note?";
            NoteView.confirmationPopUp.IsOpen = true;
        }

        else if (selectedNotes.Count > 1)
        {
            NoteView.confirmationPopUp.FindControl<TextBlock>("ConfirmationText").Text = "Are you sure you want to delete these notes?";
            NoteView.confirmationPopUp.IsOpen = true;
        }

        // Find buttons for Yes and No
        var yesButton = NoteView.confirmationPopUp.FindControl<Button>("YesButton");
        var noButton = NoteView.confirmationPopUp.FindControl<Button>("NoButton");

        // Add event handlers for buttons
        yesButton.Click += (sender, e) =>
        {
            foreach (int id in NoteList.selectedNotes)
            {
                // Deletes the selected note from the storage.
                StorageController.Note.Delete(id);
            }

            NoteList.UpdateNoteList();

            NoteView.confirmationPopUp.IsOpen = false;

            selectedNotes.Clear();
        };

        noButton.Click += (sender, e) =>
        {
            // Close the popup
            NoteView.confirmationPopUp.IsOpen = false;
        };
    }
}