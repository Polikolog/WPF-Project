using Kyrsach_core.Infrastructur;
using Kyrsach_core.Infrastructur.Entity;
using Kyrsach_core.Model;
using Kyrsach_core.ViewModel.PagesModel.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Kyrsach_core.ViewModel.PagesModel
{
    public class CatalogViewModel : BasePageViewModel
    {
        private ObservableCollection<Furniture> List = new ObservableCollection<Furniture>();

        public CatalogViewModel( string type, MainViewModel page, string Category = null)
        {
            MainViewModel = page;

            SelectedFurnitureCatalog = type;

            CountFurniture = DataWorker.CountFurniturСertainType(type).ToString() + " модели";
            List = DataWorker.GetFurniturе(type);
            CategoriesList = DataWorker.GetCategories(type);
            if (Category == null)
            { 
                FurnitureList = List;
            }
            else
            {
                ObservableCollection<Furniture> list = new ObservableCollection<Furniture>();
                List.Where(f => f.Category == Category).ToList().ForEach(f => list.Add(f));
                FurnitureList = list;
                CountFurniture += "     Категория " + Category;
            }
        }

        public CatalogViewModel(string type, ObservableCollection<Furniture> list, MainViewModel main)
        {
            MainViewModel = main;
            SelectedFurnitureCatalog = type;
            CountFurniture = list.Count.ToString() + " найдено совпадений";
            List = list;
            FurnitureList = list;
        }

        #region Свойства
        //Выбранный каталог товаров
        private string? _selectedFurnitureCatalog;
        public string? SelectedFurnitureCatalog
        {
            get => _selectedFurnitureCatalog;
            set => Set(ref _selectedFurnitureCatalog, value);
        }

        //Количество товаров в каталоге
        private string? _countFurniture;
        public string? CountFurniture
        {
            get => _countFurniture;
            set => Set(ref _countFurniture, value);
        }

        private ObservableCollection<Categories> _categoriesList;
        public ObservableCollection<Categories> CategoriesList
        {
            get => _categoriesList;
            set => Set(ref _categoriesList, value);
        }
        #endregion

        #region Команды

        #region Цена
        private ICommand _sortLightPriceCommand;
        public ICommand SortLightPriceCommand
        {
            get => _sortLightPriceCommand ?? (_sortLightPriceCommand ?? new ActionCommand( p =>
            {
                if (FurnitureList.Count != 0)
                {
                    var list = new ObservableCollection<Furniture>();
                    List.Where(f => f.Price < (GetAvgInFurnitureList() / 2)).ToList().ForEach(f => list.Add(f));
                    FurnitureList = list;
                }
            }));
        }

        private ICommand _sortComfortPriceCommand;
        public ICommand SortComfortPriceCommand
        {
            get => _sortComfortPriceCommand ?? (_sortComfortPriceCommand ?? new ActionCommand(p =>
            {
                if(FurnitureList.Count != 0)
                { 
                    var list = new ObservableCollection<Furniture>();
                    List.Where(f => f.Price < GetAvgInFurnitureList() && f.Price > (GetAvgInFurnitureList() / 2)).ToList().ForEach(f => list.Add(f));
                    FurnitureList = list;
                }
            }));
        }

        private ICommand _sortMediumPriceCommand;
        public ICommand SortMediumPriceCommand
        {
            get => _sortMediumPriceCommand ?? (_sortMediumPriceCommand ?? new ActionCommand(p =>
            {
                if (FurnitureList.Count != 0)
                {
                    var list = new ObservableCollection<Furniture>();
                    List.Where(f => f.Price > GetAvgInFurnitureList() && f.Price < (GetAvgInFurnitureList() * 2)).ToList().ForEach(f => list.Add(f));
                    FurnitureList = list;
                }
            }));
        }

        private ICommand _sortPremiumPriceCommand;
        public ICommand SortPremiumPriceCommand
        {
            get => _sortPremiumPriceCommand ?? (_sortPremiumPriceCommand ?? new ActionCommand(p =>
            {
                if (FurnitureList.Count != 0)
                {
                    var list = new ObservableCollection<Furniture>();
                    List.Where(f => f.Price > (GetAvgInFurnitureList() * 2)).ToList().ForEach(f => list.Add(f));
                    FurnitureList = list;
                }
            }));
        }
        #endregion

        #region Размер
        private ICommand _sortSmallSizeCommand;
        public ICommand SortSmallSizeCommand
        {
            get => _sortComfortPriceCommand ?? (_sortComfortPriceCommand ?? new ActionCommand(p =>
            {
                var list = new ObservableCollection<Furniture>();
                List.Where(f => (f.Width * f.Height * f.Length) < GetAvgSize() - 10).ToList().ForEach(f => list.Add(f));
                FurnitureList = list;
            }));
        }

        private ICommand _sortMediumSizeCommand;
        public ICommand SortMediumSizeCommand
        {
            get => _sortMediumSizeCommand ?? (_sortMediumSizeCommand ?? new ActionCommand(p =>
            {
                var list = new ObservableCollection<Furniture>();
                List.Where(f => (f.Width * f.Height * f.Length) < GetAvgSize() + 10 && (f.Width * f.Height * f.Length) > GetAvgSize() - 10).ToList().ForEach(f => list.Add(f));
                FurnitureList = list;
            }));
        }

        private ICommand _sortLargeSizeCommand;
        public ICommand SortLargeSizeCommand
        {
            get => _sortLargeSizeCommand ?? (_sortLargeSizeCommand ?? new ActionCommand(p =>
            {
                var list = new ObservableCollection<Furniture>();
                List.Where(f => (f.Width * f.Height * f.Length) > GetAvgSize() + 10).ToList().ForEach(f => list.Add(f));
                FurnitureList = list;
            }));
        }
        #endregion

        #region Категории
        private ICommand _sortCategoriesCommand;
        public ICommand SortCategoriesCommand
        {
            get => _sortCategoriesCommand ?? (_sortCategoriesCommand ?? new ActionCommand(p =>
            {
                var str = p.ToString();
                var list = new ObservableCollection<Furniture>();
                List.Where(f => f.Category == str).ToList().ForEach(f => list.Add(f));
                FurnitureList = list;
            }));
        }
        #endregion

        private ICommand _reloadListCommand;
        public ICommand ReloadListCommand
        {
            get => _reloadListCommand ?? (_reloadListCommand = new ActionCommand( p =>
            {
                FurnitureList = List;
            }));
        }

        #endregion

        #region Функции

        private decimal? GetAvgInFurnitureList()
        {
            decimal? avg = 0;
            if (List != null)
            {
                foreach (var item in List)
                {
                    avg += item.Price;
                }
                avg = avg / FurnitureList.Count;
            }
            return avg;
        }

        private int? GetAvgSize()
        {
            int? avgsize = 0;
            if(List != null)
            {
                int? sqr = 0;
                foreach(var item in List)
                {
                    sqr += (item.Width * item.Height * item.Length);
                }
                avgsize = (sqr / List.Count);
            }
            return avgsize;
        }


        #endregion
    }
}
