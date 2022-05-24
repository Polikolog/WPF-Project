using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Infrastructur.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kyrsach_core.Infrastructur.Repository
{
    public class BasketRepository : Repository<Basket>
    {
        public override IQueryable<Basket> GetAllItems => base.GetAllItems.Include(b => (b as Basket).User).Include(b => b.BasketFurnitures);

        public BasketRepository(ApplicationContext db) : base(db)
        {
        }

    }
}
