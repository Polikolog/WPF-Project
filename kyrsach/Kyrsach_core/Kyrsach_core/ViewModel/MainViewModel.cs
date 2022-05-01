using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.View.Pages;
using System.Windows.Controls;

namespace Kyrsach_core.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Page currentPage;
        public Page CurrentPage
        {
            get => currentPage;
            set => Set(ref currentPage, value);
        }

        private Page MainPage = new MainPage();

        private string frame;
        public string Frame
        {
            get => frame;
            set => Set(ref frame, value);
        }


        public MainViewModel()
        {
            CurrentPage = MainPage;
        }
    }
}
