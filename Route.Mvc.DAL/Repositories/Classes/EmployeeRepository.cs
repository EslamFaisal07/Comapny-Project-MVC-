using Route.Mvc.DAL.Data.Contexts;
using Route.Mvc.DAL.Models.EmployeeModel;
using Route.Mvc.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.DAL.Repositories.Classes
{
    public class EmployeeRepository(ApplicationDbContexts dbContext) :GenericRepository<Employee>(dbContext), IEmployeeRepository
    {
       
    }
}
