using Kyrsach_core.Model;
using Kyrsach_core.Model.Base;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kyrsach_core.Infrastructur.Repository
{
    public class UserRepository : Repository<User>
    {
        private IPasswordHasher passwordHasher = new PasswordHasher();

        public override IQueryable<User> GetAllItems => base.GetAllItems.Include(item => (item as User).Name).Include(item => (item as User).IsAdmin).Include(item => (item as User).Adress).Include(item => (item as User).Basket).Include(item => (item as User).Orders).Include(item => (item as User).Like).Include(item => (item as User).Image).Include(item => (item as User).Comments).Include(item => (item as User).Password).Include(item => (item as User).Phone);

        public UserRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
