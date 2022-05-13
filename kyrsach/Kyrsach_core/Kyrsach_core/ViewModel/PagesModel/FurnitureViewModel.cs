using Kyrsach_core.Infrastructur;
using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Model;
using Kyrsach_core.ViewModel.PagesModel.Base;
using System.Collections.ObjectModel;
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
            UsersList = DataWorker.GetUsers(currentViewModel.SelectedFurniture);
        }

        #region Свойтсва

        private int width;
        public int Width
        {
            get { return width; }
            set => Set(ref width, value);
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set => Set(ref _comment, value);
        }

        private ObservableCollection<Comment> _reviewsList;
        public ObservableCollection<Comment> ReviewsList
        {
            get => _reviewsList;
            set => Set(ref _reviewsList, value);
        }

        private Comment _review;
        public Comment Review
        {
            get => _review;
            set => Set(ref _review, value);
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set => Set(ref _selectedUser, value);
        }

        private ObservableCollection<User> _usersList;
        public ObservableCollection<User> UsersList
        {
            get => _usersList;
            set => Set(ref _usersList, value);
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
                    UsersList = DataWorker.GetUsers(currentViewModel.SelectedFurniture);
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
