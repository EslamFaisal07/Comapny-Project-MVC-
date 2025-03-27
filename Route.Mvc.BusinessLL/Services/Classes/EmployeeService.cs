using Route.Mvc.BusinessLL.DataTransferObjects.Employee;
using Route.Mvc.BusinessLL.Factories;
using Route.Mvc.BusinessLL.Services.Interfaces;
using Route.Mvc.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.BusinessLL.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var employees = _employeeRepository.GetAll();
            return employees.Select(e=>e.ToEmployeeDto());
            

        }



        public EmployeeDetailsDTO GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null)
            {
                return null;
            }
            else
            return employee.ToEmployeeDetailsDto();
        }








        public int AddEmployee(CreatedEmployeeDto createdEmployeeDto)
        {
            var employee = createdEmployeeDto.ToEntity();
            return _employeeRepository.Add(employee);
        }





        public int UpdateEmployee(UpdatedEmployeeDto EmployeeDto)
        {
            return _employeeRepository.Update(EmployeeDto.ToEntity());
        }








        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null)
            {
                return false;
            }
            else {
                int result = _employeeRepository.Remove(employee);
                return result > 0 ? true : false;
            }
            

        }

    

       
    }
}
