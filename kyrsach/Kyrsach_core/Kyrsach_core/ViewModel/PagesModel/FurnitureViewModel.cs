using Kyrsach_core.Infrastructur;
using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Model;
using Kyrsach_core.Model.Other;
using Kyrsach_core.ViewModel.PagesModel.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

            ReviewsList = DataWorker.GetComments(currentViewModel.SelectedFurniture);
            //UsersList = DataWorker.GetUsers(currentViewModel.SelectedFurniture);
        }

        #region Свойтсва

        private string _comment;
        public string Comment
        {
            get => _comment;
            set => Set(ref _comment, value);
        }

        private int _rating;
        public int AddRating
        {
            get => _rating;
            set 
            { 
                Set(ref _rating, value);
                DataWorker.UpdateRating(value, currentViewModel.SelectedFurniture);
            }
        }

        private ObservableCollection<UserComment> _reviewsList;
        public ObservableCollection<UserComment> ReviewsList
        {
            get => _reviewsList;
            set => Set(ref _reviewsList, value);
        }

        private UserComment _review;
        public UserComment Review
        {
            get => _review;
            set => Set(ref _review, value);
        }
        #endregion

        #region Команды
        private ICommand _addCommentCommand;
        public ICommand AddCommentCommand
        {
            get => _addCommentCommand ?? (_addCommentCommand = new ActionCommand(p =>
            {
                if (Comment != null)
                {
                    DataWorker.CreateComment(Comment, currentViewModel.SelectedFurniture);
                    ReviewsList = DataWorker.GetComments(currentViewModel.SelectedFurniture);
                    //UsersList = DataWorker.GetUsers(currentViewModel.SelectedFurniture);
                    Comment = null;
                }
                else
                    (p as TextBox).BorderBrush = Brushes.Red;
            }));
        }

        private ICommand _openPreviousPageCommand;
        public ICommand OpenPreviousPageCommand
        {
            get => _openPreviousPageCommand ?? (_openPreviousPageCommand = new ActionCommand( p =>
            {
                mainViewModel.CurrentPage = PreviousPage.LastPage;
                currentViewModel.SelectedFurniture = null;
            }));
        }
        #endregion
    }
}
