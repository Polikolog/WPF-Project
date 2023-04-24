using Kyrsach_core.Infrastructur;
using Kyrsach_core.Infrastructur.Entity;
using Kyrsach_core.Model;
using Kyrsach_core.View.AdminPage;
using Kyrsach_core.ViewModel.PagesModel.AdminViewModel.Base;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kyrsach_core.ViewModel.PagesModel.AdminViewModel
{
    public class AdminUserViewModel : BaseAdminViewModel<User>
    {
        private Page UserPage;
        private readonly OpenFileDialog _openFileDialog = new OpenFileDialog();
        public AdminUserViewModel(ViewModel.AdminViewModel adm) : base(adm)
        {
            UserPage = new UsersPage();

            GetList();
        }

        #region Свойства
        private Furniture _selectedFurnitureInLike;
        public Furniture SelectedFurnitureInLike
        {
            get => _selectedFurnitureInLike;
            set => Set(ref _selectedFurnitureInLike, value);
        }

        private Furniture _selectedFurnitureInBasket;
        public Furniture SelectedFurnitureInBasket
        {
            get => _selectedFurnitureInBasket;
            set => Set(ref _selectedFurnitureInBasket, value);
        }

        private OrderFurniture _selectedOrder;
        public OrderFurniture SelectedOrder
        {
            get => _selectedOrder;
            set => Set(ref _selectedOrder, value);
        }

        private ObservableCollection<Furniture> _furnitureInLikeList;
        public ObservableCollection<Furniture> FurnitureInLikeList
        {
            get => _furnitureInLikeList;
            set => Set(ref _furnitureInLikeList, value);
        }

        private ObservableCollection<Furniture> _furnitureInBasketList;
        public ObservableCollection<Furniture> FurnitureInBasketList
        {
            get => _furnitureInBasketList;
            set => Set(ref _furnitureInBasketList, value);
        }

        private ObservableCollection<OrderFurniture> _orderList;
        public ObservableCollection<OrderFurniture> OrderList
        {
            get => _orderList;
            set => Set(ref _orderList, value);
        }
        #endregion

        #region Команды
        //Изменение пользователя
        private ICommand _changedSelectedItemCommand;
        public ICommand ChangedSelectedItemCommand
        {
            get => _changedSelectedItemCommand ?? (_changedSelectedItemCommand = new ActionCommand(p =>
            {
                _currentAction = p.ToString();
                UserPage.DataContext = this;
                AdminViewModel.CurrentPage = UserPage;
                GetFurnitureInBasket();
                GetFurnitureInLike();
                GetOrder();
            }));
        }

        //Добавление пользователя
        private ICommand _addNewItemCommand;
        public ICommand AddNewItemCommand
        {
            get => _addNewItemCommand ?? (_addNewItemCommand = new ActionCommand(p =>
            {
                _currentAction = p.ToString();
                UserPage.DataContext = this;
                AdminViewModel.CurrentPage = UserPage;
            }));
        }

        //Удаление пользователя
        private ICommand _deleteSelectedItemCommand;
        public ICommand DeleteSelectedItemCommand
        {
            get => _deleteSelectedItemCommand ?? (_deleteSelectedItemCommand = new ActionCommand(p =>
            {
                db.Users.Delete(SelectedItem);
                GetList();
            }));
        }

        //Сохранение изменений или добавлений
        private ICommand _saveChangedCommand;
        public ICommand SaveChangedCommand
        {
            get => _saveChangedCommand ?? (_saveChangedCommand = new ActionCommand(p =>
            {
                if (_currentAction == "Changed")
                    db.Users.Update(SelectedItem);
                else if (_currentAction == "Add")
                    db.Users.Add(SelectedItem);
                GetList();
                AdminViewModel.CurrentPage = PreviousPage.AdminPage;
            }));
        }

        private ICommand _deleteFurnitureInLikeCommand;
        public ICommand DeleteFurnitureInLikeCommand
        {
            get => _deleteFurnitureInLikeCommand ?? (_deleteFurnitureInLikeCommand = new ActionCommand(p =>
            {
                if (SelectedFurnitureInLike != null)
                {
                    DataWorker.DeleteFurnitureInLike(SelectedFurnitureInLike);
                    FurnitureInLikeList.Remove(SelectedFurnitureInLike);
                }
            }));
        }

        //Удаление товара из корзины
        private ICommand _deleteFurnitureInBasketCommand;
        public ICommand DeleteFurnitureInBasketCommand
        {
            get => _deleteFurnitureInBasketCommand ?? (_deleteFurnitureInBasketCommand = new ActionCommand(p =>
            {
                if (SelectedFurnitureInBasket != null)
                {
                    DataWorker.DeleteFurnitureInLike(SelectedFurnitureInBasket);
                    FurnitureInBasketList.Remove(SelectedFurnitureInBasket);
                }
            }));
        }

        //Удаление заказа
        private ICommand _deleteSelectedOrder;
        public ICommand DeleteSelectedOrderCommand
        {
            get => _deleteSelectedOrder ?? (_deleteSelectedOrder = new ActionCommand(p =>
            {
               if(_selectedOrder != null)
               {    
                    db.OrderFurnitures.Delete(SelectedOrder);
                    db.Order.Delete(SelectedOrder.Order);
                    OrderList.Remove(SelectedOrder);
               }
            }));
        }

        //Изменение картинки пользователя
        private ICommand _changedImageCommand;
        public ICommand ChangedImageCommand
        {
            get => _changedImageCommand ?? (_changedImageCommand = new ActionCommand(p =>
            {
                _openFileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)" +
                                 "|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
                _openFileDialog.ShowDialog();
                SelectedItem.Image = _openFileDialog.FileName;
                DataWorker.ChangedImageUser(SelectedItem.Image);
            }));
        }
        #endregion

        private void GetList()
        {
            if(List.Count > 0)
                List.Clear();
            db.Users.GetAllItems.ToList().ForEach(u => List.Add(u));
            SelectedList = List;
        }
        private void GetFurnitureInBasket()
        {
            ObservableCollection<Furniture> list = new ObservableCollection<Furniture>();
            db.BasketFurnitures.GetAllItems.Where(bf => bf.BasketID == SelectedItem.ID).Join(db.Furnitures.GetAllItems, bf => bf.FurnitureID, f => f.ID, (bf, f) => new Furniture { ID = f.ID, Name = f.Name, Type = f.Type, Availability = f.Availability, Category = f.Category, Color = f.Color, Width = f.Width, Length = f.Length, Height = f.Height, Description = f.Description, Image = f.Image, ImageIcon = f.ImageIcon, LivingSector = f.LivingSector, Price = f.Price, Rating = f.Rating }).ToList().ForEach(f => list.Add(f));
            FurnitureInBasketList = list;
        }
        private void GetFurnitureInLike()
        {
            ObservableCollection<Furniture> list = new ObservableCollection<Furniture>();
            db.LikeFurnitures.GetAllItems.Where(lf => lf.LikeID == SelectedItem.Like.ID).Join(db.Furnitures.GetAllItems, lf => lf.FurnitureID, f => f.ID, (lf, f) => new Furniture { ID = f.ID, Name = f.Name, Type = f.Type, Availability = f.Availability, Category = f.Category, Color = f.Color, Width = f.Width, Length = f.Length, Height = f.Height, Description = f.Description, Image = f.Image, ImageIcon = f.ImageIcon, LivingSector = f.LivingSector, Price = f.Price, Rating = f.Rating }).ToList().ForEach(f => list.Add(f));
            FurnitureInLikeList = list;
        }
        private void GetOrder()
        {
            ObservableCollection<OrderFurniture> list = new ObservableCollection<OrderFurniture>();
            db.Order.GetAllItems.Where(o => o.ID == SelectedItem.ID).Join(db.OrderFurnitures.GetAllItems, o => o.ID, of => of.OrderID, (o, of) => new OrderFurniture { OrderID = o.ID, FurnitureID = of.FurnitureID, ID = of.ID }).ToList().ForEach(of => list.Add(of));
            MessageBox.Show(list.Count().ToString());
            OrderList = list;
        }
    } 
}
