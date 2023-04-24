using Kyrsach_core.Infrastructur.Entity;
using Kyrsach_core.Model;
using Kyrsach_core.ViewModel.PagesModel.Base;
using System.Collections.ObjectModel;
using System.Linq;

namespace Kyrsach_core.ViewModel.PagesModel
{
    public class ComparisonViewModel : BasePageViewModel
    {
        public ComparisonViewModel()
        {
            TypesFurniture = CurrentUser.GetTypesInComparison();
            SelectedType = null;
        }

        private string _selectedType;
        public string SelectedType
        {
            get => _selectedType;
            set 
            { 
                Set(ref _selectedType, value); 
                ObservableCollection<Furniture> list = new ObservableCollection<Furniture>();
                CurrentUser.FurnitureInComparison.Where(f => f.Type == value).ToList().ForEach(f => list.Add(f));
                FurnitureList = list;
            }
        }

        private ObservableCollection<string> _typesFurniture;
        public ObservableCollection<string> TypesFurniture
        {
            get => _typesFurniture;
            set => Set(ref _typesFurniture, value);
        }
    }
}
