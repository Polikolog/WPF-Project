using Kyrsach.Infrastructure;
using Kyrsach.ViewModel.Base;
using System.Windows.Input;
using Kyrsach.View;
using System.Windows;
using System;

namespace Kyrsach.ViewModel
{
    internal class MainWindowView : ViewModelBase
    {
        
        public Action CloseWin { get; set; }

        private void OpenCatalogView()
        {
            
            Catalog catalog = new Catalog();
            catalog.Owner = Application.Current.MainWindow;
            catalog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            catalog.Show();
            
        }

        public MainWindowView()
        {
            #region Команды

            #endregion
        }

        private string _username;
        public string Username
        {
            get => _username;
            set => Set(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        #region Команды

        #region Вход в приложение

        private ICommand _EntryCommand;
        public ICommand EntryCommand
        {
            get => _EntryCommand ?? new ActionCommand(OnEntryCommandExecuted, CanEntryCommandExecute);
        }

        private bool CanEntryCommandExecute(object p) => true;

        private void OnEntryCommandExecuted(object p)
        {
            if (CanEntryCommandExecute(p))
            {
                OpenCatalogView();
            }
        }

        #endregion

        #endregion
    }
}
