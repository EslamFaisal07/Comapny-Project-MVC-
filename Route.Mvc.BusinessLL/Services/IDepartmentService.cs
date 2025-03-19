using Route.Mvc.BusinessLL.DataTransferObjects.Department;

namespace Route.Mvc.BusinessLL.Services
{
    public interface IDepartmentService
    {
        int AddDepartment(CreatedDepartmentDto createdDepartmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDTO GetDepartmentById(int id);
        int UpdateDepartment(UpdatedDepartmentDto departmentDto);
    }
}