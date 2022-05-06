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
        private Page MainPage = new MainPage();
        private Page FurniturePage = new FurniturePage();

        #region Свойства
        private Page currentPage;
        public Page CurrentPage
        {
            get => currentPage;
            set => Set(ref currentPage, value);
        }

        private string frame;
        public string Frame
        {
            get => frame;
            set => Set(ref frame, value);
        }

        private ObservableCollection<Furniture> _furnitureList;
        public ObservableCollection<Furniture> FurnitureList
        {
            get => _furnitureList;
            set => Set(ref _furnitureList, value);
        }

        private Furniture _selectedFurniture;
        public Furniture SelectedFurniture
        {
            get => _selectedFurniture;
            set
            {
                Set(ref _selectedFurniture, value);
                CurrentFurniture.Furniture = value;
                CurrentPage = FurniturePage;
            }
        }

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
        #endregion

        public MainViewModel()
        {
            MainPage.DataContext = this;
            FurniturePage.DataContext = this;
            CurrentPage = MainPage;
            CurrentPageWindow._CurrentPage = MainPage;
            FurnitureList = DataWorker.GetFurniturе("Диван");
        }

        

        #region Команды

        private ICommand _OpenMainPageCommand;
        public ICommand OpenMainPageCommand
        {
            get => _OpenMainPageCommand ?? new ActionCommand(OnOpenMainPageCommand);
        }

        private void OnOpenMainPageCommand(object p)
        {
            if (CurrentPage != MainPage)
            {
                SelectedFurniture = null;
                CurrentPage = MainPage;
            }
        }

        //Добавление товара в корзину
        private ICommand _AddFurnitureInBasketCommand;
        public ICommand AddFurnitureInBasketCommand
        {
            get => _AddFurnitureInBasketCommand ?? new ActionCommand(OnAddFurnitureInBasketCommand);
        }

        private void OnAddFurnitureInBasketCommand(object p)
        {

        }

        //Открытие
        private ICommand _openListCatalogCommand;
        public ICommand OpenListCatalogCommand
        {
            get => _openListCatalogCommand ?? new ActionCommand(OnOpenListCatalogCommand);
        }

        private void OnOpenListCatalogCommand(object p)
        {
            var mainwin = p as Window;

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

                    }
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
                FurniturCatalog fr = new FurniturCatalog();
                fr.DataContext = new FurniturePageViewModal(o.Content.ToString(), this);
                CurrentPage = fr;
            });
        }

        #endregion
    }
}
