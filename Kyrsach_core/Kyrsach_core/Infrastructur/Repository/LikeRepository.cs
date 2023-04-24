using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Infrastructur.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kyrsach_core.Infrastructur.Repository
{
    public class LikeRepository : Repository<Like>
    {
        public override IQueryable<Like> GetAllItems => base.GetAllItems.Include(l => l.User).Include(l => l.LikeFurnitures);

        public LikeRepository(ApplicationContext db) : base(db)
        {
        }

    }
}
