using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Infrastructur.Entity
{
    public class BasketFurniture : Base.Entity
    {
        public int BasketID { get; set; }
        public Basket Basket { get; set; }
        public int FurnitureID { get; set; }
        public Furniture Furniture { get; set; }
        public int CountFurniture { get; set; }
    }
}
