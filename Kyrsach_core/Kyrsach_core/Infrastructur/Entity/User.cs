using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Infrastructur.Entity
{
    public class User : Kyrsach_core.Infrastructur.Entity.Base.Entity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public bool IsAdmin { get; set; }
        public Basket Basket { get; set; }
        public Like Like { get; set; }
        public List<Order> Orders { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
