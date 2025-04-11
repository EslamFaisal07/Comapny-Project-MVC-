using Route.Mvc.BusinessLL.DataTransferObjects.Department;
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
    public class DepartmentService(  IUnitOfWork _unitOfWork) : IDepartmentService
    {

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return departments.Select(d => d.ToDepartmentDto());
        }


        public DepartmentDetailsDTO GetDepartmentById(int id)
        {

            var department = _unitOfWork.DepartmentRepository.GetById(id);

            if (department is null)
            {
                return null;
            }
            else
            {

                return department.toDepartmentDetailsDto();
            }



        }



        public int AddDepartment(CreatedDepartmentDto createdDepartmentDto)
        {
            var department = createdDepartmentDto.ToEntity();
             _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.SaveChange();
        }



        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {


          _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            var result = _unitOfWork.SaveChange();
            if (result >0)
            {
                return result;
            }
            else
            { 
                return -1;
            }
        }



        public bool DeleteDepartment(int id)
        {
            var dept = _unitOfWork.DepartmentRepository.GetById(id);
            if (dept is null)
            {
                return false;
            }
            else
            {

                _unitOfWork.DepartmentRepository.Remove(dept);
                var result = _unitOfWork.SaveChange();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }












    }
}
