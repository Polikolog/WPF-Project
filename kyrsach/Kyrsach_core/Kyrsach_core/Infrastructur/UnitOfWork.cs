using Kyrsach_core.Infrastructur.Repository;
using Kyrsach_core.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Infrastructur
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationContext db = new ApplicationContext();
        private UserRepository userRepository;
        private LikeRepository likeRepository;
        private BasketRepoitory basketRepoitory;

        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public LikeRepository Likes
        {
            get
            {
                if(likeRepository == null)
                    likeRepository = new LikeRepository(db);
                return likeRepository;
            }
        }

        public BasketRepoitory Baskets
        {
            get
            {
                if(basketRepoitory == null)
                    basketRepoitory = new BasketRepoitory(db);
                return basketRepoitory;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private bool _Disposed;
        protected virtual void Dispose(bool Disposing)
        {
            if (!Disposing || _Disposed) return;
            _Disposed = true;
        }
    }
}
