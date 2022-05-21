using Kyrsach_core.Infrastructur;
using Kyrsach_core.Model;
using Kyrsach_core.View.Pages;
using Kyrsach_core.ViewModel.PagesModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kyrsach_core.ViewModel.PagesModel
{
    public class TypeCategoriesViewModel : BasePageViewModel
    {
        public TypeCategoriesViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        #region Поля
        private Page FurnitureCatalog = new FurniturCatalog();
        #endregion

        #region Свойства
        //Список категорий выбранного типа
        private ObservableCollection<Categories> _categoriesList;
        public ObservableCollection<Categories> CategoriesList
        {
            get => _categoriesList;
            set => Set(ref _categoriesList, value);
        }

        //Открытие выбранного каталога
        private string _catalogType;
        public string CatalogType
        {
            get => _catalogType;
            set => Set(ref _catalogType, value);
        }

        //Выбраная категория типа   ///////////////////////////////////////////////////////////////////////////////////////////////
        private Categories _selectedCategory;
        public Categories SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                Set(ref _selectedCategory, value);
                try
                {
                    string Category = SelectedCategory.Category;
                    FurnitureCatalog.DataContext = new CatalogViewModel(CatalogType, MainViewModel, Category);
                    MainViewModel.CurrentPage = FurnitureCatalog;
                    PreviousPage.LastPage = FurnitureCatalog;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        #region Команды
        //Открытие каталоге
        private ICommand _catalogСabinetsCommand;
        public ICommand CatalogСabinetsCommand
        {
            get => _catalogСabinetsCommand ?? new ActionCommand((p) =>
            {
                var o = p as string;
                FurnitureCatalog.DataContext = new CatalogViewModel(o, MainViewModel);
                MainViewModel.CurrentPage = FurnitureCatalog;
                PreviousPage.LastPage = FurnitureCatalog;
            });
        }

        //Открытие категорий определенного типа мебели
        private ICommand _selectCategoryCommand;
        public ICommand SelectCategoryCommand
        {
            get => _selectCategoryCommand ?? (_selectCategoryCommand = new ActionCommand(p =>
            {
                var str = p as string;
                if (str != null)
                {
                    CatalogType = str;
                    CategoriesList = DataWorker.GetCategories(str);
                    MessageBox.Show(CategoriesList.Count.ToString());
                }
            }));
        }
        #endregion
    }
}
