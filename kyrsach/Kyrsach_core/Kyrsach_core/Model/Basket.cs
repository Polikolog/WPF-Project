using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model
{
    public class Basket
    {
        public int ID { get; set; }
        public decimal? Price { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public List<BasketFurniture> BasketFurnitures { get; set; }
        public bool? OrderCompleted { get; set; }
    }
}
