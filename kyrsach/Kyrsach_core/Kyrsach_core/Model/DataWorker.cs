using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Kyrsach_core.Model.Base;

namespace Kyrsach_core.Model
{
    public static class DataWorker
    {
        public static void CreateRole()
        {
            using(var bd = new ApplicationContext())
            {
                if (bd.Roles.ToList() == null)
                {
                    bd.Roles.Add(new Role { UserRole = "Пользователь" });
                    bd.SaveChanges();

                    bd.Roles.Add(new Role { UserRole = "Админ" });
                    bd.SaveChanges();
                }
            }
        }
        public static bool AddUser(string NameUser, string PasswordUser, string Adress, int? Num)
        {
            using (var bd = new ApplicationContext())
            {
                if (GetUser(NameUser, PasswordUser, Num))
                {
                    bd.Users.Add(new User { Name = NameUser, Password = PasswordUser, Adress = Adress, Phone = Num, RoleID = 1 });
                    bd.SaveChanges();

                    bd.Baskets.Add(new Basket { Price = null, OrderCompleted = null, UserID = bd.Users.OrderBy(p => p.ID).LastOrDefault().ID });
                    bd.SaveChanges();

                    return true;
                }
                else
                    return false; 
            }
        }

        public static bool GetUser(string name, string password, int? phone = null)
        {
            using(var bd = new ApplicationContext())
            {
                if (phone == null)
                {
                    var user = bd.Users.FirstOrDefault(u => u.Name == name && u.Password == password);
                    if (user != null)
                    {
                        CurrentUser.setInstance(user);
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    var user = bd.Users.FirstOrDefault(u => u.Name == name || u.Password == password || u.Phone == phone);
                    if (user == null)
                        return true;
                    else
                        return false;
                }
            }
        }

        public static ObservableCollection<Furniture> GetFurniturе(string type)
        {
            using(var bd = new ApplicationContext())
            {
                var furniture = bd.Furnitures.Where(f => f.Type == type).ToList<Furniture>();
                var fr = new ObservableCollection<Furniture>();
                furniture.ForEach(f => fr.Add(f));
                return fr;
            }
        }

        public static bool AddFurnitureInProduct()
        {
            return true;
        }

        public static bool AddProductInBasket()
        {

            return false;
        }

        public static int CountFurniturСertainType(string type)
        {
            using (var bd = new ApplicationContext())
            {
                return bd.Furnitures.Where(f => f.Type == type).Count();
            }
        }

        public static ObservableCollection<Furniture> SearchFurniture(string name)
        {
            using (var bd = new ApplicationContext())
            {
                Regex reg = new Regex(@"\w" + name + "\\w");
                ObservableCollection<Furniture> list = new ObservableCollection<Furniture>();
                bd.Furnitures.Where(f => reg.IsMatch(f.Name)).ToList().ForEach(f => list.Add(f));
                return list;
            }
        }
    }
}
