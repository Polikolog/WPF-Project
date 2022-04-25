using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        //PropertyMetadata propert = new PropertyMetadata();
        public Prop property = new Prop();
        public UserControl1()
        {
            InitializeComponent();
        }
        string str;
        private void button_Click(object sender, RoutedEventArgs e)
        {
            property.TestData = txtBox1.Text.ToString();
            MessageBox.Show("Строка сохранена");
            txtBox1.Text = String.Empty;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            txtBox2.Text = property.TestData;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            property.ClearValue(Prop.TestDataProperty);
            MessageBox.Show("Значение проперти равно default");
        }

        private void InsertButton_MyButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((sender as Button).Name.ToString());
        }
    }
}
