using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.View.Pages;
using Kyrsach_core.Model;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Kyrsach_core.Infrastructur;
using System.Windows;
using Kyrsach_core.ViewModel.PagesModel;

namespace Kyrsach_core.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        #region Поля
        FurniturCatalog FurnitureCatalog = new FurniturCatalog();
        private Page MainPage = new MainPage();
        private Page BasketPage = new BasketPage();
        private Page LikePage = new LikePage();
        private Page ProfilePage = new ProfilePage();
        #endregion

        #region Свойства

        //Текущая страница
        private Page currentPage;
        public Page CurrentPage
        {
            get => currentPage;
            set => Set(ref currentPage, value);
        }

        //Имя фрейма
        private string frame;
        public string Frame
        {
            get => frame;
            set => Set(ref frame, value);
        }

        //Похожие товары
        private ObservableCollection<Furniture> _similarFurniture;
        public ObservableCollection<Furniture> SimilarFurniture
        {
            get => _similarFurniture;
            set => Set(ref _similarFurniture, value);
        }

        //Поисковая строка
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => Set(ref _searchText, value);
        }

        //Количество элементов в корзине
        private int _countFurnitureInBasket;
        public int CountFurnitureInBasket
        {
            get => _countFurnitureInBasket;
            set => Set(ref _countFurnitureInBasket, value);
        }

        //Количество понравившихся товаров
        private int _countLikeFurniture;
        public int CountLikeFurniture
        {
            get => _countLikeFurniture;
            set => Set(ref _countLikeFurniture, value);
        }
        #endregion

        public MainViewModel()
        {
            
            MainPage.DataContext = new MainPageViewModel(this);
            
            CurrentPage = MainPage;
            PreviousPage.LastPage = MainPage;

            LikeList.list = DataWorker.GetFurnituresInLike();
            BasketList.FurnituresInBasket = DataWorker.GetFurnituresInBasket();

            CountLikeFurniture = DataWorker.GetFurnituresInLike().Count;
            CountFurnitureInBasket = DataWorker.GetFurnituresInBasket().Count;
        }

        #region Команды

        //Открытие главной страницы
        private ICommand _OpenMainPageCommand;
        public ICommand OpenMainPageCommand
        {
            get => _OpenMainPageCommand ?? new ActionCommand(p =>
            {
                if (CurrentPage != MainPage)
                {
                    MainPage.DataContext = new MainPageViewModel(this);
                    CurrentPage = MainPage;
                    PreviousPage.LastPage = MainPage;
                }
            });
        }

        //Поиск
        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get => _searchCommand ?? new ActionCommand((p) =>
            {
                if (SearchText != null)
                {
                    var list = DataWorker.SearchFurniture(SearchText);
                    if (list.Count != 0)
                    {
                        FurnitureCatalog.DataContext = new CatalogViewModel(SearchText, list, this);
                        CurrentPage = FurnitureCatalog;
                    }
                    else
                        SearchText = "Ничего не найдено";
                }
                else
                    SearchText = "Введите текст";
            });
        }

        //Открытие каталога по кнопке в хедере
        private ICommand _catalogСabinetsCommand;
        public ICommand CatalogСabinetsCommand
        {
            get => _catalogСabinetsCommand ?? new ActionCommand((p) =>
            {
                var o = p as Button;
                FurnitureCatalog.DataContext = new CatalogViewModel(o.Content.ToString(), this);
                CurrentPage = FurnitureCatalog;
                PreviousPage.LastPage = FurnitureCatalog;
            });
        }

        //Открытие корзины
        private ICommand _openBasketCommand;
        public ICommand OpenBasketCommand
        {
            get => _openBasketCommand ?? new ActionCommand((p) =>
            {
                BasketPage.DataContext = new BasketPageViewModel(this);
                CurrentPage = BasketPage;
            });
        }
        
        //Открытие понравившихся товаров
        private ICommand _openLikeCommand;
        public ICommand OpenLikeCommand
        {
            get => _openLikeCommand ?? new ActionCommand((p) =>
            {
                LikePage.DataContext = new LikePageViewModel(this);
                CurrentPage = LikePage;
                PreviousPage.LastPage = LikePage;
            });
        }

        //Открытие окна профиля
        private ICommand _openProfileCommand;
        public ICommand OpenProfileCommand
        {
            get => _openProfileCommand ?? new ActionCommand((p) =>
            {
                ProfilePage.DataContext = new ProfileViewModel(this);
                CurrentPage = ProfilePage;
                PreviousPage.LastPage = ProfilePage;
            });

        }

        #endregion
    }
}
