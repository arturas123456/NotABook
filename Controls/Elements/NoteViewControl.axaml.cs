using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using static NotABook.StorageController;

namespace NotABook;

public partial class NoteViewControl : UserControl
{
    public static TextBox noteTitle;
    public static TextBox noteContent;
    public static Label noteID;

    public static Button saveButton;
    public static Button viewButton;

    public static Button yesButton;
    public static Button noButton;

    public static Popup notePopup;
    public static Popup confirmationPopUp;


    public NoteViewControl()
    {
        InitializeComponent();

        noteTitle = this.FindControl<TextBox>("NoteTitle");
        noteContent = this.FindControl<TextBox>("NoteText");
        noteID = this.FindControl<Label>("NoteID");

        saveButton = this.FindControl<Button>("SaveButton");
        viewButton = this.FindControl<Button>("ViewButton");

        yesButton = this.FindControl<Button>("YesButton");
        noButton = this.FindControl<Button>("NoButton");

        notePopup = this.FindControl<Popup>("NotePopup");
        confirmationPopUp = this.FindControl<Popup>("ConfirmationPopup");

    }

    public static void ViewNote(int NoteID)
    {
        Note note = Note.Get(NoteID);
        noteTitle.Text = note.Name;
        noteContent.Text = note.Data;
        noteID.Content = NoteID.ToString();
    }
}