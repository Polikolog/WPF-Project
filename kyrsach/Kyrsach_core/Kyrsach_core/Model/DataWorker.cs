using System;
using System.Collections.ObjectModel;
using System.Linq;
using Kyrsach_core.Model.Base;

namespace Kyrsach_core.Model
{
    public static class DataWorker
    {
        public static void AddUser()
        {
            using (var bd = new ApplicationContext())
            {
                bd.Users.Add(new User { Name = "1", Adress = "1", ID = 1, Password = "1", Phone = 1 });
            }
        }

        public static bool GetUser(string name, string password)
        {
            using(var bd = new ApplicationContext())
            {
                var user = bd.Users.FirstOrDefault(u => u.Name == name && u.Password == password);
                if(user != null)
                {
                    CurrentUser.setInstance(user);
                    return true;
                }
                else
                    return false;
            }
        }

    }
}
