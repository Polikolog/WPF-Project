using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Kyrsach_core.Model.Base;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;


// ??? - обозначает, что данная функция не была протестирована.


namespace Kyrsach_core.Model
{
    public static class DataWorker
    {
        private static IPasswordHasher passwordHasher = new PasswordHasher();

        //Добавление пользователя
        public static bool AddUser(string NameUser, string PasswordUser, string Adress, int? Num)
        {
            using (var bd = new ApplicationContext())
            {
                if (GetUser(NameUser, PasswordUser, Num))
                {
                    string pass = passwordHasher.HashPassword(PasswordUser);
                    bd.Users.Add(new User { Name = NameUser, Password = pass, Adress = Adress, Phone = Num, IsAdmin = false, Image = "C:\\Users\\mi\\Desktop\\1\\imageDataContext\\Profile.jpg" });
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

        //Удаление товара из корзины
        public static void DeleteFuritureInBasket(Furniture furniture = null)
        {
            using (var bd = new ApplicationContext())
            {
                if (furniture == null)
                {
                    if (GetFurnituresInBasket().Count != 0)
                    {
                        var list = new List<BasketFurniture>();
                        foreach (var item in GetFurnituresInBasket())
                        {
                            list = bd.BasketFurnitures.Where(bf => bf.FurnitureID == item.ID).ToList();
                        }
                        bd.BasketFurnitures.RemoveRange(list);
                        bd.SaveChanges();
                    }
                }
                else
                {
                    var del = bd.BasketFurnitures.Where(bf => bf.BasketID == GetBasket().ID).ToList().Where(bf => bf.FurnitureID == furniture.ID).FirstOrDefault();
                    if (del != null)
                    {
                        bd.BasketFurnitures.Remove(del);
                        bd.SaveChanges();
                    }
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

        //Получение всех товаров из корзины
        public static ObservableCollection<Furniture> GetFurnituresInBasket()
        {
            using (var bd = new ApplicationContext())
            {
                ObservableCollection<Furniture> list = new ObservableCollection<Furniture>();
                bd.BasketFurnitures.Where(bf => bf.BasketID == GetBasket().ID).Join(bd.Furnitures, bf => bf.FurnitureID, f => f.ID, (bf, f) => new Furniture { ID = f.ID, Name = f.Name, Type = f.Type, Availability = f.Availability, Category = f.Category, Color = f.Color, Width = f.Width, Length = f.Length, Height = f.Height, Description = f.Description, Image = f.Image, ImageIcon = f.ImageIcon, LivingSector = f.LivingSector, Price = f.Price, Rating = f.Rating }).ToList().ForEach(f => list.Add(f));
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

        //Добаления заказа
        public static void AddOrder()
        {
            using(var bd = new ApplicationContext())
            {
                var order = new Order() { UserID = CurrentUser.getInstance().ID };
                bd.Orders.Add(order);
                bd.SaveChanges();
                ListOrdersCurrentUser.AddOrder(order);
            }
        }

        //Добавление товара в заказ
        public static bool AddFurnituresInOrder()
        {
            using(var bd = new ApplicationContext())
            {
                if (GetFurnituresInBasket().Count != 0)
                {
                    AddOrder();
                    ICollection<OrderFurniture> list = new ObservableCollection<OrderFurniture>();
                    foreach (Furniture item in GetFurnituresInBasket())
                    {
                        bd.OrderFurnitures.Add(new OrderFurniture { FurnitureID = item.ID, OrderID = ListOrdersCurrentUser.GetLastOrder().ID});
                        bd.SaveChanges();
                        //d.Orders.Where(o => o.UserID == CurrentUser.getInstance().ID).Join(bd.OrderFurnitures, o => o.ID, of => of.OrderID, (o, of) => new OrderFurniture { OrderID = o.ID, FurnitureID = item.ID }).First()
                    }
                    DeleteFuritureInBasket();
                    return true;
                }
                else
                    return false;
            }
        }

        //Смена картинки пользователя
        public static bool ChangedImageUser(string image)
        {
            using (var bd = new ApplicationContext())
            {
                if (image != null)
                {
                    bd.Users.Where(u => u.ID == CurrentUser.getInstance().ID).First().Image = image;
                    bd.SaveChanges();
                    CurrentUser.setInstance(bd.Users.Where(u => u.ID == CurrentUser.getInstance().ID).First());
                    return true;
                }
                else
                    return false;
            }
        }

        //Создание коментария
        public static void CreateComment(string text, Furniture furniture)
        {
            using(var bd = new ApplicationContext())
            {
                bd.Comments.Add(new Comment { Text = text , UserID = CurrentUser.getInstance().ID, FurnitureID = furniture.ID});
                bd.SaveChanges();
            }
        }

        //Получение всех коментариев под определенной мебелью
        public static ObservableCollection<Comment> GetComments(Furniture furniture)
        {
            using(var bd = new ApplicationContext())
            {
                var comments = new ObservableCollection<Comment>();
                bd.Comments.Where(c => c.FurnitureID == furniture.ID).ToList().ForEach(c => comments.Add(c));
                return comments;
            }
        }

        //Получение пользователей оставивших коментарий
        public static ObservableCollection<User> GetUsers(Furniture furniture)
        {
            using(var bd = new ApplicationContext())
            {
                ObservableCollection<User> users = new ObservableCollection<User>();
                foreach (var item in GetComments(furniture))
                {
                    users.Add(bd.Users.Where(u => u.ID == item.UserID).First());
                }
                return users;
            }
        }
    }
}
