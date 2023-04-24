using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Infrastructur.Entity
{
    public class Like : Kyrsach_core.Infrastructur.Entity.Base.Entity
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public List<LikeFurniture> LikeFurnitures { get; set; }
    }
}
