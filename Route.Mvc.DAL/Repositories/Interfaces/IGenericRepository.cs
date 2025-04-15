
using Route.Mvc.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T>  where T :BaseEntity
    {




        void Add(T entity);
        IEnumerable<T> GetAll(bool WithTracking = false);

        IEnumerable<TResult> GetAll<TResult>(Expression<Func<T, TResult>> selector);


        IEnumerable<T> GetAll(Expression<Func<T,bool>> predicate);

        T? GetById(int id);
        void Remove(T entity);
        void Update(T entity);












    }
}
