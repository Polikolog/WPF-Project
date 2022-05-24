using Kyrsach_core.Infrastructur.Base;
using Kyrsach_core.Infrastructur.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Kyrsach_core.Infrastructur.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Kyrsach_core.Infrastructur.Entity.Base.Entity, new()
    {
        private readonly ApplicationContext db;
        private readonly DbSet<T> dbSet;

        public Repository(ApplicationContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }

        public void Add(T item)
        {
            if (item == null)
                return;
            db.Entry(item).State = EntityState.Added;
            db.SaveChanges();
        }

        public void Delete(T item)
        {
            if(item == null)
                return;
            db.Remove(item);
            db.SaveChanges();
        }

        public T Get(int id) => GetAllItems.SingleOrDefault(i => i.ID == id);

        public virtual IQueryable<T> GetAllItems => dbSet;

        public void Update(T item)
        {
            if (item == null)
                return;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
