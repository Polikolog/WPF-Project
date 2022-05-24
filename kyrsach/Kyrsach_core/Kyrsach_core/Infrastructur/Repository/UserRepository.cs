using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Infrastructur.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kyrsach_core.Infrastructur.Repository
{
    public class UserRepository : Repository<User>
    {
        private IPasswordHasher passwordHasher = new PasswordHasher();

        public override IQueryable<User> GetAllItems => base.GetAllItems.Include(item => (item as User).Basket).Include(item => (item as User).Orders).Include(item => (item as User).Like).Include(item => (item as User).Comments);

        public UserRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
