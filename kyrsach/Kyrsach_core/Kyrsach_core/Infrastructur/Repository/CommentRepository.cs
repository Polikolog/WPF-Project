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
    public class CommentRepository : Repository<Comment>
    {
        public override IQueryable<Comment> GetAllItems => base.GetAllItems.Include(c => c.User).Include(c => c.Furniture);
        public CommentRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
