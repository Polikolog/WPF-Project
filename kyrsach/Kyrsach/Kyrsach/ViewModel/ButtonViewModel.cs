using Kyrsach.Infrastructure;
using Kyrsach.Model;
using Kyrsach.ViewModel.Base;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Kyrsach.View.PagesMainWindow.PagesForMainPage;

namespace Kyrsach.ViewModel
{
    internal class ButtonViewModel : ViewModelBase
    {
        public ButtonViewModel()
        {
            NewButton = new PageNewButton();
            TypeForRoom = new TypeForRoom();
            ForTypeProduct = new ForTypeProduct();

            CurrentPage = TypeForRoom;
        }

        private Page NewButton;
        private Page TypeForRoom;
        private Page ForTypeProduct;

        private LabelButtonMainPage _mainPage;

        private Page _CurrentPage;
        public Page CurrentPage
        {
            get => _CurrentPage;
            set => Set(ref _CurrentPage, value);
        }

        public ICommand NewButtonPageCommand
        {
            get => new ActionCommand(OnNewButtonCommand);
        }

        public void OnNewButtonCommand (object p)
        {
            CurrentPage = NewButton;
        }

        public ICommand TypeForRoomCommand
        {
            get => new ActionCommand(OnTypeForRoomCommand);
        }

        public void OnTypeForRoomCommand(object p)
        {
            CurrentPage = TypeForRoom;
        }

        public ICommand ForTypeProductCommand
        {
            get => new ActionCommand(OnForTypeProductCommand);
        }

        public void OnForTypeProductCommand(object p)
        {
            CurrentPage = ForTypeProduct;
        }
    }
}
