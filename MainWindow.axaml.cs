using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace NotABook
{
    public partial class MainWindow : Window
    {
        private Grid _startGrid;
        private Grid _noteGrid;
        private ListBox _noteList;
        private Button _newNoteButton;

        public MainWindow()
        {
            this.Background = new SolidColorBrush(Color.Parse(ColorPalette.Background(950))); 
            this.Foreground = new SolidColorBrush(Color.Parse(ColorPalette.Text(50)));
            InitializeComponent();

            _startGrid = this.FindControl<Grid>("StartGrid");
            _noteGrid = this.FindControl<Grid>("NoteGrid");
            _noteList = this.FindControl<ListBox>("NoteList");
            _newNoteButton = this.FindControl<Button>("NewNoteButton");
            
            _noteList.Background = new SolidColorBrush(Color.Parse(ColorPalette.Primary(900)));
            _noteList.Foreground = new SolidColorBrush(Color.Parse(ColorPalette.Text(50)));
            _newNoteButton.Background = new SolidColorBrush(Color.Parse(ColorPalette.Accent(500)));
            _newNoteButton.Foreground = new SolidColorBrush(Color.Parse(ColorPalette.Text(50)));

            for (int i = 0; i < 15; i++)
            {
                // Add ListBoxItem with two textBoxes to the ListBox
                var item = new ListBoxItem();
                item.Background = new SolidColorBrush(Color.Parse(ColorPalette.Primary(800)));
                item.Foreground = new SolidColorBrush(Color.Parse(ColorPalette.Text(50)));
                item.Content = new Grid
                {
                    Children =
                    {
                        new TextBlock
                        {
                            Text = "Note #" + i,
                            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left,
                            Background = new SolidColorBrush(Color.Parse(ColorPalette.Primary(800))),
                            Foreground = new SolidColorBrush(Color.Parse(ColorPalette.Text(50))),
                        },
                        new TextBlock
                        {
                            Text = "Date",
                            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
                            Background = new SolidColorBrush(Color.Parse(ColorPalette.Primary(800))),
                            Foreground = new SolidColorBrush(Color.Parse(ColorPalette.Text(50))),
                        }
                    }
                };

                _noteList.Items.Add(item);
            }
        }

        private void Button_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            // Handle button click event
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
