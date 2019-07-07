using System;
using System.Collections.Generic;
using System.Text;
using TrivagoFinance.Data.Entities;
using TrivagoFinance.Repository.TrivagoContext.Interfaces;
using System.Linq;
using TrivagoFinance.Data.ApplicationDbContext;
using System.Linq.Expressions;

namespace TrivagoFinance.Repository.TrivagoContext
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly TrivagoDbContext _trivagoDbContext;
        public Repository(TrivagoDbContext trivagoDbContext)
        {
            _trivagoDbContext = trivagoDbContext;
        }

        public void Delete(T entity)
        {
           var obj = _trivagoDbContext.Set<T>().Remove(entity);
        }

        public T Get(int id)
        {
            return _trivagoDbContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _trivagoDbContext.Set<T>();
        }

        public T Insert(T entity)
        {
            _trivagoDbContext.Set<T>().Add(entity);
            return entity; 
        }

        public T Update(T entity)
        {
            _trivagoDbContext.Set<T>().Update(entity);
            return entity;
        }
    }
}
