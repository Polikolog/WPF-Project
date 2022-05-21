using System;
using System.Collections.Generic;
using Kyrsach_core.Infrastructur.Entity.Base;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model
{
    public class Like : Entity
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public List<LikeFurniture> LikeFurnitures { get; set; }
    }
}
