using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Resources;
using WpfApp1.lib;
using WpfApp1.model;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private string SavePath = @"C:\Users\mi\Desktop\1\OOP\WpfApp1\WpfApp1\db.txt";
        public static PhoneRepository PhonesDB { get; set; } = new PhoneRepository();
        public List<string> Langs = new List<string>() { "ru-RU", "en-US" };
        public List<string> styles = new List<string>() { "white", "dark", "pink" };
        public MainWindow()
        {
            InitializeComponent();
            phonesList.ItemsSource = PhonesDB.GetPhones();
            InitLangsBox();
            InitializeThems();
        }

        private void InitializeThems()
        {
            styleBox.SelectionChanged += ThemeChange;
            styleBox.ItemsSource = styles;
            styleBox.SelectedItem = "dark";
        }

        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            string style = styleBox.SelectedItem as string;
            // определяем путь к файлу ресурсов
            var uri = new Uri("resources/" + style + ".xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        private void InitLangsBox()
        {
            LanguageComboBox.ItemsSource = Langs;
        }

        //-----Event handlers
        private void SetCursor()
        {
            StreamResourceInfo sri = Application.GetResourceStream(new Uri("imgs/pointer.cur", UriKind.Relative));
            Cursor customCursor = new Cursor(sri.Stream);
            this.Cursor = customCursor;
        }
        private void LoadCompanies()
        {
            FilterCompany.ItemsSource = PhonesDB.GetPhones().Select(phone => phone.Company).Distinct();
        }

        private void MainWindow_loaded(object sender, RoutedEventArgs e)
        {
            SetCursor();

            try
            {
                string serializedString = Serializer.ReadFromFile(SavePath);
                var phones = Serializer.Deserialize<List<PhoneProduct>>(serializedString);
                foreach (var phone in phones)
                    PhonesDB.AddPhone(phone);
                PhonesDB.Commit();

                LoadCompanies();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                PhonesDB.Backup();
                string serializedData = Serializer.Serialize(PhonesDB.GetPhones());
                Serializer.SaveToFile(serializedData, SavePath);
            }
            catch (Exception err) { 
                MessageBox.Show(err.Message);
            }
        }

        private void FilterCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PhonesDB.Backup();
            var phonesToDelete = new List<PhoneProduct>();
            if (FilterCompany.SelectedItem == null) return;
            var selectedCompany = FilterCompany.SelectedItem.ToString();
            foreach (var phone in PhonesDB.GetPhones())
            {
                if (phone.Company != selectedCompany)
                    phonesToDelete.Add(phone);
            }
            foreach (var phone in phonesToDelete)
                PhonesDB.DeletePhone(phone);
        }

        private void PriceFilterChange()
        {
            try
            {
                decimal downValue = Convert.ToDecimal(PriceFilterDown.Text);
                decimal upValue = Convert.ToDecimal(PriceFilterUp.Text);
                if (upValue < downValue) throw new Exception();

                PhonesDB.Backup();
                var phonesToDelete = new List<PhoneProduct>();
                foreach (var phone in PhonesDB.GetPhones())
                {
                    if (phone.Price > upValue || phone.Price < downValue)
                        phonesToDelete.Add(phone);
                }
                foreach (var phone in phonesToDelete)
                    PhonesDB.DeletePhone(phone);
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        private void PriceFilterDown_TextChanged(object sender, TextChangedEventArgs e)
        {
            PriceFilterChange();
        }

        private void PriceFilterUp_TextChanged(object sender, TextChangedEventArgs e)
        {
            PriceFilterChange();
        }


        private void ResetFilters_Click(object sender, RoutedEventArgs e)
        {
            PhonesDB.Backup();
            PriceFilterDown.Text = "";
            PriceFilterUp.Text = "";
            FilterCompany.SelectedItem = null;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            PhonesDB.Backup();
            string searchString = SearchField.Text;
            Regex regex = new Regex(searchString, RegexOptions.IgnoreCase);
            var phonesToDelete = new List<PhoneProduct>();
            foreach (var phone in PhonesDB.GetPhones())
            {
                if (!regex.IsMatch(phone.Title))
                    phonesToDelete.Add(phone);
            }
            foreach (var phone in phonesToDelete)
            {
                PhonesDB.DeletePhone(phone);
            }
        }

        private void phonesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (phonesList.SelectedValue != null)
            {
                deletePhoneButton.IsEnabled = true;
                changePhoneButton.IsEnabled = true;
            }
            else
            {
                deletePhoneButton.IsEnabled = false;
                changePhoneButton.IsEnabled = false;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new CreateWindow();
            newWindow.Owner = this;
            newWindow.Show();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPhone = (PhoneProduct)phonesList.SelectedItem;
            var newWindow = new ChangeWindow(selectedPhone.Id);
            newWindow.Owner = this;
            newWindow.Show();
        }

        private void DeleteElementCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var phoneToDelete = (PhoneProduct)phonesList.SelectedItem;
            PhonesDB.DeletePhone(phoneToDelete);
            PhonesDB.Commit();
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.SetLanguageDictionary(this, LanguageComboBox.SelectedItem.ToString());
        }
    }
}