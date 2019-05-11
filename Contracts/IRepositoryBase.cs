using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    /// <summary>
    /// The main <see cref="IRepositoryBase"/> interface class that is implemented 
    /// and used by the main <see cref="RepositoryBase"/> class. Entities and their 
    /// web api backend access, to process their database manipulations is done 
    /// through this generic class and its implementation.
    /// </summary>
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetAllBaseData();
        IQueryable<T> GetByIDBaseData(Expression<Func<T, bool>> expression);
        void PostCreateBaseData(T entity);
        void PutUpdateBaseData(T entity);
        void DeleteByIDBaseData(T entity);
        Task SaveAsyncBaseData();
    }
}
