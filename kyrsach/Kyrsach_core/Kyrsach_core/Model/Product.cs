using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model
{
    internal class Product
    {
        public Guid ID { get; set; }
        public Guid FurnitureID { get; set; }
        public Furniture Furniture { get; set; }
        public decimal? Price { get; set; }
        public List<BasketProduct> BasketProducts { get; set; }
        public List<ProductMaterial> ProductMaterials { get; set; }
    }
}
