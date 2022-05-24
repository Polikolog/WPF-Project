using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Infrastructur.Entity
{
    public class Comment : Kyrsach_core.Infrastructur.Entity.Base.Entity
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int FurnitureID { get; set; }
        public Furniture Furniture { get; set; }
    }
}
