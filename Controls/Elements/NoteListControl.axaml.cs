using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static NotABook.StorageController;

namespace NotABook;

public partial class NoteListControl : UserControl
{
    public static List<int> selectedNotes = new List<int>();

    public static StackPanel _NotePanel;
    public static int lastSelectionIndex = -1;
    public static Button deleteButton;
    private static Timer backupTimer;
    public NoteListControl()
    {
        InitializeComponent();
        deleteButton = this.FindControl<Button>("DeleteButton");
        _NotePanel = this.FindControl<StackPanel>("NotePanel");
        UpdateNoteList();
        updateButtons();

        backupTimer = new Timer(OnTimerCallback, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
    }

    public static void setNoteListEvents()
    {
        foreach (var item in _NotePanel.Children)
        {
            item.DoubleTapped += (sender, e) =>
            {
                viewNote(sender);
            };

            item.Tapped += (sender, e) =>
            {
                selectNote(sender);
            };
        }
    }

    public static void selectNote(object sender)
    {
        int noteID = int.Parse((sender as NoteListItemControl).Tag.ToString());

        if (selectedNotes.Contains(noteID))
        {
            selectedNotes.Remove(noteID);
            (sender as NoteListItemControl).Classes.Remove("selected");
        }
        else
        {
            selectedNotes.Add(noteID);
            (sender as NoteListItemControl).Classes.Add("selected");
        }

        updateButtons();
    }

    public static void updateButtons()
    {
        if (selectedNotes.Count > 0)
        {
            deleteButton.IsEnabled = true;
        }
        else
        {
            deleteButton.IsEnabled = false;
        }
    }

    public static void viewNote(object sender)
    {
        NoteViewControl.ViewNote(int.Parse((sender as NoteListItemControl).Tag.ToString()));
    }

    private static void OnTimerCallback(object? state)
    {
        StorageController.Note.Backup();
    }

    public static void AddNoteToList(string title, string date, int id)
    {
        NoteListItemControl noteListItem = new NoteListItemControl();
        noteListItem.NoteTitle = title;
        noteListItem.NoteDate = date;
        noteListItem.Tag = id;

        _NotePanel.Children.Add(noteListItem);
    }

    public static void RemoveNoteFromList(int NoteID)
    {
        if (NoteID >= 0 && NoteID < _NotePanel.Children.Count)
        {
            _NotePanel.Children.Remove(_NotePanel.Children.Where(x => x.Tag.ToString() == NoteID.ToString()).FirstOrDefault());
        }

        else return;
    }

    public static void ClearNoteList()
    {
        _NotePanel.Children.Clear();
    }

    public static void UpdateNoteList()
    {
        ClearNoteList();
        List<Note> notes = StorageController.Note.LoadNotes();
        foreach (var note in notes)
        {
            AddNoteToList(note.Name, note.Date.ToString("yyyy-MM-dd"), note.Id);
        }

        setNoteListEvents();
    }

    public static int FindHighestID()
    {
        int highestID = 0;
        foreach (NoteListItemControl note in _NotePanel.Children)
        {
            if (int.Parse(note.Tag.ToString()) > highestID)
            {
                highestID = int.Parse(note.Tag.ToString());
            }
        }
        return highestID;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}