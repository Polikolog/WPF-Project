using Kyrsach_core.Model;
using Kyrsach_core.ViewModel.PagesModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.ViewModel.PagesModel
{
    public class LikePageViewModel : BasePageViewModel
    {
        public LikePageViewModel(MainViewModel mv)
        {
            MainViewModel = mv;
            FurnitureList = LikeList.list;
        }
    }
}
