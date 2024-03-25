using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace NotABook;

public partial class Navbar : UserControl
{
    public Navbar()
    {
        InitializeComponent();
    }

    private void nav_File_Click(object sender, RoutedEventArgs e)
    {
        // Handle button click event
        // For example, show the context menu
        var button = (Button)sender;
        button.ContextMenu?.Open(button);
    }

    private void ClearData_Click(object sender, RoutedEventArgs e)
    {
        // Handle Clear Data option click
        // For example, clear data
    }

    private void Export_Click(object sender, RoutedEventArgs e)
    {
        // Handle Export option click
        // For example, export data
    }
}