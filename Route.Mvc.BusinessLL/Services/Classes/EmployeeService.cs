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
    public class EmployeeService(IUnitOfWork _unitOfWork, IMapper _mapper,IAttachmentService _attachmentService) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
            {
                employees = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.GetAll(e => e.Name.ToLower().Contains(EmployeeSearchName.ToLower()));


            }


            var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            return employeesDto;


        }



        public EmployeeDetailsDTO? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);




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
            if (createdEmployeeDto.Image is not null)
            {
                employee.ImageName = _attachmentService.Upload(createdEmployeeDto.Image, "Images");

            }
             _unitOfWork.EmployeeRepository.Add(employee);
            return _unitOfWork.SaveChange();
        }





        public int UpdateEmployee(UpdatedEmployeeDto EmployeeDto)
        {
            var employee =_mapper.Map<UpdatedEmployeeDto, Employee>(EmployeeDto);
            if (EmployeeDto.Image is not null)
            {
                employee.ImageName = _attachmentService.Upload(EmployeeDto.Image, "Images");

            }

             _unitOfWork.EmployeeRepository.Update(employee);
            var result = _unitOfWork.SaveChange();
            if (result > 0)
            {
                return result;
            }
            else
            {
                return -1;
            }
        }








        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null)
            {
                return false;
            }
            else {
                //int result = _employeeRepository.Remove(employee);
                employee.IsDeleted = true;
                 _unitOfWork.EmployeeRepository.Update(employee);
                var result = _unitOfWork.SaveChange();
                return result > 0 ? true : false;
            }
            

        }

    

       
    }
}
