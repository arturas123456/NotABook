using System;
using Avalonia.Input;
using Microsoft.Win32;

namespace NotABook
{
    internal class StartupControl
    {
        bool enabled = false;
        
        private void SetStartup(bool enabled)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (enabled) key.SetValue("NotABook", System.Reflection.Assembly.GetExecutingAssembly().Location);
            else key.DeleteValue("NotABook", false);
        }
    }
}
