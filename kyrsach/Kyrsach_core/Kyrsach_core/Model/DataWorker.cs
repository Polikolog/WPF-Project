using System;
using Kyrsach_core.Model.Base;

namespace Kyrsach_core.Model
{
    public static class DataWorker
    {
        public static void AddUser()
        {
            using (var bd = new ApplicationContext())
            {
                bd.Users.Add(new User { Name = "1", Adress = "1", ID = Guid.NewGuid(), Password = "1", Phone = 1 });
            }
        }

    }
}
