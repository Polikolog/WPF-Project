using Kyrsach_core.Infrastructur;
using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Model;
using Kyrsach_core.View.AdminPage;
using Kyrsach_core.ViewModel.PagesModel.AdminViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kyrsach_core.ViewModel
{
    public class AdminViewModel : ViewModelBase
    {
        private Page SelectedPage = new SelectedPage();
        private UnitOfWork db = new UnitOfWork();

        public AdminViewModel()
        {

            SelectedPage.DataContext = new AdminFurnitureViewModel(this);
            CurrentPage = SelectedPage;
            PreviousPage.AdminPage = SelectedPage;
        }

        #region Свойства
        //Текущая страница
        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set => Set(ref _currentPage, value);
        }
        #endregion

        #region Команды
        //Октрытие страницы товаров
        private ICommand _openFurnitureCommand;
        public ICommand OpenFurnitureCommand
        {
            get => _openFurnitureCommand ?? (_openFurnitureCommand = new ActionCommand( p =>
            {
                SelectedPage.DataContext = new AdminFurnitureViewModel(this);
                CurrentPage = SelectedPage;
                PreviousPage.AdminPage = SelectedPage;
            }));
        }

        private ICommand _openUserCommand;
        public ICommand OpenUserCommand
        {
            get => _openUserCommand ?? (_openUserCommand = new ActionCommand(p =>
            {
                SelectedPage.DataContext = new AdminUserViewModel(this);
                CurrentPage = SelectedPage;
                PreviousPage.AdminPage = SelectedPage;
            }));
        }

        private ICommand _openCommentCommand;
        public ICommand OpenCommentCommand
        {
            get => _openCommentCommand ?? (_openCommentCommand = new ActionCommand(p =>
            {
                SelectedPage.DataContext = new AdminCommentViewModel(this);
                CurrentPage = SelectedPage;
                PreviousPage.AdminPage = SelectedPage;
            }));
        }
        #endregion

    }
}
