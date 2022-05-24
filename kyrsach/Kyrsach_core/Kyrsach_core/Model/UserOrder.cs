using Kyrsach_core.Infrastructur.Entity;

namespace Kyrsach_core.Model
{
    public class UserOrder
    {
        public Order Order { get; set; }
        public OrderFurniture OrderFurniture { get; set; }
    }
}
