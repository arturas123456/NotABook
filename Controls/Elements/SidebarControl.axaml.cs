using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace NotABook;

public partial class SidebarControl : UserControl
{
    public static CheckBox isMultiple;
    public SidebarControl()
    {
        InitializeComponent();

        isMultiple = this.FindControl<CheckBox>("IsMultiple");
    }
}