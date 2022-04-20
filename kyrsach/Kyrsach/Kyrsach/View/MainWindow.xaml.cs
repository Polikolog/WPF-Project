using System;
using System.Windows;
using Kyrsach.ViewModel;

namespace Kyrsach.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowView mv = new MainWindowView();
            this.DataContext = mv;
            if (mv.CloseWin == null)
                mv.CloseWin = new Action(Close);
        }
    }
}
