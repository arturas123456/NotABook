using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;

namespace NotABook;

public partial class NoteViewControl : UserControl
{
    public static TextBox noteTitle;
    public static TextBox noteContent;

    public static Button saveButton;
    public static Button viewButton;

    public static Popup notePopup;

    public NoteViewControl()
    {
        InitializeComponent();

        noteTitle = this.FindControl<TextBox>("NoteTitle");
        noteContent = this.FindControl<TextBox>("NoteContent");

        saveButton = this.FindControl<Button>("SaveButton");
        viewButton = this.FindControl<Button>("ViewButton");

        notePopup = this.FindControl<Popup>("NotePopup");
    }
}