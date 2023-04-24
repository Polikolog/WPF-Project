using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Infrastructur.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kyrsach_core.Infrastructur.Repository
{
    public class OrderRepository : Repository<Order>
    {
        public override IQueryable<Order> GetAllItems => base.GetAllItems.Include(o => o.User).Include(o => o.OrderFurnitures);
        public OrderRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
