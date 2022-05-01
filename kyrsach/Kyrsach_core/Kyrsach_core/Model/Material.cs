using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model
{
    public class Material
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal? Price { get; set; }
        public List<ProductMaterial> ProductMaterials { get; set; }
    }
}
