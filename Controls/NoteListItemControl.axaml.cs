using Avalonia;
using Avalonia.Controls.Primitives;

namespace NotABook;

public class NoteListItemControl : TemplatedControl
{

    public static StyledProperty<string> NoteTitleProperty = AvaloniaProperty.Register<NoteListItemControl, string>(nameof(NoteTitle), "Example Note Title");
    public string NoteTitle
    {
        get => GetValue(NoteTitleProperty);
        set => SetValue(NoteTitleProperty, value);
    }

    public static StyledProperty<string> NoteDateProperty = AvaloniaProperty.Register<NoteListItemControl, string>(nameof(NoteDate), "Example Note Date");
    public string NoteDate
    {
        get => GetValue(NoteDateProperty);
        set => SetValue(NoteDateProperty, value);
    }
}