using Route.Mvc.BusinessLL.DataTransferObjects.Employee;
using Route.Mvc.DAL.Models.EmployeeModel;
using Route.Mvc.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.BusinessLL.Factories
{
    public static class EmployeeFactory
    {

        public static EmployeeDto ToEmployeeDto(this Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Email = employee.Email,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Gender = employee.Gender.ToString(),
                EmployeeType = employee.EmployeeType.ToString()

            };

        }




        public static EmployeeDetailsDTO ToEmployeeDetailsDto(this Employee employee)
        {
            return new EmployeeDetailsDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Email = employee.Email,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Address = employee.Address,
                PhoneNumber = employee.PhoneNumber,
                HiringDate = DateOnly.FromDateTime(employee.HiringDate),
                Gender = employee.Gender.ToString(),
                EmployeeType = employee.EmployeeType.ToString(),
            };


        }





        public static Employee ToEntity(this CreatedEmployeeDto createdEmployeeDto)
        {
            return new Employee
            {
                Name = createdEmployeeDto.Name,
                Age = createdEmployeeDto.Age.Value,
                Address = createdEmployeeDto.Address,
                Email = createdEmployeeDto.Email,
                Salary = createdEmployeeDto.Salary,
                IsActive = createdEmployeeDto.IsActive,
                PhoneNumber = createdEmployeeDto.PhoneNumber,
                HiringDate = createdEmployeeDto.HiringDate.ToDateTime(new TimeOnly()),
                Gender = createdEmployeeDto.Gender,
                EmployeeType = createdEmployeeDto.EmployeeType,
                CreatedBy = 1,
                LastModifiedBy = 1

            };


        }




        public static Employee ToEntity(this UpdatedEmployeeDto updatedEmployeeDto)
        {
            return new Employee
            {
                Name = updatedEmployeeDto.Name,
                Age = updatedEmployeeDto.Age,
                Address = updatedEmployeeDto.Address,
                Email = updatedEmployeeDto.Email,
                Salary = updatedEmployeeDto.Salary,
                IsActive = updatedEmployeeDto.IsActive,
                PhoneNumber = updatedEmployeeDto.PhoneNumber,
                HiringDate = updatedEmployeeDto.HiringDate.ToDateTime(new TimeOnly()),
                Gender = Enum.Parse<Gender>(updatedEmployeeDto.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(updatedEmployeeDto.EmployeeType),
                CreatedBy = updatedEmployeeDto.CreatedBy,
                LastModifiedBy = updatedEmployeeDto.LastModifiedBy
            };
        }
    }
}
