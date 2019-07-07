using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrivagoFinance.Repository.TrivagoContext.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IQueryable<T> GetAll();
        T Update(T entity);
        void Delete(T entity);
        T Insert(T entity);
    }
}
