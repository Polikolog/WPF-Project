using Microsoft.EntityFrameworkCore;
using Kyrsach_core.Model;

namespace Kyrsach_core.Model.Base
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProduct> BasketsProduct { get; set;}
        public DbSet<Product> Products { get; set; }
        public DbSet<Furniture> Furnitures { get; set; }
        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Servise> Servises { get; set; }

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

            //Basket with Product
            modelBuilder.Entity<BasketProduct>().HasKey(bp => new { bp.BasketID, bp.ProductID });
            modelBuilder.Entity<BasketProduct>().HasOne<Product>(bp => bp.Product).WithMany(p => p.BasketProducts).HasForeignKey(bp => bp.ProductID);
            modelBuilder.Entity<BasketProduct>().HasOne<Basket>(sc => sc.Basket).WithMany(b => b.BasketProduct).HasForeignKey(bp => bp.BasketID);

            //Product with Material
            modelBuilder.Entity<ProductMaterial>().HasKey(pm => new { pm.ProductID, pm.MaterialID });
            modelBuilder.Entity<ProductMaterial>().HasOne<Product>(pm => pm.Product).WithMany(p => p.ProductMaterials).HasForeignKey(pm => pm.ProductID);
            modelBuilder.Entity<ProductMaterial>().HasOne<Material>(pm => pm.Material).WithMany(m => m.ProductMaterials).HasForeignKey(pm => pm.MaterialID);

            //Product with Furniture
            modelBuilder.Entity<Product>().HasOne<Furniture>(p => p.Furniture).WithMany(f => f.Products).HasForeignKey(p => p.FurnitureID);


        }
    }
}
