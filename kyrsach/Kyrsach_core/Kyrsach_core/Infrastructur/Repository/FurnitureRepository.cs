using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Infrastructur.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kyrsach_core.Infrastructur.Repository
{
    public class FurnitureRepository : Repository<Furniture>
    {
        public override IQueryable<Furniture> GetAllItems => base.GetAllItems.Include(f => f.BasketFurnitures).Include(f => f.LikeFurnitures).Include(f => f.OrderFurnitures).Include(f => f.Comments);
        public FurnitureRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
