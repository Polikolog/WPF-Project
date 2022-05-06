using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Model;
using Kyrsach_core.View.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Kyrsach_core.ViewModel.PagesModel
{
    public class FurniturePageViewModal : ViewModelBase
    {
        private Page FurniturePage = new FurniturePage();
        private MainViewModel mainViewModel;
        public FurniturePageViewModal(string type, MainViewModel page)
        {
            FurniturePage.DataContext = this;
            mainViewModel = page;
            SelectedFurniturCatalog = type;
            CountFurnitur = DataWorker.CountFurniturСertainType(type.Substring(0, type.Length - 1)).ToString() + " модели";
            FurnitureList = DataWorker.GetFurniturе(type.Substring(0, type.Length - 1));
        }

        private string _selectedFurniturCatalog;
        public string SelectedFurniturCatalog
        {
            get => _selectedFurniturCatalog;
            set => Set(ref _selectedFurniturCatalog, value);
        }

        private string _countFurnitur;
        public string CountFurnitur
        {
            get => _countFurnitur;
            set => Set(ref _countFurnitur, value);
        }

        private ObservableCollection<Furniture> _furnitureList;
        public ObservableCollection<Furniture> FurnitureList
        {
            get => _furnitureList;
            set => Set(ref _furnitureList, value);
        }
        private Furniture _selectedFurniture;
        public Furniture SelectedFurniture
        {
            get => _selectedFurniture;
            set
            {
                Set(ref _selectedFurniture, value);
                CurrentFurniture.Furniture = value;
                mainViewModel.CurrentPage = FurniturePage;
            }
        }
    }
}
