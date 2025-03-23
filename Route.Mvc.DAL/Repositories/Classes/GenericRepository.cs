using Microsoft.EntityFrameworkCore;
using Route.Mvc.DAL.Data.Contexts;
using Route.Mvc.DAL.Models.DepartmentModel;
using Route.Mvc.DAL.Models.Shared;
using Route.Mvc.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.DAL.Repositories.Classes
{
    public class GenericRepository<T>(ApplicationDbContexts _dbContext) : IGenericRepository<T> where T : BaseEntity
    {

        public IEnumerable<T> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                return _dbContext.Set<T>().ToList();
            else
                return _dbContext.Set<T>().AsNoTracking().ToList();
        }



        public  T? GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }





        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }



        public int Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();
        }


        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }




    }
}
