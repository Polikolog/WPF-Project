using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Infrastructur.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kyrsach_core.Infrastructur.Repository
{
    public class BasketFurnitureRepository : Repository<BasketFurniture>
    {
        public override IQueryable<BasketFurniture> GetAllItems => base.GetAllItems.Include(bf => bf.Basket).Include(bf => bf.Furniture);
        public BasketFurnitureRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
