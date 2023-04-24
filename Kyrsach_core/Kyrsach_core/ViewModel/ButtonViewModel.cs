using Kyrsach_core.Infrastructur.Base;
using System;
using System.Windows.Media;

namespace Kyrsach_core.ViewModel
{
    public class ButtonViewModel : ViewModelBase
    {
        private ImageSource sourceIn;
        public ImageSource SourceIn
        {
            get => sourceIn;
            set => Set(ref sourceIn, value);
        }

        private string textInBlock;
        public string TextInBlock
        {
            get => textInBlock;
            set => Set(ref textInBlock, value);
        }
    }
}
