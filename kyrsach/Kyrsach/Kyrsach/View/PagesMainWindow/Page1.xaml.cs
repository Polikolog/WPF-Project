using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kyrsach.View.PagesMainWindow
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        private void ScrollViewer_Main(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
                scrollviewer.LineUp();
            else
                scrollviewer.LineDown();
            e.Handled = true;
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
                scrollviewer.LineLeft();
            else
                scrollviewer.LineRight();
            e.Handled = true;
        }

        #region Скролы при помощи стрелок
        private void BLeft(object sender, RoutedEventArgs e)
        {
            ScrollCollec.LineLeft();
        }

        private void BRight(object sender, RoutedEventArgs e)
        {
            ScrollCollec.LineRight();
        }

        private void BLeft2(object sender, RoutedEventArgs e)
        {
            WhyScroll.LineLeft();
        }

        private void BRight2(object sender, RoutedEventArgs e)
        {
            WhyScroll.LineRight();
        }
        #endregion

        private void ListButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
