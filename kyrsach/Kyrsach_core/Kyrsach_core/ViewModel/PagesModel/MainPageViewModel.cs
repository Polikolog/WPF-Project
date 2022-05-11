using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Model;
using System.Collections.ObjectModel;
using Kyrsach_core.Model;
using Kyrsach_core.View.Pages;
using Kyrsach_core.ViewModel.PagesModel.Base;

namespace Kyrsach_core.ViewModel.PagesModel
{
    public class MainPageViewModel : BasePageViewModel
    {
        public MainPageViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
            FurnitureList = DataWorker.GetFurniturе("Диван");
        }
    }
}
