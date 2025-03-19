using Route.Mvc.BusinessLL.DataTransferObjects.Department;
using Route.Mvc.DAL.Models;

namespace Route.Mvc.BusinessLL.Factories
{
    static class DepartmentFactory
    {

        public static DepartmentDto ToDepartmentDto(this Department department)
        {
            return new DepartmentDto
            {
                DeptId = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = DateOnly.FromDateTime(department.CreatedOn.Value)
            };

        }



        public static DepartmentDetailsDTO toDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDTO
            {
                Id = department.Id,
                CreatedBy = department.CreatedBy,
                CreatedOn = DateOnly.FromDateTime(department.CreatedOn.Value),
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn.Value),
                IsDeleted = department.IsDeleted,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description
            };

        }





        public static Department ToEntity(this CreatedDepartmentDto createdDepartmentDto)
        {
            return new Department
            {
                Name = createdDepartmentDto.Name,
                Code = createdDepartmentDto.Code,
                Description = createdDepartmentDto.Description,
                CreatedOn = createdDepartmentDto.DateOfCreation.ToDateTime(new TimeOnly())

            };
        }





        public static Department ToEntity(this UpdatedDepartmentDto departmentDto)
        {
            return new Department
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation.ToDateTime(new TimeOnly())
            };

        }






    }
}
