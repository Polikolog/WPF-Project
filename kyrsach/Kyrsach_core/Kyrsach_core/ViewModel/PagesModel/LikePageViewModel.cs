using Kyrsach_core.Infrastructur;
using Kyrsach_core.Infrastructur.Entity;
using Kyrsach_core.Model;
using Kyrsach_core.ViewModel.PagesModel.Base;
using System.Collections.Generic;
using System.Windows.Input;

namespace Kyrsach_core.ViewModel.PagesModel
{
    public class LikePageViewModel : BasePageViewModel
    {
        public LikePageViewModel(MainViewModel mv)
        {
            MainViewModel = mv;
            FurnitureList = LikeList.list;
        }

        private Furniture _furnitureInLike;
        public Furniture FurnitureInLike
        {
            get => _furnitureInLike;
            set => Set(ref _furnitureInLike, value);
        }

        private Furniture _selectedItemInLike;
        public Furniture SelectedItemInLike
        {
            get => _selectedItemInLike;
            set => Set(ref _selectedItemInLike, value);
        }

        private ICommand _deleteSelectedFurnitureCommand;
        public ICommand DeleteSelectedFurnitureCommand
        {
            get => _deleteSelectedFurnitureCommand ?? (_deleteSelectedFurnitureCommand = new ActionCommand(p =>
            {
                var item = p as Furniture;
                if (item != null)
                {
                    DataWorker.DeleteFurnitureInLike(item);
                    FurnitureList.Remove(item);
                    LikeList.list.Remove(item);
                    MainViewModel.CountLikeFurniture--;
                }
            }));
        }

        private ICommand _deleteFurnitureCommand;
        public ICommand DeleteFurnitureCommand
        {
            get => _deleteFurnitureCommand ??(_deleteFurnitureCommand = new ActionCommand(p =>
            {
                    DataWorker.DeleteFurnitureInLike();
                    FurnitureList.Clear();
                    LikeList.list.Clear();
                    MainViewModel.CountLikeFurniture = 0;
            }));
        }
    }   
}
