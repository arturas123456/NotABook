using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NotABook;

public partial class NoteListControl : UserControl
{
    public static ListBox _NotePanel;
    public static int _SelectedNoteID = -1;
    public NoteListControl()
    {
        InitializeComponent();
        _NotePanel = this.FindControl<ListBox>("NotePanel");
    }

    public static void AddNoteToList(string title, string date)
    {
        NoteListItemControl noteListItem = new NoteListItemControl();
        noteListItem.NoteTitle = title;
        noteListItem.NoteDate = date;

        _NotePanel.Items.Add(noteListItem);
    }

    public static void RemoveNoteFromList(int noteID)
    {
        noteID = _SelectedNoteID;
        
        if (noteID >= 0 && noteID < _NotePanel.ItemCount)
        {
            _NotePanel.Items.RemoveAt(noteID);
        }

        else return;
    }

    public void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _SelectedNoteID = _NotePanel.SelectedIndex;
    }

    public void ClearNoteList()
    {
        _NotePanel.Items.Clear();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}