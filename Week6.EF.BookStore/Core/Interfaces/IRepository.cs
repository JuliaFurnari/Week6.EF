using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6.EF.BookStore.Core.Interfaces
{
    interface IRepository<T>
    {
        List<T> Fetch();
        T GetById(int id);
        bool Add(T item);
        bool Update(T item);
        bool Delete(T item);
    }
}
