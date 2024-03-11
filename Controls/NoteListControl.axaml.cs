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
    public static int _SelectedNoteIndex = -1;
    public NoteListControl()
    {
        InitializeComponent();
        _NotePanel = this.FindControl<ListBox>("NotePanel");
        UpdateNoteList();
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
        noteIndex = _SelectedNoteIndex;
        
        if (noteIndex >= 0 && noteIndex < _NotePanel.ItemCount)
        {
            _NotePanel.Items.RemoveAt(noteIndex);
        }

        else return;
    }

    public void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _SelectedNoteIndex = _NotePanel.SelectedIndex;
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