using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using static NotABook.StorageController;

namespace NotABook;

public partial class NoteListControl : UserControl
{
    public static ListBox _NotePanel;
    public static List<int> selectionList = new List<int>();
    public static int lastSelectionIndex = -1;
    public static Button deleteButton;
    public NoteListControl()
    {
        InitializeComponent();
        deleteButton = this.FindControl<Button>("DeleteButton");
        _NotePanel = this.FindControl<ListBox>("NotePanel");
        UpdateNoteList();

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

    public static void AddNoteToList(string title, string date)
    {
        NoteListItemControl noteListItem = new NoteListItemControl();
        noteListItem.NoteTitle = title;
        noteListItem.NoteDate = date;

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
            AddNoteToList(note.Name, note.Date.ToString("yyyy-MM-dd"));
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}