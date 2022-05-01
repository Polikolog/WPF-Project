using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model
{
    public class BasketProduct
    {
        public int BasketID { get; set; }
        public Basket Basket { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
