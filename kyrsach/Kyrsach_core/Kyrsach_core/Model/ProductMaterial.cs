using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model
{
    internal class ProductMaterial
    {
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int MaterialID { get; set; }
        public Material Material { get; set; }
    }
}
