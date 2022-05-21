using Kyrsach_core.Infrastructur;
using Kyrsach_core.Model;
using Kyrsach_core.View.Pages;
using Kyrsach_core.ViewModel.PagesModel.Base;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
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

        private string _messageContent;
        public string MessageContent
        {
            get => _messageContent;
            set => Set(ref _messageContent, value);
        }

        private ICommand _addOrderCommand;
        public ICommand AddOrderCommand
        {
            get => _addOrderCommand ?? new ActionCommand(p =>
            {
                //var mes = new SnackbarMessage();
                var o = p as Snackbar;
                if (!DataWorker.AddFurnituresInOrder())
                {
                    MessageContent = "Ваша корзина пуста!";
                    if (o != null)
                    {
                        if (o.MessageQueue is { } messageQueue)
                        {
                            //use the message queue to send a message.
                            //var message = MessageTextBox.Text;
                            //the message queue can be called from any thread
                            Task.Factory.StartNew(() => messageQueue.Enqueue(MessageContent, "OK", param => Trace.WriteLine("ОК"), MessageContent));
                        }
                    }
                }
                else
                {
                    BasketList.FurnituresInBasket.Clear();
                    FurnitureList.Clear();
                    MainViewModel.CountFurnitureInBasket = 0;
                    MessageContent = "Заказ успешно оформлен!";
                    
                    if (o != null)
                    {
                        if (o.MessageQueue is { } messageQueue)
                        {
                            //use the message queue to send a message.
                            //var message = MessageTextBox.Text;
                            //the message queue can be called from any thread
                            Task.Factory.StartNew(() => messageQueue.Enqueue(MessageContent));
                        }
                    }
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
                    MainViewModel.CountFurnitureInBasket = 0;
                }
                else
                {
                    DataWorker.DeleteFuritureInBasket();
                    FurnitureList.Clear();
                    BasketList.FurnituresInBasket.Clear();
                    MainViewModel.CountFurnitureInBasket--;
                }
            });
        }
    }
}
