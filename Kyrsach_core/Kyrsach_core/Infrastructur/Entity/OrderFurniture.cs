using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Infrastructur.Entity
{
    public class OrderFurniture : Entity.Base.Entity
    {
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public int FurnitureID { get; set; }
        public Furniture Furniture { get; set; }
    }
}
