using Kyrsach_core.Infrastructur;
using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Kyrsach_core.ViewModel.PagesModel.AdminViewModel.Base
{
    public class BaseAdminViewModel<T> : ViewModelBase
    {
        protected string _currentAction;
        protected UnitOfWork db;
        protected ObservableCollection<T> List = new ObservableCollection<T>();
        public ViewModel.AdminViewModel AdminViewModel;
        public BaseAdminViewModel(ViewModel.AdminViewModel adm)
        {
            AdminViewModel = adm;
            db = new UnitOfWork();
        }

        #region Свойства
        private ObservableCollection<T> _selectedList;
        public ObservableCollection<T> SelectedList
        {
            get => _selectedList;
            set => Set(ref _selectedList, value);
        }

        private T _selectedItem;
        public T SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }
        #endregion

        #region Команды
        private ICommand _backPageCommand;
        public ICommand BackPageCommand
        {
            get => _backPageCommand ?? (_backPageCommand = new ActionCommand(p =>
            {
                AdminViewModel.CurrentPage = PreviousPage.AdminPage;
            }));
        }
        #endregion
    }
}
