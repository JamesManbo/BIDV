using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BIDV.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> query);
        T GetById(int id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}