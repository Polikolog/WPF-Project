using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model
{
    public class LikeFurniture
    {
        public int LikeID { get; set; }
        public Like Like { get; set; }
        public int FurnitureID { get; set; }
        public Furniture Furniture { get; set; }
    }
}
