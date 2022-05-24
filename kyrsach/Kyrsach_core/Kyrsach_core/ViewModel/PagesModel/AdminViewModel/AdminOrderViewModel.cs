using Kyrsach_core.Infrastructur.Entity;
using Kyrsach_core.ViewModel.PagesModel.AdminViewModel.Base;

namespace Kyrsach_core.ViewModel.PagesModel.AdminViewModel
{
    public class AdminOrderViewModel : BaseAdminViewModel<Order>
    {
        public AdminOrderViewModel(ViewModel.AdminViewModel adm) : base(adm)
        {
        }
    }
}
