using Kyrsach_core.Model;
using Kyrsach_core.View;
using ShowMeTheXAML;
using System.Windows;

namespace Kyrsach_core
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            XamlDisplay.Init();
            base.OnStartup(e);
        }
    }
}
