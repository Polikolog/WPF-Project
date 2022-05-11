using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Kyrsach_core.Model.Base;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kyrsach_core.Model
{
    public static class DataWorker
    {
        private static IPasswordHasher passwordHasher = new PasswordHasher();
        public static void CreateRole()
        {
            using(var bd = new ApplicationContext())
            {
                if (bd.Roles.ToList() == null)
                {
                    bd.Roles.Add(new Role { UserRole = "Пользователь", ID = 1 });
                    bd.SaveChanges();

                    bd.Roles.Add(new Role { UserRole = "Админ", ID = 2 });
                    bd.SaveChanges();
                }
            }
        }

        //Добавление пользователя
        public static bool AddUser(string NameUser, string PasswordUser, string Adress, int? Num)
        {
            using (var bd = new ApplicationContext())
            {
                if (GetUser(NameUser, PasswordUser, Num))
                {
                    string pass = passwordHasher.HashPassword(PasswordUser);
                    bd.Users.Add(new User { Name = NameUser, Password = pass, Adress = Adress, Phone = Num, RoleID = 1 });
                    bd.SaveChanges();

                    bd.Likes.Add(new Like { UserID = bd.Users.OrderBy(p => p.ID).LastOrDefault().ID });
                    bd.Baskets.Add(new Basket { Price = null, OrderCompleted = null, UserID = bd.Users.OrderBy(p => p.ID).LastOrDefault().ID });
                    bd.SaveChanges();

                    return true;
                }
                else
                    return false; 
            }
        }

        //Получение пользователя
        public static bool GetUser(string name, string password, int? phone = null)
        {
            using(var bd = new ApplicationContext())
            {
                if (phone == null)
                {
                    PasswordVerificationResult ver = passwordHasher.VerifyHashedPassword(bd.Users.Where(u => u.Name == name).FirstOrDefault().Password, password);
                    if (ver == PasswordVerificationResult.Success)
                    {
                        CurrentUser.setInstance(bd.Users.Where(u => u.Name == name).First());
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

        //Получение списка товаров определенного типа 
        public static ObservableCollection<Furniture> GetFurniturе(string type)
        {
            using(var bd = new ApplicationContext())
            {
                var fr = new ObservableCollection<Furniture>();
                bd.Furnitures.Where(f => f.Type == type).ToList().ForEach(f => fr.Add(f));
                return fr;
            }
        }

        //Получение списка понравившихся товаров пользователя
        public static Like GetLike()
        {
            using (var bd = new ApplicationContext())
            {
                return bd.Users.Where(u => u.ID == CurrentUser.getInstance().ID).Join(bd.Likes, u => u.ID, l => l.UserID, (u, l) => new Like { ID = l.ID, UserID = u.ID}).FirstOrDefault();
            }
        }

        //Добавление товара в список понравившихся товаров
        public static void AddFurnitureInLike(Furniture furniture)
        {
            using(var bd = new ApplicationContext())
            {
                if(!CheckFurnitureInLike(furniture))
                {
                    var lf = new LikeFurniture() { LikeID = GetLike().ID, FurnitureID = furniture.ID };
                    bd.LikeFurnitures.Add(lf);
                    bd.SaveChanges();
                }
            }
        }
        
        //Получение всех товаров в списке понравившихся
        public static ObservableCollection<Furniture> GetFurnituresInLike()
        {
            using(var bd = new ApplicationContext())
            {
                ObservableCollection<Furniture> list = new ObservableCollection<Furniture>();
                bd.LikeFurnitures.Where(lf => lf.LikeID == GetLike().ID).Join(bd.Furnitures, lf => lf.FurnitureID, f => f.ID, (lf, f) => new Furniture { ID = f.ID, Name = f.Name, Type = f.Type, Availability = f.Availability, Category = f.Category, Color = f.Color, Width = f.Width, Length = f.Length, Height = f.Height, Description = f.Description, Image = f.Image, ImageIcon = f.ImageIcon, LivingSector = f.LivingSector, Price = f.Price, Rating = f.Rating }).ToList().ForEach(f => list.Add(f));
                return list;
            }
        }

        //Проверка нахождения товара в списке понравившегося
        private static bool CheckFurnitureInLike(Furniture furniture)
        {
            foreach(var item in GetFurnituresInLike())
            {
                if (item.ID == furniture.ID)
                    return true;        
            }
            return false;
        }

        //Получение корзины текущего пользователя
        public static Basket GetBasket()
        {
            using (var bd = new ApplicationContext())
            {
                return bd.Users.Where(u => u.ID == CurrentUser.getInstance().ID).Join(bd.Baskets, u => u.ID, b => b.UserID, (u, b) => new Basket { ID = b.ID, Price = b.Price, OrderCompleted = b.OrderCompleted, UserID = u.ID }).FirstOrDefault();
            }
        }

        //Добавление товара в корзину
        public static void AddFurnitureInBasket(Furniture furniture)
        {
            using (var bd = new ApplicationContext())
            {
                if(CheckFurnitureInBasket(furniture))
                {
                    var bf = new BasketFurniture() { BasketID = GetBasket().ID, FurnitureID = furniture.ID };
                    bd.BasketFurnitures.Add(bf);
                    bd.Baskets.Where(b => b == GetBasket()).First().Price += furniture.Price;
                    bd.SaveChanges();
                }
            }
        }

        //Проверка нахождения товара в корзине 
        private static bool CheckFurnitureInBasket(Furniture furniture)
        {
            foreach(var item in GetFurnituresInBasket())
            {
                if (item.ID == furniture.ID)
                    return false;
            }
            return true;
        }

        //Возвращение всех товаров из корзины
        public static ObservableCollection<Furniture> GetFurnituresInBasket()
        {
            using (var bd = new ApplicationContext())
            {
                ObservableCollection<Furniture> list = new ObservableCollection<Furniture>();
                bd.BasketFurnitures.Where(bf => bf.BasketID == GetBasket().ID).Join(bd.Furnitures, bf => bf.FurnitureID, f => f.ID, (bf, f) => new Furniture { ID = f.ID, Name = f.Name, Type = f.Type, Availability = f.Availability, Category = f.Category, Color = f.Color, Width = f.Width, Length = f.Length, Height = f.Height, Description = f.Description, Image = f.Image, ImageIcon = f.ImageIcon, LivingSector = f.LivingSector, Price = f.Price, Rating = f.Rating }).ToList().ForEach(f => list.Add(f)); 
                //bd.Furnitures.Join(bd.BasketFurnitures, f => f.ID, bf => bf.FurnitureID, (f, bf) => new Furniture { ID = f.ID, Name = f.Name, Type = f.Type, Availability = f.Availability, Category = f.Category, Color = f.Color, Width = f.Width, Length = f.Length, Height = f.Height, Description = f.Description, Image = f.Image, ImageIcon = f.ImageIcon, LivingSector = f.LivingSector, Price = f.Price, Rating = f.Rating }).ToList().ForEach(f => list.Add(f));
                return list;
            }
        }

        //Подсчет количества товаров определенного типа
        public static int CountFurniturСertainType(string type)
        {
            using (var bd = new ApplicationContext())
            {
                return bd.Furnitures.Where(f => f.Type == type).Count();
            }
        }

        //Поиск товара по поисковой строке
        public static ObservableCollection<Furniture> SearchFurniture(string name)
        {
            using (var bd = new ApplicationContext())
            {
                Regex reg = new Regex(@"\w" + name + "\\w");
                ObservableCollection<Furniture> list = new ObservableCollection<Furniture>();
                bd.Furnitures.Where(f => EF.Functions.Like(f.Name!, "%" + name + "%")).ToList().ForEach(f => list.Add(f));
                return list;
            }
        }
    }
}
