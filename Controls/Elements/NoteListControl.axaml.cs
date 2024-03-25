using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Threading;
using static NotABook.StorageController;

namespace NotABook;

public partial class NoteListControl : UserControl
{
    public static ListBox _NotePanel;
    public static List<int> selectionList = new List<int>();
    public static int lastSelectionIndex = -1;
    public static Button deleteButton;
    private static Timer backupTimer;
    public NoteListControl()
    {
        InitializeComponent();
        deleteButton = this.FindControl<Button>("DeleteButton");
        _NotePanel = this.FindControl<ListBox>("NotePanel");
        UpdateNoteList();

        backupTimer = new Timer(OnTimerCallback, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));

        _NotePanel.DoubleTapped += (sender, e) =>
        {
            viewNote();
        };

        _NotePanel.SelectionChanged += (sender, e) =>
        {
            updateSelection(sender, e);
        };
    }

    public static void viewNote()
    {
    }

    private static void OnTimerCallback(object? state)
    {
        StorageController.Notes.Backup();
    }

    public static void AddNoteToList(string title, string date, int id)
    {
        NoteListItemControl noteListItem = new NoteListItemControl();
        noteListItem.NoteTitle = title;
        noteListItem.NoteDate = date;
        noteListItem.Tag = id;

        _NotePanel.Items.Add(noteListItem);
    }

    public static void RemoveNoteFromList(int noteIndex)
    {
        noteIndex = lastSelectionIndex;

        if (noteIndex >= 0 && noteIndex < _NotePanel.ItemCount)
        {
            _NotePanel.Items.RemoveAt(noteIndex);
        }

        else return;
    }

    public void updateSelection(object sender, SelectionChangedEventArgs e)
    {
        lastSelectionIndex = _NotePanel.SelectedIndex;
        if (SidebarControl.isMultiple.IsChecked == true)
        {
            if (selectionList.IndexOf(lastSelectionIndex) == -1) selectionList.Add(lastSelectionIndex);
            else selectionList.Remove(lastSelectionIndex);
        }
        else
        {
            if (_NotePanel.SelectedItems.Count > 1) _NotePanel.UnselectAll();
            if (selectionList.IndexOf(lastSelectionIndex) == -1) selectionList.Add(lastSelectionIndex);
            else selectionList.Remove(lastSelectionIndex);
        }
    }

    public void ClearNoteList()
    {
        _NotePanel.Items.Clear();
    }

    public void UpdateNoteList()
    {
        ClearNoteList();
        List<Notes> notes = StorageController.Notes.LoadNotes();
        foreach (var note in notes)
        {
            AddNoteToList(note.Name, note.Date.ToString("yyyy-MM-dd"), note.Id);
        }
    }

    public static int FindHighestID()
    {
        int highestID = 0;
        foreach (NoteListItemControl note in _NotePanel.Items)
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