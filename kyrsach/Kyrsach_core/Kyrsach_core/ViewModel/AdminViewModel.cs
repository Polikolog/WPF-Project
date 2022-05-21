using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Model;
using Kyrsach_core.View.AdminPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Kyrsach_core.ViewModel
{
    public class AdminViewModel : ViewModelBase
    {
        private Page FurnituresPage = new FurnituresPage();

        public AdminViewModel()
        {

            FurnituresPage.DataContext = this;
            CurrentPage = FurnituresPage;

            //FurnitureList = DataWorker.GetFurniturе();
        }

        private ObservableCollection<Furniture> _furnitureList;
        public ObservableCollection<Furniture> FurnitureList
        {
            get => _furnitureList;
            set => Set(ref _furnitureList, value);
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
        #endregion

    }
}
