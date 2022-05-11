using Kyrsach_core.Model;
using Kyrsach_core.View.Pages;
using Kyrsach_core.ViewModel.PagesModel.Base;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Kyrsach_core.ViewModel.PagesModel
{
    public class BasketPageViewModel : BasePageViewModel
    {
        public BasketPageViewModel(MainViewModel mv)
        {
            MainViewModel = mv;
            FurnitureList = BasketList.FurnituresInBasket;
        }
    }
}
