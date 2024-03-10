using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NotABook
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            #if DEBUG
            this.AttachDevTools();
            #endif
        }


    }
}
