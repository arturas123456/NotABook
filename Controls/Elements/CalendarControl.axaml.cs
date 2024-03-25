using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace NotABook;

public partial class CalendarControl : UserControl
{
    public CalendarControl()
    {
        InitializeComponent();
        CalendarDate.Text = DateTime.Now.ToString("yyy-MM-dd");
    }
}