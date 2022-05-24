//using Microsoft.EntityFrameworkCore;
//using Kyrsach_core.Model;

//namespace Kyrsach_core.Model.Base
//{
//    public class ApplicationContext : DbContext
//    {
//        public DbSet<User> Users { get; set; }
//        public DbSet<Basket> Baskets { get; set; }
//        public DbSet<Furniture> Furnitures { get; set; }
//        public DbSet<Like> Likes { get; set; }
//        public DbSet<BasketFurniture> BasketFurnitures { get; set; }
//        public DbSet<LikeFurniture> LikeFurnitures { get; set; }
//        public DbSet<Order> Orders { get; set; }
//        public DbSet<OrderFurniture> OrderFurnitures { get; set; }
//        public DbSet<Comment> Comments { get; set; }

//        public ApplicationContext()
//        {
//            Database.EnsureCreated();
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer(@"Server = DESKTOP-5NE69RS\SQLEXPR; Database = Kyrsach_Core; Trusted_Connection = True;");
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            //User with Basket
//            modelBuilder.Entity<User>().HasOne<Basket>(u => u.Basket).WithOne(b => b.User).HasForeignKey<Basket>(b => b.UserID);

//            //User with Like
//            modelBuilder.Entity<User>().HasOne<Like>(u => u.Like).WithOne(l => l.User).HasForeignKey<Like>(l => l.UserID);

//            //Basket with Furniture
//            modelBuilder.Entity<BasketFurniture>().HasKey(bf => new { bf.BasketID, bf.FurnitureID });
//            modelBuilder.Entity<BasketFurniture>().HasOne<Furniture>(bf => bf.Furniture).WithMany(f => f.BasketFurnitures).HasForeignKey(bf => bf.FurnitureID);
//            modelBuilder.Entity<BasketFurniture>().HasOne<Basket>(bf => bf.Basket).WithMany(b => b.BasketFurnitures).HasForeignKey(bf => bf.BasketID);

//            //Like with Furniture
//            modelBuilder.Entity<LikeFurniture>().HasKey(lf => new { lf.FurnitureID, lf.LikeID });
//            modelBuilder.Entity<LikeFurniture>().HasOne<Furniture>(lf => lf.Furniture).WithMany(f => f.LikeFurnitures).HasForeignKey(lf => lf.FurnitureID);
//            modelBuilder.Entity<LikeFurniture>().HasOne<Like>(lf => lf.Like).WithMany(l => l.LikeFurnitures).HasForeignKey(lf => lf.LikeID);

//            //Order with Furniture
//            modelBuilder.Entity<OrderFurniture>().HasKey(of => new { of.OrderID, of.FurnitureID });
//            modelBuilder.Entity<OrderFurniture>().HasOne<Furniture>(of => of.Furniture).WithMany(f => f.OrderFurnitures).HasForeignKey(of => of.FurnitureID);
//            modelBuilder.Entity<OrderFurniture>().HasOne<Order>(of => of.Order).WithMany(o => o.OrderFurnitures).HasForeignKey(of => of.OrderID);

//            //Order with User
//            modelBuilder.Entity<Order>().HasOne<User>(c => c.User).WithMany(u => u.Orders).HasForeignKey(c => c.UserID);

//            //User with Comment
//            modelBuilder.Entity<Comment>().HasOne<User>(c => c.User).WithMany(u => u.Comments).HasForeignKey(c => c.UserID);

//            //Commment with Furniture
//            modelBuilder.Entity<Comment>().HasOne<Furniture>(c => c.Furniture).WithMany(f => f.Comments).HasForeignKey(c => c.FurnitureID);
//        }
//    }
//}
