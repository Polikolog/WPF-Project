using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyrsach_core.Infrastructur.Repository.Base
{
    interface IRepository<T> where T : class
    {
        void Add(T item); // создание объекта
        void Update(T item); // обновление объекта
        void Delete(T item); // удаление объекта
        T Get(int id);
    }
}
