using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Infrastructur.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Infrastructur.Repository
{
    public class OrderFurnitureRepository : Repository<OrderFurniture>
    {
        public override IQueryable<OrderFurniture> GetAllItems => base.GetAllItems.Include(of => of.Furniture).Include(o => o.Order);
        public OrderFurnitureRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
