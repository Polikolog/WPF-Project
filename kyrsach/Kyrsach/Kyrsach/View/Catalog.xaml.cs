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
using System.Windows.Shapes;
using System.Windows.Resources;
using Kyrsach.Properties;
using Kyrsach.ViewModel;

namespace Kyrsach.View
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        public Catalog()
        {
            InitializeComponent();
            this.Cursor = new Cursor(@"C:\Users\mi\Desktop\1\OOP\kyrsach\Kyrsach\Kyrsach\images\gem_arrow.cur");
        }
    }
}
