using BookApp.DataLayer;
using BookApp.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Repository
{
    public class Repository<T> : IBaseRepository<T> where T:class
    {
        private readonly AppDatabaseContext _appDatabaseContext;
        private readonly DbSet<T> _table; 
        public Repository(AppDatabaseContext DbContext)
        {
            _appDatabaseContext = DbContext;
            _table = DbContext.Set<T>();
            
        }
        public void Add(T entity)
        {
            if (entity != null)
            {
                _table.Add(entity);

            }

        }

        public void Delete(T entity)
        {
            if (entity != null)
            {
                _table.Remove(entity);

            }
        }

        public T Get(int Id)
        {
          return _table.Find(Id); 
        }

        public T GetByPredicate(Func<T, bool> predicate) 
        {
           return _table.FirstOrDefault(predicate);
        }

        public List<T> GetAll()
        {
            return _table.ToList();
        }

        public void SaveChanges()
        {
            _appDatabaseContext.SaveChanges();
        }
    }
}
