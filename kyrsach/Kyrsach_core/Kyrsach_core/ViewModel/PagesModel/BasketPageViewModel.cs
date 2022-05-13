using Kyrsach_core.Infrastructur;
using Kyrsach_core.Model;
using Kyrsach_core.View.Pages;
using Kyrsach_core.ViewModel.PagesModel.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kyrsach_core.ViewModel.PagesModel
{
    public class BasketPageViewModel : BasePageViewModel
    {
        public BasketPageViewModel(MainViewModel mv)
        {
            MainViewModel = mv;
            FurnitureList = BasketList.FurnituresInBasket;
        }

        private Furniture _furnitureInBasket;
        public Furniture FurnitureInBasket
        {
            get => _furnitureInBasket;
            set => Set(ref _furnitureInBasket, value);
        }

        private ICommand _addOrderCommand;
        public ICommand AddOrderCommand
        {
            get => _addOrderCommand ?? new ActionCommand(p =>
            {
                if(!DataWorker.AddFurnituresInOrder())
                    MessageBox.Show("NO");
                else
                {
                    BasketList.FurnituresInBasket.Clear();
                    FurnitureList.Clear();
                    MainViewModel.CountFurnitureInBasket = 0;
                }
            });
        }

        private ICommand _deleteFurnitureCommand;
        public ICommand DeleteFurnitureCommand
        {
            get => _deleteFurnitureCommand ?? new ActionCommand(p =>
            {
                if (SelectedFurniture != null)
                {
                    DataWorker.DeleteFuritureInBasket(_furnitureInBasket);
                    FurnitureList.Remove(SelectedFurniture);
                    BasketList.FurnituresInBasket.Remove(SelectedFurniture);
                }
                else
                {
                    DataWorker.DeleteFuritureInBasket();
                    FurnitureList.Clear();
                    BasketList.FurnituresInBasket.Clear();
                }
            });
        }
    }
}
