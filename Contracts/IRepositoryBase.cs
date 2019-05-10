using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        IEnumerable<T> GetAllBaseData();
        IEnumerable<T> GetByIDBaseData(Expression<Func<T, bool>> expression);
        void PostCreateBaseData(T entity);
        void PutUpdateBaseData(T entity);
        void DeleteByIDBaseData(T entity);
        void SaveBaseData();
    }
}
