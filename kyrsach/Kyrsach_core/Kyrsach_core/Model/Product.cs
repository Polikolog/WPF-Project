using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model
{
    public class Product
    {
        public int ID { get; set; }
        public int FurnitureID { get; set; }
        public Furniture Furniture { get; set; }
        public decimal? Price { get; set; }
        public List<BasketProduct> BasketProducts { get; set; }
        public List<ProductMaterial> ProductMaterials { get; set; }
        public List<ProductServise> ProductServises { get; set; }
    }
}
