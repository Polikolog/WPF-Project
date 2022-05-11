using Microsoft.EntityFrameworkCore;
using Kyrsach_core.Model;

namespace Kyrsach_core.Model.Base
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Furniture> Furnitures { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Servise> Servises { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BasketFurniture> BasketFurnitures { get; set; }
        public DbSet<LikeFurniture> LikeFurnitures { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = DESKTOP-5NE69RS\SQLEXPR; Database = Kyrsach_Core; Trusted_Connection = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User with Basket
            modelBuilder.Entity<User>().HasOne<Basket>(u => u.Basket).WithOne(b => b.User).HasForeignKey<Basket>(b => b.UserID);

            //User with Like
            modelBuilder.Entity<User>().HasOne<Like>(u => u.Like).WithOne(l => l.User).HasForeignKey<Like>(l => l.UserID);

            //Basket with Furniture
            modelBuilder.Entity<BasketFurniture>().HasKey(bf => new { bf.BasketID, bf.FurnitureID });
            modelBuilder.Entity<BasketFurniture>().HasOne<Furniture>(bf => bf.Furniture).WithMany(f => f.BasketFurnitures).HasForeignKey(bf => bf.FurnitureID);
            modelBuilder.Entity<BasketFurniture>().HasOne<Basket>(bf => bf.Basket).WithMany(b => b.BasketFurnitures).HasForeignKey(bf => bf.BasketID);

            //Like with Furniture
            modelBuilder.Entity<LikeFurniture>().HasKey(lf => new { lf.FurnitureID, lf.LikeID });
            modelBuilder.Entity<LikeFurniture>().HasOne<Furniture>(lf => lf.Furniture).WithMany(f => f.LikeFurnitures).HasForeignKey(lf => lf.FurnitureID);
            modelBuilder.Entity<LikeFurniture>().HasOne<Like>(lf => lf.Like).WithMany(l => l.LikeFurnitures).HasForeignKey(lf => lf.LikeID);

            ////Product with Servise
            //modelBuilder.Entity<ProductServise>().HasKey(ps => new { ps.ProductID, ps.ServiseID });
            //modelBuilder.Entity<ProductServise>().HasOne<Product>(ps => ps.Product).WithMany(p => p.ProductServises).HasForeignKey(ps => ps.ProductID);
            //modelBuilder.Entity<ProductServise>().HasOne<Servise>(ps => ps.Servise).WithMany(s => s.ProductServises).HasForeignKey(ps => ps.ServiseID);

            //User with Role
            modelBuilder.Entity<User>().HasOne<Role>(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleID);
        }
    }
}
