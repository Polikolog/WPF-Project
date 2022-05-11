using Kyrsach_core.Infrastructur;
using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.ViewModel.PagesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Kyrsach_core.ViewModel.Other
{
    public class QuestionViewModel : ViewModelBase
    {
        private ProfileViewModel profileViewModel;

        public QuestionViewModel(ProfileViewModel profileViewModel)
        {
            this.profileViewModel = profileViewModel;
        }


        private string _sourceImage;
        public string SourceImage
        {
            get => _sourceImage;
            set => Set(ref _sourceImage, value);
        }

        #region Команды
        private ICommand _changedImageCommand;
        public ICommand ChangedImageCommand
        {
            get => _changedImageCommand ?? new ActionCommand( win =>
            {

            });
        }

        private ICommand _closeQuestionCommand;
        public ICommand CloseQuestionCommand
        {
            get => _closeQuestionCommand ?? new ActionCommand( win =>
            {
                (win as Window).Close();
            });
        }
        #endregion
    }
}
