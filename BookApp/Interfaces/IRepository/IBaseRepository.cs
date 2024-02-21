using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Interfaces.IRepository
{
   public interface IBaseRepository<T> where T:class
    {
        T Get(int Id);
        List<T> GetAll();
        void Add(T entity);
        T GetByPredicate(Func<T, bool> predicate);
        void Delete(T entity);
       void SaveChanges();



    }
}
