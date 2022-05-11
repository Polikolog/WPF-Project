using Kyrsach_core.Infrastructur;
using Kyrsach_core.Model;
using Kyrsach_core.View.Other;
using Kyrsach_core.ViewModel.Other;
using Kyrsach_core.ViewModel.PagesModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kyrsach_core.ViewModel.PagesModel
{
    public class ProfileViewModel : BasePageViewModel
    {
        private QuestionWindow QWindow = new QuestionWindow();
        private MainViewModel mainViewModel = new MainViewModel();

        public ProfileViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            Image = CurrentUser.getInstance().Image;
            Name = CurrentUser.getInstance().Name;
            Adress = CurrentUser.getInstance().Adress;
            Phone = CurrentUser.getInstance().Phone;
        }

        private string _image;
        public string Image
        {
            get => _image;
            set => Set(ref _image, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _adress;
        public string Adress
        {
            get => _adress;
            set => Set(ref _adress, value);
        }

        private int? _phone;
        public int? Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }


        private ICommand _changeImage;
        public ICommand ChangeImage
        {
            get => _changeImage ?? new ActionCommand(p =>
            {
                var but = p as Button;
                QWindow.DataContext = new QuestionViewModel(this);
                QWindow.Show();
            });
        }

    }
}
