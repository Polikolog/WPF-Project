using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model
{
    public static class CurrentUser
    {
        private static User instance;
        private static object syncRoot = new Object();


        public static User getInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new User();
                }
            }
            return instance;
        }
        public static void setInstance(User user)
        {
            if(instance == null)
            {
                lock(syncRoot)
                {
                    if (instance == null)
                        instance = user;
                }
            }
        }

        public static ObservableCollection<Furniture> CheckFurniture = new ObservableCollection<Furniture>();
    }
}
