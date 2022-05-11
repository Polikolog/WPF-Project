using System;
#pragma warning disable CS8618

namespace Kyrsach_core.Model
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Adress { get; set; }
        public int? Phone { get; set; }
        public string Image { get; set; }
        public Basket Basket { get; set; }
        public Like Like { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }

    }
}
