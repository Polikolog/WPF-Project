using Kyrsach_core.Infrastructur;
using Kyrsach_core.Infrastructur.Entity;
using Kyrsach_core.Model;
using Kyrsach_core.View;
using Kyrsach_core.View.Other;
using Kyrsach_core.ViewModel.Other;
using Kyrsach_core.ViewModel.PagesModel.Base;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kyrsach_core.ViewModel.PagesModel
{
    public class ProfileViewModel : BasePageViewModel
    {
        private MainViewModel mainViewModel = new MainViewModel();
        private Window mainwin;
        private readonly OpenFileDialog _openFileDialog = new OpenFileDialog();

        public ProfileViewModel(MainViewModel mainViewModel, ref MainWindow win)
        {
            mainwin = win;

            this.mainViewModel = mainViewModel;
            Image = CurrentUser.getInstance().Image;
            Name = CurrentUser.getInstance().Name;
            Adress = CurrentUser.getInstance().Adress;
            Phone = CurrentUser.getInstance().Phone;

            ViewedFurnitureList = CurrentUser.CheckFurniture;
        }

        #region Свойства
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

        private string _phone;
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        private ObservableCollection<Furniture> _viewedFurnitureList;
        public ObservableCollection<Furniture> ViewedFurnitureList
        {
            get => _viewedFurnitureList;
            set => Set(ref _viewedFurnitureList, value);
        }

        #endregion

        #region Команды
        private ICommand _changeImageCommand;
        public ICommand ChangeImageCommand
        {
            get => _changeImageCommand ?? new ActionCommand(p =>
            {
                try
                {
                    _openFileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)" +
                                     "|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
                    _openFileDialog.ShowDialog();
                    Image = _openFileDialog.FileName;
                    DataWorker.ChangedImageUser(Image);
                    CurrentUser.getInstance().Image = Image;
                }
                catch
                { }
            });
        }

        private ICommand _changedUserCommand;
        public ICommand ChangedUserCommand
        {
            get => _changedUserCommand ?? (_changedUserCommand = new ActionCommand(p => 
            {
                DataWorker.ChangedUserParametrs(Name, Adress, Phone);
            }));
        }

        private ICommand _exitCommand;
        public ICommand ExitCommand
        {
            get => _exitCommand ?? (_exitCommand = new ActionCommand(p =>
            {
                CurrentUser.setInstance(null);
                Window Register = new Register();
                Register.Show();
                mainwin.Close();
            }));
        }
        #endregion
    }
}
