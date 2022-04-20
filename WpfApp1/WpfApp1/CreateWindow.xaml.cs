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
using WpfApp1.lib;
using WpfApp1.model;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для CreateWindow.xaml
    /// </summary>
    public partial class CreateWindow : Window
    {
        public CreateWindow()
        {
            InitializeComponent();
        }

        private void NewElementCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var newPhone = new PhoneProduct();
            var phones = MainWindow.PhonesDB.GetPhones();
            uint maxId = 0;
            if (phones.Count > 0)
                maxId = phones.Select(phone => phone.Id).Max();

            newPhone.Id = maxId+1;
            newPhone.Title = TitleFiled.Text;
            newPhone.Company = CompanyFiled.Text;
            newPhone.Description = DescriptionFiled.Text;
            newPhone.Image = ImageFiled.Text;
            newPhone.Price = decimal.Parse(PriceFiled.Text);
            newPhone.Rating = (float)RatingFiled.Value;

            MainWindow.PhonesDB.AddPhone(newPhone);
            MainWindow.PhonesDB.Commit();
            this.Close();
        }
    }
}
