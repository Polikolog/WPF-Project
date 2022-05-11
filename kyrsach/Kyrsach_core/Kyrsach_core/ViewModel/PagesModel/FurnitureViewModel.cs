using Kyrsach_core.Infrastructur;
using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Model;
using Kyrsach_core.ViewModel.PagesModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kyrsach_core.ViewModel.PagesModel
{
    public class FurnitureViewModel : ViewModelBase
    {
        public MainViewModel mainViewModel { get; set; }
        public BasePageViewModel currentViewModel { get; set; }

        public FurnitureViewModel(BasePageViewModel basepage)
        {
            currentViewModel = basepage;
            mainViewModel = basepage.MainViewModel;
        }
    }
}
