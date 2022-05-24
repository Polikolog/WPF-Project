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
    public class LikeFurnitureRepository : Repository<LikeFurniture>
    {
        public override IQueryable<LikeFurniture> GetAllItems => base.GetAllItems.Include(lf => lf.Furniture).Include(lf => lf.Like);
        public LikeFurnitureRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
