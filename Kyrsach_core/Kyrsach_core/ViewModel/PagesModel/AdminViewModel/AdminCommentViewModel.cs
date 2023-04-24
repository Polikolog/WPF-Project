using Kyrsach_core.Infrastructur.Entity;
using Kyrsach_core.ViewModel.PagesModel.AdminViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.ViewModel.PagesModel.AdminViewModel
{
    public class AdminCommentViewModel : BaseAdminViewModel<Comment>
    {
        public AdminCommentViewModel(ViewModel.AdminViewModel adm) : base(adm)
        {
            GetComment();
        }

        void GetComment()
        {
            List.Clear();
            db.Comment.GetAllItems.ToList().ForEach(c => List.Add(c));
            SelectedList = List;
        }
    }
}
