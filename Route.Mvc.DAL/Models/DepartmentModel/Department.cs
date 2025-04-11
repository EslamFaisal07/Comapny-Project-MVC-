using Route.Mvc.DAL.Models.EmployeeModel;
using Route.Mvc.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.DAL.Models.DepartmentModel
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }


        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();



    }
}
