using Route.Mvc.DAL.Data.Contexts;
using Route.Mvc.DAL.Models.DepartmentModel;
using Route.Mvc.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.DAL.Repositories.Classes
{
    public class DepartmentRepository(ApplicationDbContexts dbContext) :GenericRepository<Department>(dbContext) ,IDepartmentRepository
    {



















    }
}
