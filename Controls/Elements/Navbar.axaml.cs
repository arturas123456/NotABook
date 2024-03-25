using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using static NotABook.StorageController;

namespace NotABook;

public partial class Navbar : UserControl
{
    public Navbar()
    {
        InitializeComponent();
    }

    private void nav_File_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        button.ContextMenu?.Open(button);
    }

    private void ClearData_Click(object sender, RoutedEventArgs e)
    {
    }

    private async void Export_Click(object sender, RoutedEventArgs e)
    {
        // Ask user where to export data
        var dialog = new SaveFileDialog();
        dialog.InitialFileName = "Notes.json";
        dialog.DefaultExtension = "json";
        dialog.Title = "Export Data";
        dialog.Filters.Add(new FileDialogFilter() { Name = "JSON Files", Extensions = { "json" } });

        var result = await dialog.ShowAsync((Window)this.VisualRoot);
        if (result != null)
        {
            StorageController.ExportData(result);
        }
        else
        {
            Console.WriteLine("Export operation cancelled.");
        }
    }
}