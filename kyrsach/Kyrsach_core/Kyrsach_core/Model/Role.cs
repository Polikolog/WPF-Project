using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model
{
    internal class Role
    {
        public int ID { get; set; }
        public string UserRole { get; set; }
        public List<User> Users { get; set; }
    }
}
