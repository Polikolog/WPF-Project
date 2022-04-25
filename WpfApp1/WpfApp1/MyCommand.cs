using System.Windows.Input;

namespace WpfApp1
{
    internal class MyCommand
    {
        static MyCommand()
        {
            Exit = new RoutedCommand("Exit", typeof(MainWindow));
        }
        public static RoutedCommand Exit { get; set; }
    }

}
