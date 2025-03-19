using Route.Mvc.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.DAL.Repositories
{
    public class DepartmentRepository(ApplicationDbContexts dbContext) : IDepartmentRepository
    {
        private readonly ApplicationDbContexts _dbContext = dbContext;



        public IEnumerable<Department> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                return _dbContext.Departments.ToList();
            else
                return _dbContext.Departments.AsNoTracking().ToList();
        }



        public Department? GetById(int id)
        {
            return _dbContext.Departments.Find(id);
        }





        public int Update(Department department)
        {
            _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges();
        }



        public int Remove(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();

        }


        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }




















    }
}
