using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    /// <summary>
    /// The <see cref="RepositoryContext"/> protected DBContext field is the main member of the 
    /// main generic 'RepositoryBase' abstract class. This class is the base data repository 
    /// access layer inherited and used by all other data repositories, through the 
    /// <see cref="RepositoryWrapper"/> class, to access the entity models and 
    /// their related entity models' backend database.
    /// </summary>
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext { get; set; }

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public IEnumerable<T> GetAllBaseData()
        {
            return this.RepositoryContext.Set<T>();
        }

        public IEnumerable<T> GetByIDBaseData(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression);
        }

        public void PostCreateBaseData(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }

        public void PutUpdateBaseData(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }

        public void DeleteByIDBaseData(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }

        public void SaveBaseData()
        {
            this.RepositoryContext.SaveChanges();
        }
    }
}
