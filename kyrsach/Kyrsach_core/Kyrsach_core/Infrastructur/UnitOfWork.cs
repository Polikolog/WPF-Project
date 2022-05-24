using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Infrastructur.Repository;
using System;

namespace Kyrsach_core.Infrastructur
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationContext db = new ApplicationContext();
        private UserRepository userRepository;
        private LikeRepository likeRepository;
        private BasketRepository basketRepoitory;
        private FurnitureRepository furnitureRepository;
        private BasketFurnitureRepository basketFurnitureRepository;
        private LikeFurnitureRepository likeFurnitureRepository;
        private OrderRepository orderRepository;
        private OrderFurnitureRepository orderFurnitureRepository;
        private CommentRepository commentRepository;
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

        public BasketRepository Baskets
        {
            get
            {
                if(basketRepoitory == null)
                    basketRepoitory = new BasketRepository(db);
                return basketRepoitory;
            }
        }

        public FurnitureRepository Furnitures
        {
            get
            {
                if(furnitureRepository == null)
                    furnitureRepository = new FurnitureRepository(db);
                return furnitureRepository;
            }
        }
        
        public BasketFurnitureRepository BasketFurnitures
        {
            get
            {
                if (basketFurnitureRepository == null)
                    basketFurnitureRepository = new BasketFurnitureRepository(db);
                return basketFurnitureRepository;
            }
        }

        public LikeFurnitureRepository LikeFurnitures
        {
            get
            {
                if (likeFurnitureRepository == null)
                    likeFurnitureRepository = new LikeFurnitureRepository(db);
                return likeFurnitureRepository;
            }
        }

        public OrderRepository Order
        {
            get
            {
                if(orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }

        public OrderFurnitureRepository OrderFurnitures
        {
            get
            {
                if (orderFurnitureRepository == null)
                    orderFurnitureRepository = new OrderFurnitureRepository(db);
                return orderFurnitureRepository;
            }
        }

        public CommentRepository Comment
        {
            get
            {
                if(commentRepository == null)
                    commentRepository = new CommentRepository(db);
                return commentRepository;
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
