using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Model
{
    public static class ListOrdersCurrentUser
    {
        private static ObservableCollection<Order> list = new ObservableCollection<Order>();

        public static void AddOrder(Order order)
        {
            list.Add(order);
        }

        public static void DeleteOrder(Order order)
        {
            list.Remove(order);
        }

        public static Order GetLastOrder()
        {
            return list.LastOrDefault();
        }
    }
}
