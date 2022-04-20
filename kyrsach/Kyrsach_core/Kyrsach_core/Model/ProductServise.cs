using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model
{
    internal class ProductServise
    {
        public Guid ProductID { get; set; }
        public Product Product { get; set; }
        public Guid ServiseID { get; set; }
        public Servise Servise { get; set; }
    }
}
