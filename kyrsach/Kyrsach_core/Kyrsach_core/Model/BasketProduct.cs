using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model
{
    internal class BasketProduct
    {
        public Guid BasketID { get; set; }
        public Basket Basket { get; set; }
        public Guid ProductID { get; set; }
        public Product Product { get; set; }
    }
}
