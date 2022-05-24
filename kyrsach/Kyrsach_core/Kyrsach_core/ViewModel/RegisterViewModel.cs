using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Components; 
using Kyrsach_core.Infrastructur;
using System.Windows.Input;
using Kyrsach_core.Model;
using Kyrsach_core.View;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System;

namespace Kyrsach_core.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private UnitOfWork unitOfWork;
        public RegisterViewModel()
        {
        }

        #region Свойства
        public UnitOfWork db { get => unitOfWork; }

        private string _nameTextBox;
        public string NameTextBox
        {
            get => _nameTextBox;
            set => Set(ref _nameTextBox, value);
        }
        private string _nameUser;
        public string NameUser
        {
            get => _nameUser;
            set => Set(ref _nameUser, value);
        }

        private string _userNameReg;
        public string NameUserReg
        {
            get => _userNameReg;
            set => Set(ref _userNameReg, value);
        }

        private string _passwordUserReg;
        public string PasswordUserReg
        {
            get => _passwordUserReg;
            set => Set(ref _passwordUserReg, value);
        }

        private string _passwordUser;
        public string PasswordUser
        {
            get => _passwordUser;
            set => Set(ref _passwordUser, value);
        }

        private string _userAdress;
        public string UserAdress
        {
            get => _userAdress;
            set => Set(ref _userAdress, value);
        }

        private string _userNum;
        public string UserNum
        {
            get => _userNum;
            set => Set(ref _userNum, value);
        }
        #endregion

        #region Команды
        private ICommand _EntryUserCommand;
        public ICommand EntryUserCommand
        {
            get => _EntryUserCommand ?? new ActionCommand(OnEntryUserCommand);
        }

        private void OnEntryUserCommand(object p)
        {
            var rg = p as TextBox;
            try
            {
                if (DataWorker.GetUser(NameUser, PasswordUser))
                {
                    if (CurrentUser.getInstance().IsAdmin)
                    {
                        AdminWindow aw = new AdminWindow();
                        aw.DataContext = new AdminViewModel();
                        App.Current.MainWindow.Close();
                        aw.Show();
                    }
                    else
                    {
                        MainWindow mw = new MainWindow();
                        mw.DataContext = new MainViewModel();
                        App.Current.MainWindow.Close();
                        mw.Show();
                    }
                }
                else
                {
                    RedBorder(ref rg);
                }
            }
            catch(Exception ex)
            {
                RedBorder(ref rg);
                MessageBox.Show(ex.Message);
            }
        }

        private ICommand _registerUserCommand;
        public ICommand RegisterUserCommand
        {
            get => _registerUserCommand ?? new ActionCommand(OnRegisterUserCommand);
        }

        private void OnRegisterUserCommand(object p)
        {
            try
            {
                if (!DataWorker.AddUser(NameUserReg, PasswordUserReg, _userAdress, _userNum))
                {
                    var label = p as Label;
                    if (label != null)
                        label.Content = "Такой пользователь уже существует";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        //Vis
        private void RedBorder(ref TextBox rg)
        {
            rg.BorderBrush = Brushes.Red;
        }
    }
}
