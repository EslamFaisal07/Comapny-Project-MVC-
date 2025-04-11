using AutoMapper;
using Route.Mvc.BusinessLL.DataTransferObjects.Employee;
using Route.Mvc.BusinessLL.Factories;
using Route.Mvc.BusinessLL.Services.Interfaces;
using Route.Mvc.DAL.Models.EmployeeModel;
using Route.Mvc.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.BusinessLL.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository, IMapper _mapper) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
            {
                employees = _employeeRepository.GetAll();
            }
            else
            {
                employees = _employeeRepository.GetAll(e => e.Name.ToLower().Contains(EmployeeSearchName.ToLower()));


            }


            var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            return employeesDto;


        }



        public EmployeeDetailsDTO? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);




            if (employee is null)
            {
                return null;
            }
            else
            {
            var employeeDetails = _mapper.Map<Employee, EmployeeDetailsDTO>(employee);

                return employeeDetails;
            }
        }








        public int AddEmployee(CreatedEmployeeDto createdEmployeeDto)
        {
            var employee = _mapper.Map<CreatedEmployeeDto , Employee>(createdEmployeeDto);
            return _employeeRepository.Add(employee);
        }





        public int UpdateEmployee(UpdatedEmployeeDto EmployeeDto)
        {
            var employee =_mapper.Map<UpdatedEmployeeDto, Employee>(EmployeeDto);
            return _employeeRepository.Update(employee);
        }








        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null)
            {
                return false;
            }
            else {
                //int result = _employeeRepository.Remove(employee);
                employee.IsDeleted = true;
                int result = _employeeRepository.Update(employee);
                return result > 0 ? true : false;
            }
            

        }

    

       
    }
}
