using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Model;
using System.Collections.ObjectModel;
using Kyrsach_core.Model;


namespace Kyrsach_core.ViewModel.PagesModel
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            FurnitureList = DataWorker.GetFurniturе("Диван");
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
            set => Set(ref _selectedFurniture, value);
        }

    }
}
