using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NotABook;

public partial class NoteListControl : UserControl
{
    ListBox _NotePanel;

    public NoteListControl()
    {
        InitializeComponent();
        _NotePanel = this.FindControl<ListBox>("NotePanel");

        for (int i = 0; i < 10; i++)
        {
            AddNoteToList("Example Note Title", "Example Note Date");
        }
    }

    public void AddNoteToList(string title, string date)
    {
        NoteListItemControl noteListItem = new NoteListItemControl();
        noteListItem.NoteTitle = title;
        noteListItem.NoteDate = date;

        _NotePanel.Items.Add(noteListItem);
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