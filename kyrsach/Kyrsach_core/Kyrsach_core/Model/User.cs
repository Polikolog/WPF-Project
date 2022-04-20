using System;
#pragma warning disable CS8618

namespace Kyrsach_core.Model
{
    internal class User
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Adress { get; set; }
        public int Phone { get; set; }
        //public Guid BasketID { get; set; }

        public Basket Basket { get; set; }

    }
}
