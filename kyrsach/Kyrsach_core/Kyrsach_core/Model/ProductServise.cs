using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model
{
    public class ProductServise
    {
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int ServiseID { get; set; }
        public Servise Servise { get; set; }
    }
}
