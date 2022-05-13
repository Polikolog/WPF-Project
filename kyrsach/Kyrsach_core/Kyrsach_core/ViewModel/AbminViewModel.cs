using Kyrsach_core.Infrastructur.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Kyrsach_core.ViewModel
{
    public class AbminViewModel : ViewModelBase
    {
        public AbminViewModel()
        {

        }

        //Текущая страница
        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set => Set(ref _currentPage, value);
        }


    }
}
