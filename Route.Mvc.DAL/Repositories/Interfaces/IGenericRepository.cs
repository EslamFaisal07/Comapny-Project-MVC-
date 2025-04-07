
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




        int Add(T entity);
        IEnumerable<T> GetAll(bool WithTracking = false);
       
        T? GetById(int id);
        int Remove(T entity);
        int Update(T entity);












    }
}
