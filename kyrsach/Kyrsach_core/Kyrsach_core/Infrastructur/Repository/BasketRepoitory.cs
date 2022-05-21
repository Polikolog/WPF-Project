using Kyrsach_core.Model;
using Kyrsach_core.Model.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Infrastructur.Repository
{
    public class BasketRepoitory : Repository<Basket>
    {
        public override IQueryable<Basket> GetAllItems => base.GetAllItems.Include(b => (b as Basket).User).Include(b => b.OrderCompleted).Include(b => b.BasketFurnitures).Include(b => b.Price).Include(b => b.UserID);

        public BasketRepoitory(ApplicationContext db) : base(db)
        {
        }

    }
}
