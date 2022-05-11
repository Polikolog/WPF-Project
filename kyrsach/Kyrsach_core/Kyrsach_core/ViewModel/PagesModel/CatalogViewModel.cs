using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Model;
using Kyrsach_core.View.Pages;
using Kyrsach_core.ViewModel.PagesModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Kyrsach_core.ViewModel.PagesModel
{
    public class CatalogViewModel : BasePageViewModel
    {
        public CatalogViewModel(string type, MainViewModel page)
        {
            MainViewModel = page;

            SelectedFurnitureCatalog = type;

            CountFurniture = DataWorker.CountFurniturСertainType(type.Substring(0, type.Length - 1)).ToString() + " модели";

            FurnitureList = DataWorker.GetFurniturе(type.Substring(0, type.Length - 1));
        }

        public CatalogViewModel(string type, ObservableCollection<Furniture> list, MainViewModel main)
        {
            MainViewModel = main;
            SelectedFurnitureCatalog = type;
            CountFurniture = list.Count.ToString() + " найдено совпадений"; 
            FurnitureList = list;
        }

        #region Свойства
        //Выбранный каталог товаров
        private string? _selectedFurnitureCatalog;
        public string? SelectedFurnitureCatalog
        {
            get => _selectedFurnitureCatalog;
            set => Set(ref _selectedFurnitureCatalog, value);
        }

        //Количество товаров в каталоге
        private string? _countFurniture;
        public string? CountFurniture
        {
            get => _countFurniture;
            set => Set(ref _countFurniture, value);
        }
        #endregion
    }
}
