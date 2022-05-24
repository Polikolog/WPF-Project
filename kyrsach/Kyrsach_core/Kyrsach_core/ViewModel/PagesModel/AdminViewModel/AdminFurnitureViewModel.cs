using Kyrsach_core.Infrastructur;
using Kyrsach_core.Infrastructur.Entity;
using Kyrsach_core.Model;
using Kyrsach_core.View.AdminPage;
using Kyrsach_core.ViewModel.PagesModel.AdminViewModel.Base;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kyrsach_core.ViewModel.PagesModel.AdminViewModel
{
    public class AdminFurnitureViewModel : BaseAdminViewModel<Furniture>
    {
        private Page FurniturePage;
        private readonly OpenFileDialog _openFileDialog = new OpenFileDialog();

        public AdminFurnitureViewModel(ViewModel.AdminViewModel adm) : base(adm)
        {
            FurniturePage = new FurniturePage();

            GetFurniture();
        }

        #region Команды
        //Изменение товара
        private ICommand _changedSelectedItemCommand;
        public ICommand ChangedSelectedItemCommand
        {
            get => _changedSelectedItemCommand ?? (_changedSelectedItemCommand = new ActionCommand(p =>
            {
                _currentAction = p.ToString();
                FurniturePage.DataContext = this;
                AdminViewModel.CurrentPage = FurniturePage;
            }));
        }

        //Добавление товара
        private ICommand _addNewItemCommand;
        public ICommand AddNewItemCommand
        {
            get => _addNewItemCommand ?? (_addNewItemCommand = new ActionCommand(p =>
            {
                _currentAction = p.ToString();
                SelectedItem = new Furniture();
                FurniturePage.DataContext = this;
                AdminViewModel.CurrentPage = FurniturePage;
            }));
        }

        //Удаление товара
        private ICommand _deleteSelectedItemCommand;
        public ICommand DeleteSelectedItemCommand
        {
            get => _deleteSelectedItemCommand ?? (_deleteSelectedItemCommand = new ActionCommand(p =>
            {
                db.Furnitures.Delete(SelectedItem);
                GetFurniture();
            }));
        }

        //Сохранение изменений или добавлений
        private ICommand _saveChangedCommand;
        public ICommand SaveChangedCommand
        {
            get => _saveChangedCommand ?? (_saveChangedCommand = new ActionCommand(p =>
            {
                if(_currentAction == "Changed")
                    db.Furnitures.Update(SelectedItem);
                else if(_currentAction == "Add")
                    db.Furnitures.Add(SelectedItem);
                GetFurniture();
            }));
        }

        private ICommand _changedImageCommand;
        public ICommand ChangedImageCommand
        {
            get => _changedImageCommand ?? (_changedImageCommand = new ActionCommand(p =>
            {
                _openFileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)" +
                                 "|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
                _openFileDialog.ShowDialog();
                SelectedItem.Image = _openFileDialog.FileName;
                db.Users.GetAllItems.Where(u => u.ID == SelectedItem.ID).FirstOrDefault().Image = SelectedItem.Image;
            }));
        }
        #endregion

        private void GetFurniture()
        {
            List.Clear();
            db.Furnitures.GetAllItems.ToList().ForEach(f => List.Add(f));
            SelectedList = List;
        }
    }
}
