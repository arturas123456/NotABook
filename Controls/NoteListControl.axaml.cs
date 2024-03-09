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
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}