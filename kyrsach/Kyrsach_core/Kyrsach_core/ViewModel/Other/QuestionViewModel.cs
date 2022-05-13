using Kyrsach_core.Infrastructur;
using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Model;
using Kyrsach_core.View.Other;
using Kyrsach_core.ViewModel.PagesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Kyrsach_core.ViewModel.Other
{
    public class QuestionViewModel : ViewModelBase
    {
        private QuestionWindow _QstnWindows;
        public QuestionWindow QstnWindows
        {
            get { return _QstnWindows; }
            set =>Set(ref _QstnWindows,value);
        }
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
                var wind = win as QuestionWindow;
                if (!DataWorker.ChangedImageUser(SourceImage))
                {
                    wind.TextBox.BorderBrush = Brushes.Red;
                }
                else
                    wind.Close();
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
