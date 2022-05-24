using Kyrsach_core.Infrastructur;
using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Infrastructur.Entity;
using Kyrsach_core.Model;
using Kyrsach_core.View.Pages;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kyrsach_core.ViewModel.PagesModel.Base
{
    public class BasePageViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel { get; set; }
        private Page FurniturePage = new FurniturePage();

        //Список товаров
        private ObservableCollection<Furniture> _furnitureList;
        public ObservableCollection<Furniture> FurnitureList
        {
            get => _furnitureList;
            set => Set(ref _furnitureList, value);
        }

        //Выбранный товар
        private Furniture _selectedFurniture;
        public Furniture SelectedFurniture
        {
            get => _selectedFurniture;
            set
            {
                Set(ref _selectedFurniture, value);
                //CurrentFurniture.Furniture = value;
                if (value != null)
                {
                    var furnitureViewModel = new FurnitureViewModel(this);
                    CurrentUser.AddFurnitureInViewed(value);
                    FurniturePage.DataContext = furnitureViewModel;
                    MainViewModel.CurrentPage = FurniturePage;
                }
            }
        }

        private ICommand _addFurnitureInBasketCommand;
        public ICommand AddFunritureInBasketCommand
        {
            get => _addFurnitureInBasketCommand ?? new ActionCommand(p =>
            {
                DataWorker.AddFurnitureInBasket(SelectedFurniture);
                BasketList.FurnituresInBasket.Add(SelectedFurniture);
                MainViewModel.CountFurnitureInBasket++;
            });
        }

        private ICommand _addFurnitureInLike;
        public ICommand AddFurnitureInLike
        {
            get => _addFurnitureInLike ?? new ActionCommand(p =>
            {
                DataWorker.AddFurnitureInLike(SelectedFurniture);
                LikeList.list.Add(SelectedFurniture);
                MainViewModel.CountLikeFurniture++;
            });
        }
    }
}
