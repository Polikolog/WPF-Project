using Kyrsach.Infrastructure;
using Kyrsach.ViewModel.Base;
using System.Windows.Input;
using System.Windows;

namespace Kyrsach.ViewModel
{
    internal class CatalogViewModel : ViewModelBase
    {
        public CatalogViewModel()
        {
            

            #region Команды

            MenuMainCommand = new ActionCommand(OnMenuMainCommandExecuted, CanMenuMainCommandExecute);

            #endregion


        }

        #region Заголовок окна

        /// <summary>Заголовок Окна </summary>

        private string _Title = "МолоМебель";

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Команды

        #region Кнопка раскрития меню
        public ICommand MenuMainCommand { get; }

        private bool CanMenuMainCommandExecute(object p) => true;

        private void OnMenuMainCommandExecuted(object p)
        {

        }
        #endregion

        #endregion

    }
}
