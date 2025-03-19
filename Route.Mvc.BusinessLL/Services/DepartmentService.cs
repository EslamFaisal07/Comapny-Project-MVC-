using Route.Mvc.BusinessLL.DataTransferObjects.Department;
using Route.Mvc.BusinessLL.Factories;
using Route.Mvc.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Mvc.BusinessLL.Services
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    {

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
            return departments.Select(d => d.ToDepartmentDto());
        }


        public DepartmentDetailsDTO GetDepartmentById(int id)
        {

            var department = _departmentRepository.GetById(id);

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
            return _departmentRepository.Add(department);

        }



        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {


            return _departmentRepository.Update(departmentDto.ToEntity());
        }



        public bool DeleteDepartment(int id)
        {
            var dept = _departmentRepository.GetById(id);
            if (dept is null)
            {
                return false;
            }
            else
            {

                int result = _departmentRepository.Remove(dept);
                return result > 0 ? true : false;
            }


        }












    }
}
