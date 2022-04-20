using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach.Model
{
    internal class User
    {
        private string username;
        private string password;

        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public string Adress { get; set; }

        public int Num { get; set; }

        public Guid BasketID { get; set; }
    }
}
