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
        public static void AddFurnitureInViewed(Furniture furniture)
        {
            if(CheckFurniture.Where(f => f.ID == furniture.ID).Count() <= 0)
            {
                CheckFurniture.Add(furniture);
            }
        }

        public static ObservableCollection<Furniture> FurnitureInComparison = new ObservableCollection<Furniture>();

        public static ObservableCollection<string> GetTypesInComparison()
        {
            var list = new ObservableCollection<string>();
            foreach(var item in FurnitureInComparison)
            {
                list.Add(item.Type.ToString());
            }
            return list;
        }
    }
}
