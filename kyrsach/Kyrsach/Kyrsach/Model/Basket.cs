using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach.Model
{
    internal class Basket
    {
        public  Guid ID { get; set; }
        public List<Product> Products { get; set; }
    }
}
