using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model
{
    internal class Basket
    {
        public Guid ID { get; set; }
        public decimal Price { get; set; }
        public Guid UserID { get; set; }
        public User User { get; set; }
        public List<BasketProduct> BasketProduct { get; set; }
    }
}
