using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Kyrsach_core.Infrastructur;
using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Infrastructur.Entity;
using Kyrsach_core.Model.Other;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;


// ??? - обозначает, что данная функция не была протестирована.


namespace Kyrsach_core.Model
{
    public static class DataWorker
    {
        private static UnitOfWork db = new UnitOfWork();
        private static IPasswordHasher passwordHasher = new PasswordHasher();

        //Добавление пользователя
        public static bool AddUser(string NameUser, string PasswordUser, string Adress, string Num)
        {
            //using (var db = new ApplicationContext())
            //{
                if (GetUser(NameUser, PasswordUser, Num))
                {
                    string pass = passwordHasher.HashPassword(PasswordUser);
                    db.Users.Add(new User { Name = NameUser, Password = pass, Adress = Adress, Phone = Num, IsAdmin = false, Image = "C:\\Users\\mi\\Desktop\\1\\imageDataContext\\Profile.jpg" });
                    //db.SaveChanges();

                    db.Likes.Add(new Like { UserID = db.Users.GetAllItems.OrderBy(p => p.ID).LastOrDefault().ID });
                    db.Baskets.Add(new Basket { Price = null, OrderCompleted = null, UserID = db.Users.GetAllItems.OrderBy(p => p.ID).LastOrDefault().ID });
                    //db.SaveChanges();

                    return true;
                }
                else
                    return false; 
            //}
        }

        //Получение пользователя
        public static bool GetUser(string name, string password, string phone = null)
        {
            //using(var db = new ApplicationContext())
            //{
                if (phone == null)
                {
                    if (db.Users.GetAllItems.FirstOrDefault(u => u.Name == name) != null)
                    {
                        PasswordVerificationResult ver = passwordHasher.VerifyHashedPassword(db.Users.GetAllItems.FirstOrDefault(u => u.Name == name).Password, password);
                        if (ver == PasswordVerificationResult.Success)
                        {
                            CurrentUser.setInstance(db.Users.GetAllItems.FirstOrDefault(u => u.Name == name));
                            if (CurrentUser.getInstance() != null)
                                return true;
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                {
                    var user = db.Users.GetAllItems.FirstOrDefault(u => u.Name == name || u.Password == password || u.Phone == phone);
                    if (user == null)
                        return true;
                    else
                        return false;
                }
            //}
        }

        //Смена картинки пользователя
        public static bool ChangedImageUser(string image)
        {
            using (var db = new ApplicationContext())
            {
                if (image != null)
                {
                    db.Users.Where(u => u.ID == CurrentUser.getInstance().ID).First().Image = image;
                    db.SaveChanges();
                    CurrentUser.setInstance(db.Users.Where(u => u.ID == CurrentUser.getInstance().ID).First());
                    return true;
                }
                else
                    return false;
            }
        }

        //Изменение параметров пользователя
        public static void ChangedUserParametrs(string name, string adress, string phone)
        {
            using(var db = new ApplicationContext())
            {
                var user = db.Users.First(u => u.ID == CurrentUser.getInstance().ID);
                user.Name = name;
                db.Entry<User>(user).State = EntityState.Modified;
                db.SaveChanges();
                //db.Users.Where(u => u.ID == CurrentUser.getInstance().ID).First()
            }
        }

        //Получение списка товаров определенного типа 
        public static ObservableCollection<Furniture> GetFurniturе(string type)
        {
            //using(var db = new ApplicationContext())
            //{
                var fr = new ObservableCollection<Furniture>();
                db.Furnitures.GetAllItems.Where(f => f.Type == type).ToList().ForEach(f => fr.Add(f));
                return fr;
            //}
        }

        //Получение списка понравившихся товаров пользователя
        public static Like GetLike()
        {
            using (var db = new ApplicationContext())
            {
                return db.Users.Where(u => u.ID == CurrentUser.getInstance().ID).Join(db.Likes, u => u.ID, l => l.UserID, (u, l) => new Like { ID = l.ID, UserID = u.ID}).FirstOrDefault();
            }
        }

        //Добавление товара в список понравившихся товаров
        public static void AddFurnitureInLike(Furniture furniture)
        {
            using(var db = new ApplicationContext())
            {
                if(!CheckFurnitureInLike(furniture))
                {
                    var lf = new LikeFurniture() { LikeID = GetLike().ID, FurnitureID = furniture.ID };
                    db.LikeFurnitures.Add(lf);
                    db.SaveChanges();
                }
            }
        }
        
        //Получение всех товаров в списке понравившихся
        public static ObservableCollection<Furniture> GetFurnituresInLike()
        {
            using(var db = new ApplicationContext())
            {
                ObservableCollection<Furniture> list = new ObservableCollection<Furniture>();
                db.LikeFurnitures.Where(lf => lf.LikeID == GetLike().ID).Join(db.Furnitures, lf => lf.FurnitureID, f => f.ID, (lf, f) => new Furniture { ID = f.ID, Name = f.Name, Type = f.Type, Availability = f.Availability, Category = f.Category, Color = f.Color, Width = f.Width, Length = f.Length, Height = f.Height, Description = f.Description, Image = f.Image, ImageIcon = f.ImageIcon, LivingSector = f.LivingSector, Price = f.Price, Rating = f.Rating }).ToList().ForEach(f => list.Add(f));
                return list;
            }
        }

        //Удаление товаров из списка понравившихся
        public static void DeleteFurnitureInLike(Furniture furniture = null)
        {
            using(var db = new ApplicationContext())
            {
                if(furniture == null)
                {
                    if(GetFurnituresInLike().Count != 0)
                    {
                        var list = new List<LikeFurniture>();
                        foreach(var item in GetFurnituresInLike())
                        {
                            list.Add(db.LikeFurnitures.Where(lf => lf.FurnitureID == item.ID).ToList().FirstOrDefault());
                        }
                        db.LikeFurnitures.RemoveRange(list);
                        db.SaveChanges();
                    }
                }
                else
                {
                    var del = db.LikeFurnitures.Where(lf => lf.LikeID == GetLike().ID).ToList().Where(lf => lf.FurnitureID == furniture.ID).FirstOrDefault();
                    if(del != null)
                    {
                        db.LikeFurnitures.Remove(del);
                        db.SaveChanges();
                    }
                }
            }
        }

        //Получение всех категорий мебели определенного типа
        public static ObservableCollection<Categories> GetCategories(string type)
        {
            using (var db = new ApplicationContext())
            {
                var list = new ObservableCollection<Categories>();
                db.Furnitures.Where(f => f.Type == type).ToList().ForEach(f =>
                {
                    if (list.Count > 0)
                    {
                        if (list.Where(l => l.Category == f.Category).ToList().Count <= 0)
                            list.Add(new Categories { Category = f.Category });
                    }
                    else
                        list.Add(new Categories { Category = f.Category });
                });
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
            using (var db = new ApplicationContext())
            {
                return db.Users.Where(u => u.ID == CurrentUser.getInstance().ID).Join(db.Baskets, u => u.ID, b => b.UserID, (u, b) => new Basket { ID = b.ID, Price = b.Price, OrderCompleted = b.OrderCompleted, UserID = u.ID }).FirstOrDefault();
            }
        }

        //Добавление товара в корзину
        public static void AddFurnitureInBasket(Furniture furniture)
        {
            using (var db = new ApplicationContext())
            {
                if(CheckFurnitureInBasket(furniture))
                {
                    var bf = new BasketFurniture() { BasketID = GetBasket().ID, FurnitureID = furniture.ID };
                    db.BasketFurnitures.Add(bf);
                    db.Baskets.Where(b => b == GetBasket()).First().Price += furniture.Price;
                    db.SaveChanges();
                }
            }
        }

        //Удаление товара из корзины
        public static void DeleteFuritureInBasket(Furniture furniture = null)
        {
            using (var db = new ApplicationContext())
            {
                if (furniture == null)
                {
                    if (GetFurnituresInBasket().Count != 0)
                    {
                        var list = new List<BasketFurniture>();
                        foreach (var item in GetFurnituresInBasket())
                        {
                            list.Add(db.BasketFurnitures.Where(bf => bf.FurnitureID == item.ID).ToList().FirstOrDefault());
                        }
                        db.BasketFurnitures.RemoveRange(list);
                        db.SaveChanges();
                    }
                }
                else
                {
                    var del = db.BasketFurnitures.Where(bf => bf.BasketID == GetBasket().ID).ToList().Where(bf => bf.FurnitureID == furniture.ID).FirstOrDefault();
                    if (del != null)
                    {
                        db.BasketFurnitures.Remove(del);
                        db.SaveChanges();
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
            using (var db = new ApplicationContext())
            {
                ObservableCollection<Furniture> list = new ObservableCollection<Furniture>();
                db.BasketFurnitures.Where(bf => bf.BasketID == GetBasket().ID).Join(db.Furnitures, bf => bf.FurnitureID, f => f.ID, (bf, f) => new Furniture { ID = f.ID, Name = f.Name, Type = f.Type, Availability = f.Availability, Category = f.Category, Color = f.Color, Width = f.Width, Length = f.Length, Height = f.Height, Description = f.Description, Image = f.Image, ImageIcon = f.ImageIcon, LivingSector = f.LivingSector, Price = f.Price, Rating = f.Rating }).ToList().ForEach(f => list.Add(f));
                return list;
            }
        }

        //Подсчет количества товаров определенного типа
        public static int CountFurniturСertainType(string type)
        {
            using (var db = new ApplicationContext())
            {
                return db.Furnitures.Where(f => f.Type == type).Count();
            }
        }

        //Поиск товара по поисковой строке
        public static ObservableCollection<Furniture> SearchFurniture(string name)
        {
            using (var db = new ApplicationContext())
            {
                Regex reg = new Regex(@"\w" + name + "\\w");
                ObservableCollection<Furniture> list = new ObservableCollection<Furniture>();
                db.Furnitures.Where(f => EF.Functions.Like(f.Name!, "%" + name + "%")).ToList().ForEach(f => list.Add(f));
                return list;
            }
        }

        //Добаления заказа
        public static void AddOrder()
        {
            using(var db = new ApplicationContext())
            {
                var order = new Order() { UserID = CurrentUser.getInstance().ID };
                db.Orders.Add(order);
                db.SaveChanges();
                ListOrdersCurrentUser.AddOrder(order);
            }
        }

        //Добавление товара в заказ
        public static bool AddFurnituresInOrder()
        {
            using(var db = new ApplicationContext())
            {
                if (GetFurnituresInBasket().Count != 0)
                {
                    AddOrder();
                    ICollection<OrderFurniture> list = new ObservableCollection<OrderFurniture>();
                    foreach (Furniture item in GetFurnituresInBasket())
                    {
                        db.OrderFurnitures.Add(new OrderFurniture { FurnitureID = item.ID, OrderID = ListOrdersCurrentUser.GetLastOrder().ID});
                        db.SaveChanges();
                        //d.Orders.Where(o => o.UserID == CurrentUser.getInstance().ID).Join(db.OrderFurnitures, o => o.ID, of => of.OrderID, (o, of) => new OrderFurniture { OrderID = o.ID, FurnitureID = item.ID }).First()
                    }
                    DeleteFuritureInBasket();
                    return true;
                }
                else
                    return false;
            }
        }

        //Создание коментария
        public static void CreateComment(string text, Furniture furniture)
        {
            using(var db = new ApplicationContext())
            {
                db.Comments.Add(new Comment { Text = text , UserID = CurrentUser.getInstance().ID, FurnitureID = furniture.ID});
                db.SaveChanges();
            }
        }

        //Получение всех коментариев под определенной мебелью
        public static ObservableCollection<UserComment> GetComments(Furniture furniture)
        {
            using(var db = new ApplicationContext())
            {
                var comments = new ObservableCollection<UserComment>();
                db.Comments.Where(c => c.FurnitureID == furniture.ID).Join(db.Users, c => c.UserID, u => u.ID, (c, u) => new UserComment { User = u, Comment = c }).OrderByDescending(uc => uc.Comment.ID).ToList().ForEach(c => comments.Add(c));
                return comments;
            }
        }

        //Изменение рейтинга товара
        public static void UpdateRating(int rating, Furniture furniture)
        {
            using(var db = new ApplicationContext())
            {
                int? cur = db.Furnitures.Where(f => f.ID == furniture.ID).First().Rating;
                db.Furnitures.Where(f => f.ID == furniture.ID).First().Rating = (int)((cur + rating) / 2);
                db.SaveChanges();
            }
        }
    }
}
