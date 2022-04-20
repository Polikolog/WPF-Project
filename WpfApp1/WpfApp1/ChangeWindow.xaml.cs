using System.Windows;
using System.Windows.Input;
using WpfApp1.model;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ChangeWindow.xaml
    /// </summary>
    public partial class ChangeWindow : Window
    {
        public uint PhoneId { get; set; }
        public ChangeWindow()
        {
            InitializeComponent();
        }
        public ChangeWindow(uint Id) : this()
        {
            InitializeComponent();
            PhoneId = Id;

            InitForm();
        }

        private void InitForm()
        {
            PhoneProduct phone = MainWindow.PhonesDB.GetPhone(PhoneId);
            TitleFiled.Text = phone.Title;
            CompanyFiled.Text = phone.Company;
            DescriptionFiled.Text = phone.Description;
            ImageFiled.Text = phone.Image;
            PriceFiled.Text = phone.Price.ToString();
            RatingFiled.Value = phone.Rating;
        }

        private void ChangeElementCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var canNullablePhone = MainWindow.PhonesDB.GetPhone(PhoneId);
            if (canNullablePhone == null) return;

            var phone = canNullablePhone;
            phone.Title = TitleFiled.Text;
            phone.Company = CompanyFiled.Text;
            phone.Description = DescriptionFiled.Text;
            phone.Image = ImageFiled.Text;
            phone.Price = decimal.Parse(PriceFiled.Text);
            phone.Rating = (float)RatingFiled.Value;

            MainWindow.PhonesDB.Commit();
            this.Close();
        }
    }
}
