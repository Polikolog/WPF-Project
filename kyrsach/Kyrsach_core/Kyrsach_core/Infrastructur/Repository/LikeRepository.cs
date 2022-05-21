using Kyrsach_core.Model;
using Kyrsach_core.Model.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kyrsach_core.Infrastructur.Repository
{
    public class LikeRepository : Repository<Like>
    {
        public override IQueryable<Like> GetAllItems => base.GetAllItems.Include(l => l.User).Include(l => l.LikeFurnitures).Include(l => l.UserID);

        public LikeRepository(ApplicationContext db) : base(db)
        {
        }

    }
}
