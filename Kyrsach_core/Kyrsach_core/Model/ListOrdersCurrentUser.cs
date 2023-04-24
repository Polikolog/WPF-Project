using Kyrsach_core.Infrastructur.Entity;
using System.Collections.ObjectModel;
using System.Linq;

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
