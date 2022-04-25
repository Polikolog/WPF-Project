using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Components; 
using Kyrsach_core.Infrastructur;
using System.Windows.Input;
using Kyrsach_core.Model;
using Kyrsach_core.View;
using System.Windows;
using System.Windows.Media;

namespace Kyrsach_core.ViewModel
{
    internal class RegisterViewModel : ViewModelBase
    {
        private string _nameUser;
        public string NameUser
        {
            get => _nameUser;
            set => Set(ref _nameUser, value);
        }

        private string _passwordUser;
        public string PasswordUser
        {
            get => _passwordUser;
            set => Set(ref _passwordUser, Passwords.);
        }

        private ICommand _EntryUserCommand;
        public ICommand EntryUserCommand
        {
            get => _EntryUserCommand ?? new ActionCommand(OnEntryUserCommand);
        }

        private void OnEntryUserCommand(object p)
        {
            Register rg = p as Register;
            if(DataWorker.GetUser(_nameUser, _passwordUser))
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                Application.Current.MainWindow.Hide();

            }
            else
            {
                RedBorder(rg); 
            }
        }

        private ICommand _RegisterUserCommand;
        public ICommand RegisterUserCommand
        {
            get => _RegisterUserCommand ?? new ActionCommand(OnRegisterUserCommand);
        }

        private void OnRegisterUserCommand(object p)
        {
            DataWorker.AddUser();
        }


        //Vis
        private void RedBorder(Register rg)
        {
            rg.Login.BorderBrush = Brushes.Red; 
        }
    }
}
