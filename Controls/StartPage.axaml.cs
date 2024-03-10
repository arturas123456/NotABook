using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NotABook;

public partial class StartPage : UserControl
{
    private Button _btn;

    public StartPage()
    {
        InitializeComponent();

        _btn = this.FindControl<Button>("NewNoteButton");

        // adds a new note to the list when the button is clicked
        _btn.Click += (sender, e) => { 
            NoteListControl.AddNoteToList("Titlel", "Datel");
        };

    }
}